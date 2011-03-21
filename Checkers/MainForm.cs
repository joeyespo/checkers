using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Checkers;
using Checkers.Agents;
using System.Net;
using System.Net.Sockets;

namespace Checkers
{
    /// <summary>
    /// Represents the main game form.
    /// </summary>
    public sealed partial class MainForm : Form
    {
        #region Helper Enums

        private enum ClientMessage : byte
        {
            Closed = 0,
            ChatMessage = 1,
            AbortGame = 2,
            MakeMove = 3,
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        #region Event Handler Methods

        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            // Hack: Get actual minimal size
            ClientSize = new Size(CheckersUI.Width + 8, CheckersUI.Height + 8);
            MinimumSize = Size;
            // Set initial size
            Size = new Size(MinimumSize.Width + 130, MinimumSize.Height);
            // Load settings
            settings = CheckersSettings.Load();
            UpdateBoard();
        }

        /// <summary>
        /// Handles the Activated event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmMain_Activated(object sender, System.EventArgs e)
        {
            tmrFlashWindow.Stop();
            if(gameType == CheckersGameType.NetGame)
                txtSend.Select();
        }

        /// <summary>
        /// Handles the Deactivate event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmMain_Deactivate(object sender, System.EventArgs e)
        {
            if((gameType == CheckersGameType.NetGame) && (CheckersUI.IsPlaying) && (CheckersUI.Game.Turn == 1))
                if(settings.FlashWindowOnTurn)
                    DoFlashWindow();
        }

        /// <summary>
        /// Handles the Closing event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!DoCloseGame())
                e.Cancel = true;
        }

        /// <summary>
        /// Handles the Click event of the menuGameNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuGameNew_Click(object sender, System.EventArgs e)
        {
            DoNewGame();
        }

        /// <summary>
        /// Handles the Click event of the menuGameEnd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuGameEnd_Click(object sender, System.EventArgs e)
        {
            DoCloseGame();
        }

        /// <summary>
        /// Handles the Click event of the menuGameExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuGameExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Popup event of the menuView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuView_Popup(object sender, System.EventArgs e)
        {
            menuViewGamePanel.Checked = (Width != MinimumSize.Width);
            menuViewNetPanel.Checked = (Height != MinimumSize.Height);
        }

        /// <summary>
        /// Handles the Click event of the menuViewGamePanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuViewGamePanel_Click(object sender, System.EventArgs e)
        {
            menuViewGamePanel.Checked = !menuViewGamePanel.Checked;
            Width = ((menuViewGamePanel.Checked) ? (MinimumSize.Width + 140) : (MinimumSize.Width));
        }

        /// <summary>
        /// Handles the Click event of the menuViewNetPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuViewNetPanel_Click(object sender, System.EventArgs e)
        {
            menuViewNetPanel.Checked = !menuViewNetPanel.Checked;
            Height = ((menuViewNetPanel.Checked) ? (MinimumSize.Height + 80) : (MinimumSize.Height));
        }

        /// <summary>
        /// Handles the Click event of the menuViewHint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuViewHint_Click(object sender, System.EventArgs e)
        {
            CheckersUI.ShowHint();
        }

        /// <summary>
        /// Handles the Click event of the menuViewLastMoved control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuViewLastMoved_Click(object sender, System.EventArgs e)
        {
            CheckersUI.ShowLastMove();
        }

        /// <summary>
        /// Handles the Click event of the menuViewPreferences control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuViewPreferences_Click(object sender, System.EventArgs e)
        {
            PreferencesDialog form = new PreferencesDialog();
            form.Settings = settings;
            if(form.ShowDialog(this) == DialogResult.Cancel)
                return;
            settings = form.Settings;
            UpdateBoard();
        }

        /// <summary>
        /// Handles the Click event of the menuHelpAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuHelpAbout_Click(object sender, System.EventArgs e)
        {
            (new AboutDialog()).ShowDialog(this);
        }

        /// <summary>
        /// Handles the Popup event of the menuChat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChat_Popup(object sender, System.EventArgs e)
        {
            menuChatClear.Enabled = txtChat.TextLength > 0;
            menuChatCopy.Enabled = txtChat.SelectionLength > 0;
        }

        /// <summary>
        /// Handles the Click event of the menuChatCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatCopy_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetDataObject(txtChat.Text, true);
        }

        /// <summary>
        /// Handles the Click event of the menuChatSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatSave_Click(object sender, System.EventArgs e)
        {
            if(dlgSaveChat.ShowDialog(this) != DialogResult.OK)
                return;
            txtChat.SaveFile(dlgSaveChat.FileName, ((Path.GetExtension(dlgSaveChat.FileName) == ".rtf") ? (RichTextBoxStreamType.RichText) : (RichTextBoxStreamType.PlainText)));
        }

        /// <summary>
        /// Handles the Click event of the menuChatClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatClear_Click(object sender, System.EventArgs e)
        {
            if(MessageBox.Show(this, "Clear the chat window?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            txtChat.Clear();
        }

        /// <summary>
        /// Handles the Click event of the menuChatSelectAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatSelectAll_Click(object sender, System.EventArgs e)
        {
            txtChat.SelectAll();
        }

        /// <summary>
        /// Handles the GameStarted event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckersUI_GameStarted(object sender, System.EventArgs e)
        {
            DoStarted();
            PlaySound(CheckersSounds.Begin);
        }

        /// <summary>
        /// Handles the GameStopped event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckersUI_GameStopped(object sender, System.EventArgs e)
        {
            DoStopped();
        }

        /// <summary>
        /// Handles the TurnChanged event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckersUI_TurnChanged(object sender, System.EventArgs e)
        {
            DoNextTurn();
        }

        /// <summary>
        /// Handles the WinnerDeclared event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckersUI_WinnerDeclared(object sender, System.EventArgs e)
        {
            DoWinnerDeclared();
            if(((gameType == CheckersGameType.SinglePlayer) || (gameType == CheckersGameType.NetGame)) && (CheckersUI.Winner != 1))
                PlaySound(CheckersSounds.Lost);
            else
                PlaySound(CheckersSounds.Winner);
        }

        /// <summary>
        /// Handles the PiecePickedUp event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckersUI_PiecePickedUp(object sender, System.EventArgs e)
        {
            PlaySound(CheckersSounds.Select);
        }

        /// <summary>
        /// Handles the PieceMoved event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Checkers.UI.MoveEventArgs"/> instance containing the event data.</param>
        private void CheckersUI_PieceMoved(object sender, Checkers.UI.MoveEventArgs e)
        {
            if(!e.IsWinningMove)
            {
                if(e.Move.Kinged)
                    PlaySound(CheckersSounds.King);
                else if(e.Move.Jumped.Length == 1)
                    PlaySound(CheckersSounds.Jump);
                else if(e.Move.Jumped.Length > 1)
                    PlaySound(CheckersSounds.JumpMultiple);
                else
                    PlaySound(CheckersSounds.Drop);
                if((settings.ShowTextFeedback) && (e.MovedByPlayer) && (e.Move.Jumped.Length > 1))
                {
                    if((e.Move.Piece.Player == 2) && (gameType != CheckersGameType.Multiplayer))
                        return;
                    CheckersUI.Text = "";
                    if(e.Move.Jumped.Length > 3)
                    {
                        tmrTextDisplay.Interval = 2500;
                        CheckersUI.TextBorderColor = Color.White;
                        CheckersUI.ForeColor = Color.LightSalmon;
                        CheckersUI.Text = "INCREDIBLE !!";
                    }
                    else if(e.Move.Jumped.Length > 2)
                    {
                        tmrTextDisplay.Interval = 2000;
                        CheckersUI.TextBorderColor = Color.White;
                        CheckersUI.ForeColor = Color.RoyalBlue;
                        CheckersUI.Text = "AWESOME !!";
                    }
                    else
                    {
                        tmrTextDisplay.Interval = 1000;
                        CheckersUI.TextBorderColor = Color.Black;
                        CheckersUI.ForeColor = Color.PaleTurquoise;
                        CheckersUI.Text = "NICE !!";
                    }
                    tmrTextDisplay.Start();
                }
            }

            if((gameType == CheckersGameType.NetGame) && (e.MovedByPlayer) && (remotePlayer != null))
                DoMovePieceNet(e.Move);
        }

        /// <summary>
        /// Handles the PieceMovedPartial event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Checkers.UI.MoveEventArgs"/> instance containing the event data.</param>
        private void CheckersUI_PieceMovedPartial(object sender, Checkers.UI.MoveEventArgs e)
        {
            if(e.Move.Kinged)
                PlaySound(CheckersSounds.King);
            else if(e.Move.Jumped.Length == 1)
                PlaySound(CheckersSounds.Jump);
            else if(e.Move.Jumped.Length > 1)
                PlaySound(CheckersSounds.JumpMultiple);
            else
                PlaySound(CheckersSounds.Drop);
        }

        /// <summary>
        /// Handles the PieceBadMove event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Checkers.UI.MoveEventArgs"/> instance containing the event data.</param>
        private void CheckersUI_PieceBadMove(object sender, Checkers.UI.MoveEventArgs e)
        {
            PlaySound(CheckersSounds.BadMove);
        }

        /// <summary>
        /// Handles the PieceDeselected event of the CheckersUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckersUI_PieceDeselected(object sender, System.EventArgs e)
        {
            PlaySound(CheckersSounds.Deselect);
        }

        /// <summary>
        /// Handles the Tick event of the CheckersAgent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Checkers.AgentTickEventArgs"/> instance containing the event data.</param>
        private void CheckersAgent_Tick(object sender, AgentTickEventArgs e)
        {
            Application.DoEvents();
        }

        /// <summary>
        /// Handles the Tick event of the tmrTimePassed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmrTimePassed_Tick(object sender, System.EventArgs e)
        {
            DoUpdateTimePassed();
        }

        /// <summary>
        /// Handles the Tick event of the tmrTextDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmrTextDisplay_Tick(object sender, System.EventArgs e)
        {
            tmrTextDisplay.Stop();
            CheckersUI.Text = "";
        }

        /// <summary>
        /// Handles the Tick event of the tmrFlashWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmrFlashWindow_Tick(object sender, System.EventArgs e)
        {
            if(Form.ActiveForm == this)
            {
                tmrFlashWindow.Stop();
                return;
            }
            FlashWindow(Handle, 1);
        }

        /// <summary>
        /// Handles the Tick event of the tmrConnection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmrConnection_Tick(object sender, System.EventArgs e)
        {
            if(gameType != CheckersGameType.NetGame)
                return;
            CheckForClientMessage();
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkLocalIP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lnkLocalIP_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetDataObject(lnkLocalIP.Text, true);
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkRemoteIP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lnkRemoteIP_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetDataObject(lnkRemoteIP.Text, true);
        }

        /// <summary>
        /// Handles the Enter event of the txtSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtSend_Enter(object sender, System.EventArgs e)
        {
            AcceptButton = btnSend;
        }

        /// <summary>
        /// Handles the Leave event of the txtSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtSend_Leave(object sender, System.EventArgs e)
        {
            AcceptButton = null;
        }

        /// <summary>
        /// Handles the Click event of the btnSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSend_Click(object sender, System.EventArgs e)
        {
            DoSendMessage();
        }

        #endregion

        #region Game Methods

        /// <summary>
        /// Starts a new game.
        /// </summary>
        private void DoNewGame()
        {
            if((CheckersUI.IsPlaying) || (CheckersUI.Winner != 0))
            {
                if(!DoCloseGame())
                    return;
                // Stop current game (with no winner)
                CheckersUI.Stop();
            }

            // Get new game type
            NewGameDialog newGame = new NewGameDialog(settings, agentNames);
            // Set defaults
            newGame.GameType = gameType;
            newGame.Player1Name = lblNameP1.Text;
            newGame.Player2Name = lblNameP2.Text;

            // Show dialog
            if(newGame.ShowDialog(this) == DialogResult.Cancel)
                return;

            // Set new game parameters
            gameType = newGame.GameType;
            agent = null;

            // Set Game Panel properties
            lblNameP1.Text = newGame.Player1Name;
            lblNameP2.Text = newGame.Player2Name;
            picPawnP1.Image = newGame.ImageSet[0];
            picPawnP2.Image = newGame.ImageSet[2];

            // Set UI properties
            switch(gameType)
            {
                case CheckersGameType.SinglePlayer:
                    CheckersUI.Player1Active = true;
                    CheckersUI.Player2Active = false;
                    agent = agents[newGame.AgentIndex];
                    break;
                case CheckersGameType.Multiplayer:
                    CheckersUI.Player1Active = true;
                    CheckersUI.Player2Active = true;
                    break;
                case CheckersGameType.NetGame:
                    remotePlayer = newGame.RemotePlayer;
                    if(remotePlayer == null)
                    {
                        MessageBox.Show(this, "Remote user disconnected before the game began", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    CheckersUI.Player1Active = true;
                    CheckersUI.Player2Active = (remotePlayer == null);
                    if(!menuViewNetPanel.Checked)
                        menuViewNetPanel_Click(menuViewNetPanel, EventArgs.Empty);
                    tmrConnection.Start();
                    panNet.Visible = true;
                    lnkLocalIP.Text = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                    lnkRemoteIP.Text = ((IPEndPoint)remotePlayer.Socket.RemoteEndPoint).Address.ToString();
                    AppendMessage("", "Connected to player");
                    break;
                default:
                    return;
            }
            CheckersUI.CustomPawn1 = newGame.ImageSet[0];
            CheckersUI.CustomKing1 = newGame.ImageSet[1];
            CheckersUI.CustomPawn2 = newGame.ImageSet[2];
            CheckersUI.CustomKing2 = newGame.ImageSet[3];

            // Create the new game
            CheckersGame game = new CheckersGame();
            game.FirstMove = newGame.FirstMove;

            // Start a new checkers game
            CheckersUI.Play(game);
        }

        /// <summary>
        /// Called after a game is started.
        /// </summary>
        private void DoStarted()
        {
            if(agent != null)
                agent.Tick += new AgentTickEventHandler(CheckersAgent_Tick);
            playTime = DateTime.Now;
            tmrTimePassed.Start();
            DoUpdateTimePassed();
            panGameInfo.Visible = true;
            menuGameEnd.Enabled = true;
            DoNextTurn();
        }

        /// <summary>
        /// Called after a game is stopped.
        /// </summary>
        private void DoStopped()
        {
            if(agent != null)
                agent.Tick -= new AgentTickEventHandler(CheckersAgent_Tick);
            panGameInfo.Visible = false;
            menuGameEnd.Enabled = false;
            tmrTimePassed.Stop();
            txtTimePassed.Text = "0:00";
            CheckersUI.Text = "";
        }

        /// <summary>
        /// Called after a player's turn is up.
        /// </summary>
        private void DoNextTurn()
        {
            DoShowTurn(CheckersUI.Game.Turn);
            UpdatePlayerInfo();
            if((gameType == CheckersGameType.SinglePlayer) && (CheckersUI.Game.Turn == 2))
            {
                // Move agent after we leave this event
                BeginInvoke(new DoMoveAgentDelegate(DoMoveAgent));
            }
        }

        /// <summary>
        /// Called after a winner is declared.
        /// </summary>
        private void DoWinnerDeclared()
        {
            UpdatePlayerInfo();
            tmrTimePassed.Stop();
            DoUpdateTimePassed();
            DoShowWinner(CheckersUI.Winner);
        }

        /// <summary>
        /// Called after the game is closed.
        /// </summary>
        private bool DoCloseGame()
        {
            return DoCloseGame(false);
        }

        /// <summary>
        /// Called after the game is closed.
        /// </summary>
        private bool DoCloseGame(bool forced)
        {
            if((!CheckersUI.IsPlaying) && (CheckersUI.Winner == 0))
                return true;
            if(CheckersUI.IsPlaying)
            {
                if(!forced)
                {
                    // Confirm the quit
                    if(MessageBox.Show(this, "Quit current game?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
                }
                // Forceful end sound
                PlaySound(CheckersSounds.ForceEnd);
            }
            else
            {
                PlaySound(CheckersSounds.EndGame);
            }
            picTurn.Visible = false;
            if(gameType == CheckersGameType.NetGame)
            {
                tmrConnection.Stop();
                panNet.Visible = false;
                lnkLocalIP.Text = "";
                lnkRemoteIP.Text = "";
                if(remotePlayer != null)
                {
                    try
                    {
                        BinaryWriter bw = new BinaryWriter(new NetworkStream(remotePlayer.Socket, false));
                        bw.Write((byte)ClientMessage.Closed);
                        remotePlayer.Close();
                        bw.Close();
                    }
                    catch(IOException)
                    {
                    }
                    catch(SocketException)
                    {
                    }
                    catch(InvalidOperationException)
                    {
                    }
                    AppendMessage("", "Connection closed");
                    remotePlayer = null;
                }
            }
            CheckersUI.Stop();
            return true;
        }

        /// <summary>
        /// Updates the time passed.
        /// </summary>
        private void DoUpdateTimePassed()
        {
            TimeSpan time = DateTime.Now.Subtract(playTime);
            txtTimePassed.Text = ((int)time.TotalMinutes).ToString().PadLeft(2, '0') + ":" + time.Seconds.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Moves the agent.
        /// </summary>
        private void DoMoveAgent()
        {
            // Do events before moving
            //Refresh();
            Application.DoEvents();
            if((gameType == CheckersGameType.SinglePlayer) && (CheckersUI.Game.Turn == 2))
                CheckersUI.MovePiece(agent);
        }

        /// <summary>
        /// Shows the current player's turn.
        /// </summary>
        private void DoShowTurn(int player)
        {
            SuspendLayout();
            picTurn.Visible = false;
            lblNameP1.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblNameP1.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            lblNameP2.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblNameP2.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            if(player == 1)
            {
                picTurn.Image = imlTurn.Images[0];
                picTurn.Top = picPawnP1.Top + 1;
                picTurn.Visible = true;
                lblNameP1.BackColor = Color.FromKnownColor(KnownColor.Highlight);
                lblNameP1.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
            }
            else if(player == 2)
            {
                picTurn.Image = imlTurn.Images[((gameType == CheckersGameType.Multiplayer) ? (0) : (1))];
                picTurn.Top = picPawnP2.Top + 1;
                picTurn.Visible = true;
                lblNameP2.BackColor = Color.FromKnownColor(KnownColor.Highlight);
                lblNameP2.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
            }
            ResumeLayout();
        }

        /// <summary>
        /// Shows the declared winner.
        /// </summary>
        private void DoShowWinner(int player)
        {
            SuspendLayout();
            tmrTextDisplay.Stop();
            CheckersUI.Text = "";
            CheckersUI.TextBorderColor = Color.White;
            CheckersUI.ForeColor = Color.Gold;
            picTurn.Visible = false;
            lblNameP1.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblNameP1.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            lblNameP2.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblNameP2.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            if(player == 1)
            {
                picTurn.Image = imlTurn.Images[2];
                picTurn.Top = picPawnP1.Top + 1;
                picTurn.Visible = true;
                // Display appropriate message
                if((gameType == CheckersGameType.SinglePlayer) || (gameType == CheckersGameType.NetGame))
                {
                    CheckersUI.Text = "You Win!";
                }
                else
                {
                    CheckersUI.Text = lblNameP1.Text + "\nWins";
                }
            }
            else if(player == 2)
            {
                picTurn.Image = imlTurn.Images[2];
                picTurn.Top = picPawnP2.Top + 1;
                picTurn.Visible = true;
                if((gameType == CheckersGameType.SinglePlayer) || (gameType == CheckersGameType.NetGame))
                {
                    CheckersUI.ForeColor = Color.Coral;
                    CheckersUI.Text = "You Lose!";
                }
                else
                {
                    CheckersUI.Text = lblNameP2.Text + "\nWins";
                }
            }
            ResumeLayout();
        }

        /// <summary>
        /// Flashes the window.
        /// </summary>
        private void DoFlashWindow()
        {
            if(Form.ActiveForm == this)
                return;
            FlashWindow(Handle, 1);
            tmrFlashWindow.Start();
        }

        /// <summary>
        /// Updates the player info.
        /// </summary>
        private void UpdatePlayerInfo()
        {
            SuspendLayout();
            // Update player information
            txtRemainingP1.Text = CheckersUI.Game.GetRemainingCount(1).ToString();
            txtJumpsP1.Text = CheckersUI.Game.GetJumpedCount(2).ToString();
            txtRemainingP2.Text = CheckersUI.Game.GetRemainingCount(2).ToString();
            txtJumpsP2.Text = CheckersUI.Game.GetJumpedCount(1).ToString();
            ResumeLayout();
        }

        /// <summary>
        /// Sends a message to the opponent.
        /// </summary>
        private void DoSendMessage()
        {
            if((gameType != CheckersGameType.NetGame) || (remotePlayer == null))
            {
                MessageBox.Show(this, "Not connected to any other players", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(txtSend.Text.Trim() == "")
                return;

            try
            {
                BinaryWriter bw = new BinaryWriter(new NetworkStream(remotePlayer.Socket, false));
                bw.Write((byte)ClientMessage.ChatMessage);
                bw.Write(txtSend.Text.Trim());
                bw.Close();
            }
            catch(IOException)
            {
            }
            catch(SocketException)
            {
            }
            catch(InvalidOperationException)
            {
            }

            AppendMessage(lblNameP1.Text, txtSend.Text);
            txtSend.Text = "";
            txtSend.Select();
        }

        /// <summary>
        /// Sends the player's move to the opponent's game.
        /// </summary>
        private void DoMovePieceNet(CheckersMove move)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(new NetworkStream(remotePlayer.Socket, false));
                bw.Write((byte)ClientMessage.MakeMove);
                bw.Write(move.InitialPiece.Location.X);
                bw.Write(move.InitialPiece.Location.Y);
                bw.Write(move.Path.Length);
                foreach(Point point in move.Path)
                {
                    bw.Write(point.X);
                    bw.Write(point.Y);
                }
                bw.Close();
            }
            catch(IOException)
            {
                AppendMessage("", "Connection closed");
                CloseNetGame();
                remotePlayer = null;
            }
            catch(SocketException ex)
            {
                AppendMessage("", "Disconnected from opponent: " + ex.Message);
                CloseNetGame();
                remotePlayer = null;
            }
            catch(InvalidOperationException ex)
            {
                AppendMessage("", "Disconnected from opponent: " + ex.Message);
                CloseNetGame();
                remotePlayer = null;
            }
        }

        /// <summary>
        /// Updates the board.
        /// </summary>
        private void UpdateBoard()
        {
            CheckersUI.BackColor = settings.BackColor;
            CheckersUI.BoardBackColor = settings.BoardBackColor;
            CheckersUI.BoardForeColor = settings.BoardForeColor;
            CheckersUI.BoardGridColor = settings.BoardGridColor;
            CheckersUI.HighlightSelection = settings.HighlightSelection;
            CheckersUI.HighlightPossibleMoves = settings.HighlightPossibleMoves;
            CheckersUI.ShowJumpMessage = settings.ShowJumpMessage;
        }

        /// <summary>
        /// Plays a sound.
        /// </summary>
        private void PlaySound(CheckersSounds sound)
        {
            // Play sound
            if(settings.MuteSounds)
                return;
            string soundFileName = settings.sounds[(int)sound];
            string fileName = ((Path.IsPathRooted(soundFileName)) ? (soundFileName) : (Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds\\" + soundFileName));
            // Play sound
            sndPlaySound(fileName, IntPtr.Zero, (SoundFlags.SND_FileName | SoundFlags.SND_ASYNC | SoundFlags.SND_NOWAIT));
        }

        /// <summary>
        /// Checks for a message from the opponent.
        /// </summary>
        private void CheckForClientMessage()
        {
            if(inCheckForClientMessage)
                return;
            inCheckForClientMessage = true;
            if(remotePlayer == null)
                return;

            try
            {
                NetworkStream ns = new NetworkStream(remotePlayer.Socket, false);
                BinaryReader br = new BinaryReader(ns);
                BinaryWriter bw = new BinaryWriter(ns);
                while(ns.DataAvailable)
                {
                    switch((ClientMessage)br.ReadByte())
                    {
                        case ClientMessage.Closed:
                            throw new IOException();
                        case ClientMessage.ChatMessage:
                            AppendMessage(lblNameP2.Text, br.ReadString());
                            if(settings.FlashWindowOnGameEvents)
                                DoFlashWindow();
                            break;
                        case ClientMessage.AbortGame:
                            AppendMessage("", "Game has been aborted by opponent");
                            CloseNetGame();
                            break;
                        case ClientMessage.MakeMove:
                            if(CheckersUI.Game.Turn != 2)
                            {
                                AppendMessage("", "Opponent took turn out of place; game aborted");
                                CloseNetGame();
                                bw.Write((byte)ClientMessage.AbortGame);
                                break;
                            }
                            // Get move
                            Point location = RotateOpponentPiece(br);
                            CheckersPiece piece = CheckersUI.Game.PieceAt(location);
                            int count = br.ReadInt32();
                            Point[] path = new Point[count];
                            for(int i = 0; i < count; i++)
                                path[i] = RotateOpponentPiece(br);
                            // Move the piece an break if successful
                            if(piece != null)
                            {
                                if(CheckersUI.MovePiece(piece, path, true, true))
                                {
                                    if(settings.FlashWindowOnTurn)
                                        DoFlashWindow();
                                    break;
                                }
                            }
                            AppendMessage("", "Opponent made a bad move; game aborted");
                            CloseNetGame();
                            bw.Write((byte)ClientMessage.AbortGame);
                            break;
                    }
                }
                br.Close();
                bw.Close();
            }
            catch(IOException)
            {
                AppendMessage("", "Connection closed");
                CloseNetGame();
                remotePlayer = null;
            }
            catch(SocketException ex)
            {
                AppendMessage("", "Disconnected from opponent: " + ex.Message);
                CloseNetGame();
                remotePlayer = null;
            }
            catch(InvalidOperationException ex)
            {
                AppendMessage("", "Disconnected from opponent: " + ex.Message);
                CloseNetGame();
                remotePlayer = null;
            }
            inCheckForClientMessage = false;
        }

        /// <summary>
        /// Closes the network game.
        /// </summary>
        private void CloseNetGame()
        {
            if(settings.FlashWindowOnGameEvents)
                DoFlashWindow();
            if(CheckersUI.IsPlaying)
                CheckersUI.Game.DeclareStalemate();
        }

        /// <summary>
        /// Appends a new message.
        /// </summary>
        private void AppendMessage(string name, string message)
        {
            txtChat.SelectionStart = txtChat.TextLength;
            txtChat.SelectionLength = 0;
            if(name == "")
            {
                txtChat.SelectionColor = Color.Red;
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
            }
            else if(name == lblNameP1.Text)
            {
                txtChat.SelectionColor = Color.Red;
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
                txtChat.AppendText(" " + name + ": ");
                txtChat.SelectionColor = Color.Black;
                PlaySound(CheckersSounds.SendMessage);
            }
            else
            {
                txtChat.SelectionColor = Color.Blue;
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
                txtChat.AppendText(" " + name + ": ");
                txtChat.SelectionColor = Color.Black;
                PlaySound(CheckersSounds.ReceiveMessage);
            }
            txtChat.AppendText(message);
            txtChat.AppendText("\n");
            txtChat.Select(txtChat.TextLength, 0);
            txtChat.ScrollToCaret();
            int min = 0, max = 0;
            GetScrollRange(txtChat.Handle, 1, ref min, ref max);
            SendMessage(txtChat.Handle, EM_SETSCROLLPOS, 0, new Win32Point(0, max - txtChat.Height));
        }

        /// <summary>
        /// Rotates the opponent's incoming piece.
        /// </summary>
        private Point RotateOpponentPiece(BinaryReader br)
        {
            int x = br.ReadInt32();
            int y = br.ReadInt32();
            return RotateOpponentPiece(new Point(x, y));
        }

        /// <summary>
        /// Rotates the opponent's incoming piece.
        /// </summary>
        private Point RotateOpponentPiece(Point location)
        {
            return new Point(CheckersGame.BoardSize.Width - location.X - 1, CheckersGame.BoardSize.Height - location.Y - 1);
        }

        #endregion

        #region Win32 Members

        [DllImport("user32", EntryPoint = "GetScrollRange", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int GetScrollRange(IntPtr hWnd, int nBar, ref int lpMinPos, ref int lpMaxPos);

        [DllImport("user32", EntryPoint = "SendMessageA", ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern int SendMessage(IntPtr hWnd, int ByVal, int wParam, Win32Point lParam);

        [StructLayout(LayoutKind.Sequential)]
        private class Win32Point
        {
            public int x;
            public int y;

            public Win32Point()
            {
                x = 0;
                y = 0;
            }

            public Win32Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private static readonly int EM_SETSCROLLPOS = 0x400 + 222;

        [DllImport("user32", EntryPoint = "FlashWindow", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.Winapi)]
        private static extern int FlashWindow(IntPtr hWnd, int bInvert);

        [DllImport("winmm.dll", EntryPoint = "PlaySound", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool sndPlaySound(string pszSound, IntPtr hMod, SoundFlags sf);

        [Flags]
        public enum SoundFlags : int
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FileName = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        #endregion

        private delegate void DoMoveAgentDelegate();
        private readonly CheckersAgent[] agents = { new MinMaxSimpleAgent(0), new MinMaxSimpleAgent(1), new MinMaxSimpleAgent(2), new MinMaxSimpleAgent(3), new MinMaxComplexAgent(3) };
        private readonly string[] agentNames = { "Beginner", "Intermediate", "Advanced", "Expert", "Uber (Experimental)" };
        private CheckersGameType gameType = CheckersGameType.None;
        private CheckersAgent agent = null;
        private CheckersSettings settings;
        private DateTime playTime;
        private MenuItem menuViewHint;
        private TcpClient remotePlayer = null;
        private bool inCheckForClientMessage = false;
    }
}
