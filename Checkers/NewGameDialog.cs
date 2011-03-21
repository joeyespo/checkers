using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Checkers
{
    public enum CheckersGameType
    {
        None = 0,
        SinglePlayer = 1,
        Multiplayer = 2,
        NetGame = 3,
    }

    public struct PlayerInfo
    {
        public string Name;
        public bool IsPlayer;
    }

    internal enum ClientMessage : byte
    {
        Closed = 0,
        Header = 1,
        RefreshPlayerInfo = 2,
        RefreshNetImages = 3,
        ChatMessage = 4,
        BeginGame = 5,
    }

    /// <summary>
    /// Represents the "New Game" dialog.
    /// </summary>
    public sealed partial class NewGameDialog : Form
    {
        #region Class Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameDialog"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="agents">The agents.</param>
        public NewGameDialog(CheckersSettings settings, string[] agents)
        {
            InitializeComponent();

            this.settings = settings;
            this.imageSet = new Image[4];

            // Fill agent list
            cmbAgent1P.Items.Clear();
            cmbAgent1P.Items.AddRange(agents);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public CheckersGameType GameType
        {
            get
            {
                return gameType;
            }
            set
            {
                gameType = value;
            }
        }

        /// <summary>
        /// Gets which player has the first move.
        /// </summary>
        public int FirstMove
        {
            get
            {
                return firstMove;
            }
        }

        /// <summary>
        /// Gets the selected agent index.
        /// </summary>
        public int AgentIndex
        {
            get
            {
                return agentIndex;
            }
        }

        /// <summary>
        /// Gets the image set.
        /// </summary>
        public Image[] ImageSet
        {
            get
            {
                return imageSet;
            }
        }

        /// <summary>
        /// Gets or sets the name of the first player.
        /// </summary>
        /// <value>The name of the first player.</value>
        public string Player1Name
        {
            get
            {
                return player1Name;
            }
            set
            {
                player1Name = value;
            }
        }
        /// <summary>
        /// Gets or sets the name of the second player.
        /// </summary>
        /// <value>The name of the second player.</value>
        public string Player2Name
        {
            get
            {
                return player2Name;
            }
            set
            {
                player2Name = value;
            }
        }

        /// <summary>
        /// Gets or sets the remote player.
        /// </summary>
        /// <value>The remote player.</value>
        public TcpClient RemotePlayer
        {
            get
            {
                return remotePlayer;
            }
            set
            {
                remotePlayer = value;
            }
        }

        #endregion

        #region Event Handler Methods

        #region Form Event Handler Methods

        /// <summary>
        /// Handles the Load event of the frmNewGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmNewGame_Load(object sender, EventArgs e)
        {
            // Add tournament images
            imlImageSet.Images.Add(CreatePieceImage(Color.Firebrick, false));
            imlImageSet.Images.Add(CreatePieceImage(Color.Firebrick, true));
            imlImageSet.Images.Add(CreatePieceImage(Color.LightGray, false));
            imlImageSet.Images.Add(CreatePieceImage(Color.LightGray, true));

            // Load presets
            EnumPresetMenuItems();

            // Set initial combobox values
            cmbFirstMove1P.SelectedIndex = 0;
            cmbImageSet1P.SelectedIndex = 0;
            cmbAgent1P.SelectedIndex = 0;

            cmbFirstMove2P.SelectedIndex = 0;
            cmbImageSet2P.SelectedIndex = 0;

            cmbNetGameType.SelectedIndex = 0;
            cmbFirstMoveNet.SelectedIndex = 0;
            SetImageSet(0, picPawnNet1, picKingNet1, picPawnNet2, picKingNet2);
            picKingNet1.Visible = true;
            picKingNet2.Visible = true;
            panNet.BringToFront();

            // Select initial control
            cmbAgent1P.Select();
        }

        /// <summary>
        /// Handles the Activated event of the frmNewGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmNewGame_Activated(object sender, EventArgs e)
        {
            if((tabGame.SelectedIndex == 2) && (panNetSettings.Visible))
                txtSend.Select();
        }

        /// <summary>
        /// Handles the Closing event of the frmNewGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void frmNewGame_Closing(object sender, CancelEventArgs e)
        {
            if(DialogResult != DialogResult.OK)
                return;
            switch(tabGame.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    if((txtPlayerName2P1.Text.Trim() != "") && (txtPlayerName2P1.Text.ToLower().Trim() == txtPlayerName2P2.Text.ToLower().Trim()))
                    {
                        MessageBox.Show(this, "Player names must not be the same", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPlayerName2P2.SelectAll();
                        txtPlayerName2P2.Select();
                    }
                    else
                        break;
                    e.Cancel = true;
                    break;
                case 2:
                    if(remotePlayer != null)
                        break;
                    if(panNet.Visible)
                        MessageBox.Show(this, "You must create or join a game before you can play", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if((hostClient != null) || (clientListener == null))
                        MessageBox.Show(this, "Only the owner of this Net Game may begin the game", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if(GetNetPlayerCount() < 2)
                        MessageBox.Show(this, "Cannot start a net game without first selecting two players", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if(GetNetPlayerCount() == 2)
                    {
                        // Server now must send game info to all connected clients (but not to self, even if self is playing)
                        foreach(TcpClient client in clients)
                        {
                            if(client.Tag == null)
                                continue;
                            PlayerInfo info = ((PlayerInfo)client.Tag);
                            try
                            {
                                BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
                                bw.Write((byte)ClientMessage.BeginGame);
                                if(!info.IsPlayer)
                                {
                                    bw.Write(false);          // NOT playing; just exit
                                    bw.Close();
                                    continue;
                                }
                                bw.Write(true);             // Playing; continue with game data
                                // Write player 1 data
                                bw.Write(info.Name);
                                bw.Write(info.Name == cmbFirstMoveNet.Text);    // Has first move?
                                MemoryStream mem;
                                Image pawn = ((lblImageNet1.Text == info.Name) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                                Image king = ((lblImageNet1.Text == info.Name) ? (picKingNet1.Image) : (picKingNet2.Image));
                                mem = new MemoryStream();
                                pawn.Save(mem, ImageFormat.Png);
                                bw.Write((int)mem.Length);
                                bw.Write(mem.ToArray());
                                mem = new MemoryStream();
                                king.Save(mem, ImageFormat.Png);
                                bw.Write((int)mem.Length);
                                bw.Write(mem.ToArray());
                                // Either server is a player or it is not
                                if(isSelfPlayer)
                                {
                                    // Write player 2 data
                                    bw.Write(txtPlayerNameNet.Text);
                                    pawn = ((lblImageNet1.Text == txtPlayerNameNet.Text) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                                    king = ((lblImageNet1.Text == txtPlayerNameNet.Text) ? (picKingNet1.Image) : (picKingNet2.Image));
                                    mem = new MemoryStream();
                                    pawn.Save(mem, ImageFormat.Png);
                                    bw.Write((int)mem.Length);
                                    bw.Write(mem.ToArray());
                                    mem = new MemoryStream();
                                    king.Save(mem, ImageFormat.Png);
                                    bw.Write((int)mem.Length);
                                    bw.Write(mem.ToArray());
                                }
                                else
                                {
                                    foreach(TcpClient client2 in clients)
                                    {
                                        if(client2 == client)
                                            continue;
                                        if(client2.Tag == null)
                                            continue;
                                        PlayerInfo info2 = ((PlayerInfo)client2.Tag);
                                        if(!info2.IsPlayer)
                                            continue;
                                        // Write player 2 data
                                        bw.Write(info2.Name);
                                        pawn = ((lblImageNet1.Text == info2.Name) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                                        king = ((lblImageNet1.Text == info2.Name) ? (picKingNet1.Image) : (picKingNet2.Image));
                                        mem = new MemoryStream();
                                        pawn.Save(mem, ImageFormat.Png);
                                        bw.Write((int)mem.Length);
                                        bw.Write(mem.ToArray());
                                        mem = new MemoryStream();
                                        king.Save(mem, ImageFormat.Png);
                                        bw.Write((int)mem.Length);
                                        bw.Write(mem.ToArray());
                                    }
                                }
                                bw.Close();
                                return;
                            }
                            catch(SocketException ex)
                            {
                                MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            catch(IOException ex)
                            {
                                MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            catch(InvalidOperationException ex)
                            {
                                MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            e.Cancel = true;
                            break;
                        }
                    }
                    else
                        break;
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the tabGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tabGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabGame.SelectedIndex == -1)
                return;

            CloseNetGame();
            switch(tabGame.SelectedIndex)
            {
                case 0:
                    cmbAgent1P.Select();
                    break;
                case 1:
                    txtPlayerName2P1.Select();
                    break;
                case 2:
                    lnkIPAddress.Text = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                    cmbNetGameType.Select();
                    break;
            }
        }

        #endregion

        #region Chat Event Handler Methods

        /// <summary>
        /// Handles the Popup event of the menuChat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChat_Popup(object sender, EventArgs e)
        {
            menuChatClear.Enabled = txtChat.TextLength > 0;
            menuChatCopy.Enabled = (txtChat.SelectionLength > 0);
        }

        /// <summary>
        /// Handles the Click event of the menuChatClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(this, "Clear the chat window?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            txtChat.Clear();
        }

        /// <summary>
        /// Handles the Click event of the menuChatCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtChat.Text, true);
        }

        /// <summary>
        /// Handles the Click event of the menuChatSelectAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChatSelectAll_Click(object sender, EventArgs e)
        {
            txtChat.SelectAll();
        }

        #endregion

        #region Image Event Handler Methods

        /// <summary>
        /// Handles the Click event of the menuImageDefault control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImageDefault_Click(object sender, System.EventArgs e)
        {
            SetDefaultImage();
        }

        /// <summary>
        /// Handles the Click event of the menuImageBrowse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImageBrowse_Click(object sender, System.EventArgs e)
        {
            BrowseCustomImage();
        }

        /// <summary>
        /// Handles the Click event of the menuImageChooseColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImageChooseColor_Click(object sender, System.EventArgs e)
        {
            ChooseCustomImageColor();
        }

        /// <summary>
        /// Handles the Click event of the menuImagePresetRed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImagePresetRed_Click(object sender, System.EventArgs e)
        {
            SetPresetImage(imlImageSet.Images[0], imlImageSet.Images[1], Color.Firebrick);
        }

        /// <summary>
        /// Handles the Click event of the menuImagePresetBlack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImagePresetBlack_Click(object sender, System.EventArgs e)
        {
            SetPresetImage(imlImageSet.Images[4], imlImageSet.Images[5], Color.DimGray);
        }

        /// <summary>
        /// Handles the Click event of the menuImagePresetWhite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImagePresetWhite_Click(object sender, System.EventArgs e)
        {
            SetPresetImage(imlImageSet.Images[2], imlImageSet.Images[3], Color.LightGray);
        }

        /// <summary>
        /// Handles the Click event of the menuImagePresetGold control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImagePresetGold_Click(object sender, System.EventArgs e)
        {
            SetPresetImage(imlImageSet.Images[6], imlImageSet.Images[7], Color.Gold);
        }

        /// <summary>
        /// Handles the Click event of the menuImagePreset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImagePreset_Click(object sender, System.EventArgs e)
        {
            // Load preset images
            string presetsPath = Path.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath) + "\\Presets");
            string presetPath = "";
            for(MenuItem item = (MenuItem)sender; item != menuImagePreset; item = (MenuItem)item.Parent)
                presetPath = "\\" + item.Text + presetPath;
            presetPath = presetsPath + presetPath;
            // Be sure preset still exists
            if(Directory.Exists(presetPath))
            {
                string pawnPreset = "", kingPreset = "";
                // Get the pawn and king filenames
                foreach(string file in Directory.GetFiles(presetPath))
                {
                    if(Path.GetFileNameWithoutExtension(file).ToLower() == "pawn")
                        pawnPreset = file;
                    if(Path.GetFileNameWithoutExtension(file).ToLower() == "king")
                        kingPreset = file;
                }
                if((pawnPreset != "") && (kingPreset != ""))
                {
                    Bitmap pawn = null, king = null;
                    try
                    {
                        // Load the preset image set
                        pawn = new Bitmap(Image.FromFile(pawnPreset));
                        king = new Bitmap(Image.FromFile(kingPreset));
                        // Set transparencies (if not transparent)
                        pawn.MakeTransparent();
                        king.MakeTransparent();
                        // Resize image set
                        pawn = new Bitmap(pawn, 32, 32);
                        king = new Bitmap(king, 32, 32);
                    }
                    catch(OutOfMemoryException)
                    {
                        MessageBox.Show(this, "One or both presets were not supported!", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // Set the preset image set
                    if((pawn != null) && (king != null))
                        SetPresetImage(pawn, king, Color.Empty);
                }
                else
                {
                    MessageBox.Show(this, "Preset not properly set up!\n\nPreset directories should contain:\nAn image file named 'Pawn' and\nAn image file named 'King'", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Preset not found!", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the picImageSwap1P control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void picImageSwap1P_Click(object sender, System.EventArgs e)
        {
            SwapImageSet(picPawn1P1, picPawn1P2);
            SwapImageSet(picKing1P1, picKing1P2);
        }

        /// <summary>
        /// Handles the Click event of the picImageSwap2P control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void picImageSwap2P_Click(object sender, System.EventArgs e)
        {
            SwapImageSet(picPawn2P1, picPawn2P2);
            SwapImageSet(picKing2P1, picKing2P2);
        }

        /// <summary>
        /// Handles the Click event of the picImageSwapNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void picImageSwapNet_Click(object sender, System.EventArgs e)
        {
            SwapImageSet(picPawnNet1, picPawnNet2);
            SwapImageSet(picKingNet1, picKingNet2);
            RefreshNetImages();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbImageSet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbImageSet_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SetImageSet((ComboBox)sender);
        }

        /// <summary>
        /// Handles the Click event of the picImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void picImage_Click(object sender, System.EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            selectedPicture = pictureBox;
            menuImage.Show(pictureBox.Parent, new Point(pictureBox.Left, pictureBox.Top + pictureBox.Height));
        }

        /// <summary>
        /// Handles the EnabledChanged event of the picImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void picImage_EnabledChanged(object sender, System.EventArgs e)
        {
            ((PictureBox)sender).BackColor = ((((PictureBox)sender).Enabled) ? (Color.White) : (Color.FromKnownColor(KnownColor.Control)));
        }

        /// <summary>
        /// Handles the MouseDown event of the picImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picImage_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            picImage_Click(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Popup event of the menuImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuImage_Popup(object sender, System.EventArgs e)
        {
            menuImageDefault.Visible = ((selectedPicture == picKing1P1) || (selectedPicture == picKing1P2) || (selectedPicture == picKing2P1) || (selectedPicture == picKing2P2) || (selectedPicture == picKingNet1) || (selectedPicture == picKingNet2));
            menuImageLine.Visible = menuImageDefault.Visible;
        }

        #endregion

        #region Network Game Event Handler Methods

        /// <summary>
        /// Handles the Tick event of the tmrConnection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmrConnection_Tick(object sender, EventArgs e)
        {
            CheckSockets();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbNetGameType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbNetGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbNetGameType.SelectedIndex)
            {
                case 0:
                    lblGamesNet.Text = "Recent Games:";
                    panConnectNet.Visible = true;
                    btnJoinNet.Enabled = true;
                    btnCreateNet.Enabled = true;
                    LoadRecentGames();
                    break;
                case 1:
                    panConnectNet.Visible = false;
                    btnJoinNet.Enabled = false;
                    btnCreateNet.Enabled = false;
                    lblGamesNet.Text = "Available Games:";
                    lstGamesNet.Items.Clear();
                    lstGamesNet.Items.Add("Not yet implemented; please use Internet TCP/IP");
                    // lstGamesNet.Items.Add("Looking for games...");
                    // !!!!! Use UDP to find a LAN game
                    break;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkIPAddress control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lnkIPAddress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetDataObject(lnkIPAddress.Text, true);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtRemoteHostNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtRemoteHostNet_TextChanged(object sender, EventArgs e)
        {
            panNet_Enter(panNet, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Enter event of the panNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void panNet_Enter(object sender, EventArgs e)
        {
            AcceptButton = ((txtRemoteHostNet.Text.Trim().Length == 0) ? (btnCreateNet) : (btnJoinNet));
        }

        /// <summary>
        /// Handles the Leave event of the panNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void panNet_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnOK;
        }

        /// <summary>
        /// Handles the Enter event of the panNetSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void panNetSettings_Enter(object sender, EventArgs e)
        {
            CancelButton = btnBackNet;
            btnBackNet.DialogResult = DialogResult.None;
        }

        /// <summary>
        /// Handles the Leave event of the panNetSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void panNetSettings_Leave(object sender, EventArgs e)
        {
            CancelButton = btnCancel;
            btnCancel.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the Enter event of the txtSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtSend_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnSend;
        }

        /// <summary>
        /// Handles the Leave event of the txtSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtSend_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnOK;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstGamesNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lstGamesNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((cmbNetGameType.SelectedIndex == 0) && (lstGamesNet.SelectedIndex != -1))
                txtRemoteHostNet.Text = lstGamesNet.SelectedItem.ToString();
        }

        /// <summary>
        /// Handles the Click event of the btnCreateNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCreateNet_Click(object sender, EventArgs e)
        {
            if(txtJoinNameNet.Text.Trim() == "")
            {
                MessageBox.Show(this, "Player name is required to play a Net Game.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtJoinNameNet.Select();
                return;
            }
            CloseNetSockets();
            // Start listening
            try
            {
                clientListener = new TcpListener(CheckersSettings.Port);
                clientListener.Start();
            }
            catch(SocketException ex)
            {
                CloseNetGame();
                MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            isSelfPlayer = true;
            txtPlayerNameNet.Text = txtJoinNameNet.Text.Trim();
            lstPlayersNet.CheckBoxes = true;
            txtGameNameNet.Text = txtPlayerNameNet.Text + "'s Game";
            lblImageNet1.Text = "You";
            lblImageNet2.Text = "Opponent";
            cmbFirstMoveNet.Items.Clear();
            lstPlayersNet.Items.Clear();
            cmbFirstMoveNet.Visible = true;
            txtFirstMoveNet.Visible = false;
            picPawnNet1.Enabled = true;
            picKingNet1.Enabled = true;
            picPawnNet2.Enabled = true;
            picKingNet2.Enabled = true;
            chkLockImagesNet.Visible = true;
            picImageSwapNet.Visible = true;
            // Save recent games and new player name
            SaveRecentGames();

            panNetSettings.Show();
            panNetSettings.BringToFront();
            panNet.Hide();
            AppendMessage("", "*** Game created");
            RefreshPlayerInfo();
            RefreshNetImages();
            tmrConnection.Start();
            txtSend.Select();
        }

        /// <summary>
        /// Handles the Click event of the btnJoinNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnJoinNet_Click(object sender, EventArgs e)
        {
            CloseNetSockets();

            switch(cmbNetGameType.SelectedIndex)
            {
                case 0:
                    if(txtRemoteHostNet.Text == "")
                    {
                        MessageBox.Show(this, "Remote host must not be empty", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemoteHostNet.Select();
                        return;
                    }

                    try
                    {
                        hostClient = new TcpClient(txtRemoteHostNet.Text, CheckersSettings.Port);
                    }
                    catch(SocketException ex)
                    {
                        MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemoteHostNet.SelectAll();
                        txtRemoteHostNet.Select();
                        return;
                    }
                    break;
                case 1:
                    // !!!!! Join found LAN game via TcpClient
                    break;
            }
            try
            {
                // Write data to host
                BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
                bw.Write((byte)ClientMessage.Header);
                bw.Write("CHECKERS".ToCharArray());
                bw.Write((byte)1);
                bw.Write(txtJoinNameNet.Text.Trim());
                bw.Close();
                // Wait for responds
            }
            catch(SocketException ex)
            {
                MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch(IOException ex)
            {
                MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            txtPlayerNameNet.Text = txtJoinNameNet.Text.Trim();
            lstPlayersNet.CheckBoxes = false;
            tmrConnection.Start();

            lstPlayersNet.Items.Clear();
            lblImageNet1.Text = "You";
            lblImageNet2.Text = "Opponent";
            cmbFirstMoveNet.Items.Clear();
            cmbFirstMoveNet.Visible = false;
            txtFirstMoveNet.Visible = true;
            picPawnNet1.Enabled = false;
            picKingNet1.Enabled = false;
            picPawnNet2.Enabled = false;
            picKingNet2.Enabled = false;
            chkLockImagesNet.Visible = false;
            picImageSwapNet.Visible = false;

            panNetSettings.Show();
            panNetSettings.BringToFront();
            panNet.Hide();
            AppendMessage("", "*** Entered room");
            txtSend.Select();
        }

        /// <summary>
        /// Handles the Click event of the btnBackNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBackNet_Click(object sender, EventArgs e)
        {
            CloseNetGame();
            cmbNetGameType_SelectedIndexChanged(cmbNetGameType, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of the btnSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if(txtSend.Text.Trim() == "")
                return;

            if(hostClient != null)
            {
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
                    bw.Write((byte)ClientMessage.ChatMessage);
                    bw.Write(txtPlayerNameNet.Text);
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
            }

            foreach(TcpClient client in clients)
            {
                if(client.Tag == null)
                    continue;
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
                    bw.Write((byte)ClientMessage.ChatMessage);
                    bw.Write(txtPlayerNameNet.Text);
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
            }

            AppendMessage(txtPlayerNameNet.Text, txtSend.Text);
            PlaySound(CheckersSounds.SendMessage);
            txtSend.Text = "";
            txtSend.Select();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbFirstMoveNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbFirstMoveNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFirstMoveNet.SelectedIndex == -1)
                return;
            if(clientListener != null)
            {
                RefreshPlayerInfo();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkLockImagesNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkLockImagesNet_CheckedChanged(object sender, EventArgs e)
        {
            if(clientListener != null)
                RefreshPlayerInfo();
        }

        /// <summary>
        /// Handles the ItemCheck event of the lstPlayersNet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ItemCheckEventArgs"/> instance containing the event data.</param>
        private void lstPlayersNet_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(clientListener == null)
                return;
            if(e.CurrentValue == e.NewValue)
                return;
            if(refreshingPlayerInfo)
                return;

            ListViewItem item = lstPlayersNet.Items[e.Index];
            bool newIsPlayer = (e.NewValue != CheckState.Unchecked);
            // Be sure there are still players to become available
            if(item.Text == txtPlayerNameNet.Text)
            {
                e.NewValue = CheckState.Checked;
                MessageBox.Show(this, "Host must be a player", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if((newIsPlayer == true) && (GetNetPlayerCount() >= 2))
            {
                e.NewValue = CheckState.Unchecked;
                MessageBox.Show(this, "Cannot have more than two players", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Update player info
            if(item.Text == txtPlayerNameNet.Text)
            {
                isSelfPlayer = newIsPlayer;
            }
            else
            {
                TcpClient client = GetClientByName(item.Text);
                if((client == null) || (client.Tag == null))
                    return;
                PlayerInfo info = (PlayerInfo)client.Tag;
                info.IsPlayer = (e.NewValue != CheckState.Unchecked);
                client.Tag = info;
            }
            // Refresh settings
            RefreshPlayerInfo();
        }

        #endregion

        #endregion

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="IWin32Window"/> that represents the top-level window that will own the modal dialog box.</param>
        /// <returns>One of the <see cref="DialogResult"/> values.</returns>
        /// <exception cref="ArgumentException">The form specified in the <paramref name="owner"/> parameter is the same as the form being shown.</exception>
        /// <exception cref="InvalidOperationException">The form being shown is already visible.-or-The form being shown is disabled.-or-The form being shown is not a top-level window.-or-The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive"/>).</exception>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            return OnShowDialog(owner);
        }

        #region Helper Methods

        #region Dialog Helper Methods

        /// <summary>
        /// Called when the dialog is shown.
        /// </summary>
        private DialogResult OnShowDialog(IWin32Window owner)
        {
            // Set control properties
            if((player1Name != "") && (player1Name != "Player") && (player1Name != "Player 1"))
                txtPlayerName1P.Text = txtPlayerName2P1.Text = txtJoinNameNet.Text = player1Name;
            if((player2Name != "") && (gameType == CheckersGameType.Multiplayer))
                txtPlayerName2P2.Text = player2Name;
            // Set net player name if not already set
            if(txtJoinNameNet.Text == "")
            {
                string fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\RecentNet.ini";
                StreamReader fs = new StreamReader((File.Exists(fileName)) ? (File.OpenRead(fileName)) : (File.Create(fileName)), Encoding.UTF8);
                txtJoinNameNet.Text = fs.ReadLine();
                fs.Close();
            }

            // Show dialog
            DialogResult result = base.ShowDialog(owner);

            // Set properties
            if(result != DialogResult.Cancel)
            {
                switch(tabGame.SelectedIndex)
                {
                    case 0:
                        gameType = CheckersGameType.SinglePlayer;
                        firstMove = ((cmbFirstMove1P.SelectedIndex == 0) ? (1) : (2));
                        agentIndex = ((cmbAgent1P.SelectedIndex == -1) ? (0) : (cmbAgent1P.SelectedIndex));
                        player1Name = ((txtPlayerName1P.Text.Trim() != "") ? (txtPlayerName1P.Text.Trim()) : ("Player"));
                        player2Name = "Opponent";
                        imageSet[0] = picPawn1P1.Image;
                        imageSet[1] = picKing1P1.Image;
                        imageSet[2] = picPawn1P2.Image;
                        imageSet[3] = picKing1P2.Image;
                        break;
                    case 1:
                        gameType = CheckersGameType.Multiplayer;
                        firstMove = ((cmbFirstMove2P.SelectedIndex == 0) ? (1) : (2));
                        agentIndex = 0;
                        player1Name = ((txtPlayerName2P1.Text.Trim() != "") ? (txtPlayerName2P1.Text.Trim()) : ("Player 1"));
                        player2Name = ((txtPlayerName2P2.Text.Trim() != "") ? (txtPlayerName2P2.Text.Trim()) : ("Player 2"));
                        imageSet[0] = picPawn2P1.Image;
                        imageSet[1] = picKing2P1.Image;
                        imageSet[2] = picPawn2P2.Image;
                        imageSet[3] = picKing2P2.Image;
                        break;
                    case 2:
                        gameType = CheckersGameType.NetGame;
                        agentIndex = 0;
                        // Client
                        if(remotePlayer != null)
                        {
                            try
                            {
                                BinaryReader br = new BinaryReader(new NetworkStream(remotePlayer.Socket, false));
                                // Player 1 info
                                player1Name = br.ReadString();
                                firstMove = ((br.ReadBoolean()) ? (1) : (2));
                                MemoryStream mem;
                                int len;
                                len = br.ReadInt32();
                                mem = new MemoryStream(br.ReadBytes(len));
                                imageSet[0] = Image.FromStream(mem);
                                len = br.ReadInt32();
                                mem = new MemoryStream(br.ReadBytes(len));
                                imageSet[1] = Image.FromStream(mem);
                                // Player 2 info
                                player2Name = br.ReadString();
                                len = br.ReadInt32();
                                mem = new MemoryStream(br.ReadBytes(len));
                                imageSet[2] = Image.FromStream(mem);
                                len = br.ReadInt32();
                                mem = new MemoryStream(br.ReadBytes(len));
                                imageSet[3] = Image.FromStream(mem);
                                br.Close();
                                break;
                            }
                            catch(SocketException ex)
                            {
                                MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            catch(IOException ex)
                            {
                                MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            catch(InvalidOperationException ex)
                            {
                                MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            gameType = CheckersGameType.None;
                            break;
                        }
                        // Server
                        if(GetNetPlayerCount() < 2)
                        {
                            gameType = CheckersGameType.None;
                            break;
                        }

                        // Set up server game
                        TcpClient client1 = null, client2 = null;
                        foreach(TcpClient client in clients)
                        {
                            if(client.Tag == null)
                                continue;
                            PlayerInfo info = ((PlayerInfo)client.Tag);
                            if(info.IsPlayer)
                            {
                                if(client1 == null)
                                    client1 = client;
                                else
                                    client2 = client;
                                continue;
                            }
                        }

                        // Two cases .. either server is a player or it is not
                        if(isSelfPlayer)
                        {
                            // Player 1 (self) info
                            player1Name = txtPlayerNameNet.Text;
                            firstMove = ((player1Name == cmbFirstMoveNet.Text) ? (1) : (2));
                            imageSet[0] = ((lblImageNet1.Text == player1Name) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                            imageSet[1] = ((lblImageNet1.Text == player1Name) ? (picKingNet1.Image) : (picKingNet2.Image));
                            // Player 2 info
                            PlayerInfo info = (PlayerInfo)client1.Tag;
                            player2Name = info.Name;
                            imageSet[2] = ((lblImageNet1.Text == player2Name) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                            imageSet[3] = ((lblImageNet1.Text == player2Name) ? (picKingNet1.Image) : (picKingNet2.Image));
                            remotePlayer = client1;
                            foreach(TcpClient client in clients)
                            {
                                if(client1 == client)
                                {
                                    clients.Remove(client);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // Get first player
                            if(((PlayerInfo)client1.Tag).Name != cmbFirstMoveNet.Text)
                            {
                                TcpClient temp = client1;
                                client1 = client2;
                                client2 = temp;
                            }
                            PlayerInfo info1 = (PlayerInfo)client1.Tag;
                            PlayerInfo info2 = (PlayerInfo)client2.Tag;
                            // Player 1 info
                            player1Name = info1.Name;
                            firstMove = ((player1Name == cmbFirstMoveNet.Text) ? (1) : (2));
                            imageSet[0] = ((lblImageNet1.Text == player1Name) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                            imageSet[1] = ((lblImageNet1.Text == player1Name) ? (picKingNet1.Image) : (picKingNet2.Image));
                            // Player 2 info
                            player1Name = info2.Name;
                            imageSet[2] = ((lblImageNet1.Text == player2Name) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                            imageSet[3] = ((lblImageNet1.Text == player2Name) ? (picKingNet1.Image) : (picKingNet2.Image));
                        }
                        break;
                    default:
                        MessageBox.Show(this, "New Game Dialog exited in an unrecognized tab; no settings were set", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        gameType = CheckersGameType.None;
                        break;
                }
            }
            CloseNetGame();
            return result;
        }

        /// <summary>
        /// Loads the list of recent games.
        /// </summary>
        private void LoadRecentGames()
        {
            lstGamesNet.Items.Clear();
            // Load recent games list
            string fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\RecentNet.ini";
            StreamReader fs = new StreamReader((File.Exists(fileName)) ? (File.OpenRead(fileName)) : (File.Create(fileName)), Encoding.UTF8);
            fs.ReadLine();          // Read player name
            while(fs.Peek() != -1)
            {
                string line = fs.ReadLine().Trim();
                if(line == "")
                    continue;
                lstGamesNet.Items.Add(line);
            }
            fs.Close();
        }

        /// <summary>
        /// Saves the list of recent games.
        /// </summary>
        private void SaveRecentGames()
        {
            if(cmbNetGameType.SelectedIndex == 1)
                return;
            // Save recent games and player name
            string fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\RecentNet.ini";
            StreamWriter fs = File.CreateText(fileName);
            fs.WriteLine(txtPlayerNameNet.Text);
            bool inList = false;
            // Append list of hosts
            string remoteHost = txtRemoteHostNet.Text.Trim();
            foreach(string item in lstGamesNet.Items)
            {
                if(remoteHost == item)
                    inList = true;
                fs.WriteLine(item);
            }
            if((!inList) && (txtRemoteHostNet.Text != ""))
                fs.WriteLine(remoteHost);
            fs.Close();
        }

        #endregion

        #region Image Helper Functions

        private void OnCustomImageSet()
        {
            if((selectedPicture == picPawn1P1) || (selectedPicture == picKing1P1) || (selectedPicture == picPawn1P2) || (selectedPicture == picKing1P2))
                cmbImageSet1P.SelectedIndex = 3;
            else if((selectedPicture == picPawn2P1) || (selectedPicture == picKing2P1) || (selectedPicture == picPawn2P2) || (selectedPicture == picKing2P2))
                cmbImageSet2P.SelectedIndex = 3;
        }

        private void SwapImageSet(PictureBox pic1, PictureBox pic2)
        {
            Image temp = pic1.Image;
            pic1.Image = pic2.Image;
            pic2.Image = temp;
            object tag = pic1.Tag;
            pic1.Tag = pic2.Tag;
            pic2.Tag = tag;
        }

        private void SetImageSet(ComboBox sender)
        {
            if(sender == cmbImageSet1P)
                SetImageSet(cmbImageSet1P.SelectedIndex, picPawn1P1, picKing1P1, picPawn1P2, picKing1P2);
            else if(sender == cmbImageSet2P)
                SetImageSet(cmbImageSet2P.SelectedIndex, picPawn2P1, picKing2P1, picPawn2P2, picKing2P2);
        }

        private void SetImageSet(int setIndex, PictureBox pawnPic1, PictureBox kingPic1, PictureBox pawnPic2, PictureBox kingPic2)
        {
            switch(setIndex)
            {
                case 0:
                    pawnPic1.Image = imlImageSet.Images[0];
                    pawnPic1.Tag = Color.Firebrick;
                    kingPic1.Image = imlImageSet.Images[1];
                    kingPic1.Tag = kingPic1.Image;
                    pawnPic2.Image = imlImageSet.Images[2];
                    pawnPic1.Tag = Color.LightGray;
                    kingPic2.Image = imlImageSet.Images[3];
                    kingPic2.Tag = kingPic2.Image;
                    kingPic1.Visible = false;
                    kingPic2.Visible = false;
                    break;
                case 1:
                    pawnPic1.Image = imlImageSet.Images[4];
                    pawnPic1.Tag = Color.DimGray;
                    kingPic1.Image = imlImageSet.Images[5];
                    kingPic1.Tag = kingPic1.Image;
                    pawnPic2.Image = imlImageSet.Images[2];
                    pawnPic1.Tag = Color.LightGray;
                    kingPic2.Image = imlImageSet.Images[3];
                    kingPic2.Tag = kingPic2.Image;
                    kingPic1.Visible = false;
                    kingPic2.Visible = false;
                    break;
                case 2:
                    pawnPic1.Image = imlImageSet.Images[8];
                    pawnPic1.Tag = Color.Firebrick;
                    kingPic1.Image = imlImageSet.Images[9];
                    kingPic1.Tag = kingPic1.Image;
                    pawnPic2.Image = imlImageSet.Images[10];
                    pawnPic1.Tag = Color.LightGray;
                    kingPic2.Image = imlImageSet.Images[11];
                    kingPic2.Tag = kingPic2.Image;
                    kingPic1.Visible = false;
                    kingPic2.Visible = false;
                    break;
                default:
                    kingPic1.Visible = true;
                    kingPic2.Visible = true;
                    break;
            }
        }

        private void SetDefaultImage()
        {
            if(selectedPicture.Tag == null)
                return;
            if(selectedPicture == picKing1P1)
                picKing1P1.Image = (Image)picKing1P1.Tag;
            else if(selectedPicture == picKing1P2)
                picKing1P2.Image = (Image)picKing1P2.Tag;
            else if(selectedPicture == picKing2P1)
                picKing2P1.Image = (Image)picKing2P1.Tag;
            else if(selectedPicture == picKing2P2)
                picKing2P2.Image = (Image)picKing2P2.Tag;
            else if(selectedPicture == picKingNet1)
            {
                picKingNet1.Image = (Image)picKingNet1.Tag;
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet2)
            {
                picKingNet2.Image = (Image)picKingNet2.Tag;
                RefreshNetImages();
            }
            OnCustomImageSet();
        }

        private void BrowseCustomImage()
        {
            if(selectedPicture == picPawn1P1)
                BrowseCustomPawn(picPawn1P1, picKing1P1);
            else if(selectedPicture == picKing1P1)
                BrowseCustomKing(picKing1P1);
            else if(selectedPicture == picPawn1P2)
                BrowseCustomPawn(picPawn1P2, picKing1P2);
            else if(selectedPicture == picKing1P2)
                BrowseCustomKing(picKing1P2);
            else if(selectedPicture == picPawn2P1)
                BrowseCustomPawn(picPawn2P1, picKing2P1);
            else if(selectedPicture == picKing2P1)
                BrowseCustomKing(picKing2P1);
            else if(selectedPicture == picPawn2P2)
                BrowseCustomPawn(picPawn2P2, picKing2P2);
            else if(selectedPicture == picKing2P2)
                BrowseCustomKing(picKing2P2);
            else if(selectedPicture == picPawnNet1)
            {
                BrowseCustomPawn(picPawnNet1, picKingNet1);
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet1)
            {
                BrowseCustomKing(picKingNet1);
                RefreshNetImages();
            }
            else if(selectedPicture == picPawnNet2)
            {
                BrowseCustomPawn(picPawnNet2, picKingNet2);
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet2)
            {
                BrowseCustomKing(picKingNet2);
                RefreshNetImages();
            }
            OnCustomImageSet();
        }

        private void BrowseCustomPawn(PictureBox pawnPic, PictureBox kingPic)
        {
            // Open pawn image
            dlgOpenImage.Title = "Open Custom Pawn Image";
            if(dlgOpenImage.ShowDialog(this) == DialogResult.Cancel)
                return;
            string pawnFileName = dlgOpenImage.FileName;
            // Create piece images
            Bitmap pawn = new Bitmap(Image.FromFile(pawnFileName));
            Bitmap king = new Bitmap(Image.FromFile(pawnFileName));
            // Set transparencies (if not transparent)
            pawn.MakeTransparent();
            king.MakeTransparent();
            // Resize image set
            pawn = new Bitmap(pawn, 32, 32);
            king = new Bitmap(king, 32, 32);
            // Draw king icon on the king
            DrawKingIcon(king);
            // Set custom pawn.king images
            pawnPic.Image = pawn;
            pawnPic.Tag = null;
            kingPic.Image = king;
            kingPic.Tag = kingPic.Image;
        }

        private void BrowseCustomKing(PictureBox kingPic)
        {
            // Open king image
            dlgOpenImage.Title = "Open Custom King Image";
            if(dlgOpenImage.ShowDialog(this) == DialogResult.Cancel)
                return;
            string kingFileName = dlgOpenImage.FileName;
            // Create piece images
            Bitmap king = new Bitmap(Image.FromFile(kingFileName));
            // Set transparencies (if not transparent)
            king.MakeTransparent();
            // Resize image set
            king = new Bitmap(king, 32, 32);
            // Set custom pawn.king images
            kingPic.Image = king;
        }

        private void ChooseCustomImageColor()
        {
            if(selectedPicture == picPawn1P1)
                ChooseCustomPawnColor(picPawn1P1, picKing1P1);
            else if(selectedPicture == picKing1P1)
                ChooseCustomKingColor(picPawn1P1, picKing1P1);
            else if(selectedPicture == picPawn1P2)
                ChooseCustomPawnColor(picPawn1P2, picKing1P2);
            else if(selectedPicture == picKing1P2)
                ChooseCustomKingColor(picPawn1P1, picKing1P2);
            else if(selectedPicture == picPawn2P1)
                ChooseCustomPawnColor(picPawn2P1, picKing2P1);
            else if(selectedPicture == picKing2P1)
                ChooseCustomKingColor(picPawn2P1, picKing2P1);
            else if(selectedPicture == picPawn2P2)
                ChooseCustomPawnColor(picPawn2P2, picKing2P2);
            else if(selectedPicture == picKing2P2)
                ChooseCustomKingColor(picPawn2P1, picKing2P2);
            else if(selectedPicture == picPawnNet1)
            {
                ChooseCustomPawnColor(picPawnNet1, picKingNet1);
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet1)
            {
                ChooseCustomKingColor(picPawnNet1, picKingNet1);
                RefreshNetImages();
            }
            else if(selectedPicture == picPawnNet2)
            {
                ChooseCustomPawnColor(picPawnNet2, picKingNet2);
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet2)
            {
                ChooseCustomKingColor(picPawnNet1, picKingNet2);
                RefreshNetImages();
            }
            OnCustomImageSet();
        }

        private void ChooseCustomPawnColor(PictureBox pawnPic, PictureBox kingPic)
        {
            dlgSelectColor.Color = ((pawnPic.Tag != null) ? ((Color)pawnPic.Tag) : (Color.Gray));
            if(dlgSelectColor.ShowDialog(this) == DialogResult.Cancel)
                return;
            pawnPic.Image = CreatePieceImage(dlgSelectColor.Color, false);
            pawnPic.Tag = dlgSelectColor.Color;
            kingPic.Image = CreatePieceImage(dlgSelectColor.Color, true);
            kingPic.Tag = kingPic.Image;
        }

        private void ChooseCustomKingColor(PictureBox pawnPic, PictureBox kingPic)
        {
            dlgSelectColor.Color = ((pawnPic.Tag != null) ? ((Color)pawnPic.Tag) : (Color.Gray));
            if(dlgSelectColor.ShowDialog(this) == DialogResult.Cancel)
                return;
            kingPic.Image = CreatePieceImage(dlgSelectColor.Color, true);
        }

        private void SetPresetImage(Image pawn, Image king, Color color)
        {
            if(selectedPicture == picPawn1P1)
                SetPresetPawn(picPawn1P1, picKing1P1, pawn, king, color);
            else if(selectedPicture == picKing1P1)
                SetPresetKing(picKing1P1, king);
            else if(selectedPicture == picPawn1P2)
                SetPresetPawn(picPawn1P2, picKing1P2, pawn, king, color);
            else if(selectedPicture == picKing1P2)
                SetPresetKing(picKing1P2, king);
            else if(selectedPicture == picPawn2P1)
                SetPresetPawn(picPawn2P1, picKing2P1, pawn, king, color);
            else if(selectedPicture == picKing2P1)
                SetPresetKing(picKing2P1, king);
            else if(selectedPicture == picPawn2P2)
                SetPresetPawn(picPawn2P2, picKing2P2, pawn, king, color);
            else if(selectedPicture == picKing2P2)
                SetPresetKing(picKing2P2, king);
            else if(selectedPicture == picPawnNet1)
            {
                SetPresetPawn(picPawnNet1, picKingNet1, pawn, king, color);
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet1)
            {
                SetPresetKing(picKingNet1, king);
                RefreshNetImages();
            }
            else if(selectedPicture == picPawnNet2)
            {
                SetPresetPawn(picPawnNet2, picKingNet2, pawn, king, color);
                RefreshNetImages();
            }
            else if(selectedPicture == picKingNet2)
            {
                SetPresetKing(picKingNet2, king);
                RefreshNetImages();
            }
            OnCustomImageSet();
        }

        private void SetPresetPawn(PictureBox pawnPic, PictureBox kingPic, Image pawn, Image king, Color color)
        {
            pawnPic.Image = pawn;
            pawnPic.Tag = ((color.IsEmpty) ? (null) : ((object)color));
            kingPic.Image = king;
            kingPic.Tag = kingPic.Image;
        }

        private void SetPresetKing(PictureBox kingPic, Image king)
        {
            kingPic.Image = king;
        }

        private void EnumPresetMenuItems()
        {
            menuImagePresetsNone.Visible = (!EnumPresetMenuItems(menuImagePreset, ""));
        }

        private bool EnumPresetMenuItems(MenuItem item, string subPath)
        {
            string presetsPath = Path.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath) + "\\Presets" + subPath);
            if(!Directory.Exists(presetsPath))
                return false;

            // Enumerate presets into the menu
            string[] presetPathList = Directory.GetDirectories(presetsPath);
            // Add sub-categories
            foreach(string presetPath in presetPathList)
            {
                if(Directory.GetDirectories(presetPath).Length > 0)
                    EnumPresetMenuItems(item.MenuItems.Add(Path.GetFileName(presetPath)), subPath + "\\" + Path.GetFileName(presetPath));
            }
            // Add presets
            foreach(string presetPath in presetPathList)
            {
                if(Directory.GetDirectories(presetPath).Length == 0)
                    item.MenuItems.Add(Path.GetFileName(presetPath), new EventHandler(menuImagePreset_Click));
            }
            return (presetPathList.Length != 0);
        }

        private Bitmap CreatePieceImage(Color color, bool isKing)
        {
            Bitmap pieceImage = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(pieceImage);
            Brush fillBrush = new SolidBrush(color);
            Pen ringColor = new Pen(Color.FromArgb(((color.R + 0x28 > 0xFF) ? (0xFF) : (color.R + 0x28)), ((color.G + 0x28 > 0xFF) ? (0xFF) : (color.G + 0x28)), ((color.B + 0x28 > 0xFF) ? (0xFF) : (color.B + 0x28))));
            g.FillEllipse(fillBrush, 2, 2, 32 - 5, 32 - 5);
            g.DrawEllipse(ringColor, 3, 3, 32 - 7, 32 - 7);
            g.DrawEllipse(Pens.Black, 2, 2, 32 - 5, 32 - 5);
            if(isKing)
                DrawKingIcon(g);
            ringColor.Dispose();
            fillBrush.Dispose();
            g.Dispose();
            return pieceImage;
        }

        private void DrawKingIcon(Image image)
        {
            Graphics g = Graphics.FromImage(image);
            DrawKingIcon(g);
            g.Dispose();
        }

        private void DrawKingIcon(Graphics g)
        {
            g.DrawImage(imlKing.Images[0], 0, 0, 32, 32);
        }

        #endregion

        #region Socket Helper Functions

        private void CheckSockets()
        {
            if(checkingSockets)
                return;
            checkingSockets = true;

            // Check sockets for as long as there is data
            bool doCheck = true;
            while(doCheck)
            {
                doCheck = false;
                if(clients.Count > 0)
                {
                    for(int i = 0; i < clients.Count; i++)
                    {
                        string closedName = "", message = "";
                        try
                        {
                            if(CheckClientForMessage(clients[i]))
                                doCheck = true;
                            continue;
                        }
                        catch(IOException)
                        {
                            closedName = ((clients[i].Tag != null) ? (((PlayerInfo)clients[i].Tag).Name) : (""));
                            message = "";
                        }
                        catch(SocketException ex)
                        {
                            closedName = ((clients[i].Tag != null) ? (((PlayerInfo)clients[i].Tag).Name) : (""));
                            message = closedName + " disconnected unexpectedly:\n\n" + ex.Message;
                        }
                        catch(InvalidOperationException ex)
                        {
                            closedName = ((clients[i].Tag != null) ? (((PlayerInfo)clients[i].Tag).Name) : (""));
                            message = closedName + " disconnected unexpectedly:\n\n" + ex.Message;
                        }
                        clients[i].Close();
                        clients.RemoveAt(i);
                        i--;
                        if(closedName != "")
                            BroadcastMessage("", "*** " + closedName + " has left the room", null);
                        RefreshPlayerInfo();
                        if(message != "")
                            MessageBox.Show(this, message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if(hostClient != null)
                {
                    try
                    {
                        if(CheckHostForMessage())
                            doCheck = true;
                    }
                    catch(SocketException ex)
                    {
                        CloseNetGame();
                        MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch(IOException ex)
                    {
                        CloseNetGame();
                        MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch(InvalidOperationException ex)
                    {
                        CloseNetGame();
                        MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if(clientListener != null)
                {
                    if(clientListener.Pending())
                    {
                        // Add to clients, in an uninitialized state (tag = null)
                        clients.Add(new TcpClient(clientListener.AcceptSocket()));
                    }
                }
                Application.DoEvents();
            }

            checkingSockets = false;
        }

        private bool CheckHostForMessage()
        {
            NetworkStream ns = new NetworkStream(hostClient.Socket, false);
            if(!ns.DataAvailable)
                return false;
            BinaryReader br = new BinaryReader(ns);
            BinaryWriter bw = new BinaryWriter(ns);
            switch((ClientMessage)br.ReadByte())
            {
                case ClientMessage.Header:
                    string hostName = br.ReadString();
                    txtGameNameNet.Text = hostName + "'s Game";
                    SaveRecentGames();
                    break;
                case ClientMessage.Closed:
                    int closeReason = -1;
                    if(ns.DataAvailable)
                        closeReason = (int)br.ReadByte();
                    CloseNetGame();
                    // Show reason for close
                    if(true)     // Workaround Visual Studio's text reformat visual error
                    {
                        switch(closeReason)
                        {
                            case 0:
                                MessageBox.Show(this, "Disconnected by remote host", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case 1:
                                MessageBox.Show(this, "Checkers versions do not match", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case 2:
                                MessageBox.Show(this, "Name is already in use", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            default:
                                MessageBox.Show(this, "Unexpectedly disconnected from host:\n\nDisconnect message sent from host without a reason", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                        }
                    }
                    break;
                case ClientMessage.RefreshPlayerInfo:
                    isSelfPlayer = false;
                    lstPlayersNet.BeginUpdate();
                    lstPlayersNet.Items.Clear();
                    int count = 0;
                    string playerName1 = "", playerName2 = "";
                    while(br.ReadBoolean())
                    {
                        PlayerInfo info = new PlayerInfo();
                        info.Name = br.ReadString();
                        info.IsPlayer = br.ReadBoolean();
                        ListViewItem item = new ListViewItem(new string[] { info.Name, ((info.IsPlayer) ? ("Player") : (((count == 0) ? ("Dedicated Server") : ("--")))) });
                        lstPlayersNet.Items.Add(item);
                        // Get player name
                        if(info.IsPlayer)
                        {
                            if(playerName1 == "")
                                playerName1 = info.Name;
                            else
                                playerName2 = info.Name;
                        }
                        // See if self is playing
                        if((info.IsPlayer) && (info.Name == txtPlayerNameNet.Text))
                            isSelfPlayer = true;
                        count++;
                    }
                    lstPlayersNet.EndUpdate();
                    txtFirstMoveNet.Text = br.ReadString();
                    if(isSelfPlayer)
                        panImageSetNet.Enabled = true;
                    bool lockedImages = br.ReadBoolean();

                    // Decide who is displayed first
                    if(playerName2 == txtFirstMoveNet.Text)
                    {
                        string temp = playerName1;
                        playerName1 = playerName2;
                        playerName2 = temp;
                    }

                    // Show correct Custom Images order and enable if player
                    lblImageNet1.Text = playerName1;
                    lblImageNet2.Text = playerName2;
                    picPawnNet1.Enabled = picKingNet1.Enabled = ((!lockedImages) && (isSelfPlayer) && (playerName1 == txtPlayerNameNet.Text));
                    picPawnNet2.Enabled = picKingNet2.Enabled = ((!lockedImages) && (isSelfPlayer) && (playerName2 == txtPlayerNameNet.Text));
                    break;
                case ClientMessage.RefreshNetImages:
                    MemoryStream mem;
                    int len;
                    len = br.ReadInt32();
                    mem = new MemoryStream(br.ReadBytes(len));
                    picPawnNet1.Image = Image.FromStream(mem);
                    len = br.ReadInt32();
                    mem = new MemoryStream(br.ReadBytes(len));
                    picKingNet1.Image = Image.FromStream(mem);
                    len = br.ReadInt32();
                    mem = new MemoryStream(br.ReadBytes(len));
                    picPawnNet2.Image = Image.FromStream(mem);
                    len = br.ReadInt32();
                    mem = new MemoryStream(br.ReadBytes(len));
                    picKingNet2.Image = Image.FromStream(mem);
                    break;
                case ClientMessage.ChatMessage:
                    string name = br.ReadString();
                    string message = br.ReadString();
                    AppendMessage(name, message);
                    PlaySound(CheckersSounds.ReceiveMessage);
                    break;
                case ClientMessage.BeginGame:
                    // Attact to client and close the form
                    if(!br.ReadBoolean())
                        throw new IOException("Game started; disconnected from host");
                    remotePlayer = hostClient;
                    hostClient = null;
                    // Close the form
                    DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                default:
                    throw new IOException("An unknown message was received");
            }
            br.Close();
            bw.Close();
            return true;
        }

        private bool CheckClientForMessage(TcpClient client)
        {
            NetworkStream ns = new NetworkStream(client.Socket, false);
            if(!ns.DataAvailable)
                return false;
            BinaryReader br = new BinaryReader(ns);
            BinaryWriter bw = new BinaryWriter(ns);
            PlayerInfo info;
            switch((ClientMessage)br.ReadByte())
            {
                case ClientMessage.Header:
                    if(client.Tag != null)
                        throw new IOException("A bad message was received");
                    if(!((new string(br.ReadChars(8)) == "CHECKERS") && (br.ReadByte() == 1)))
                    {
                        bw.Write((byte)ClientMessage.Closed);
                        bw.Write((byte)1);
                        bw.Close();
                        throw new IOException();
                    }
                    info = new PlayerInfo();
                    info.Name = br.ReadString().Trim();
                    bool validName = (txtPlayerNameNet.Text.ToLower() != info.Name.ToLower());
                    foreach(TcpClient checkClient in clients)
                    {
                        if((checkClient.Tag != null) && ((PlayerInfo)checkClient.Tag).Name.ToLower() == info.Name.ToLower())
                        {
                            validName = false;
                            break;
                        }
                    }
                    if(!validName)
                    {
                        bw.Write((byte)ClientMessage.Closed);
                        bw.Write((byte)2);
                        bw.Close();
                        throw new IOException();
                    }
                    info.IsPlayer = (GetClientCount() == 0);
                    client.Tag = info;
                    // Broadcast message and refresh the list
                    BroadcastMessage("", "*** " + info.Name + " has entered the room", client);
                    // Write header
                    bw.Write((byte)ClientMessage.Header);
                    bw.Write(txtPlayerNameNet.Text.Trim());
                    // Refresh the list
                    RefreshPlayerInfo();
                    RefreshNetImages();
                    break;
                case ClientMessage.Closed:
                    throw new IOException();
                case ClientMessage.RefreshPlayerInfo:
                    throw new IOException("A wrong message was received");
                case ClientMessage.RefreshNetImages:
                    if(client.Tag == null)
                        throw new IOException("A wrong message was received");
                    info = ((PlayerInfo)client.Tag);
                    Image pawn, king;
                    MemoryStream mem;
                    int len;
                    len = br.ReadInt32();
                    mem = new MemoryStream(br.ReadBytes(len));
                    pawn = Image.FromStream(mem);
                    len = br.ReadInt32();
                    mem = new MemoryStream(br.ReadBytes(len));
                    king = Image.FromStream(mem);
                    if((!chkLockImagesNet.Checked) && (info.IsPlayer))
                    {
                        if(info.Name == cmbFirstMoveNet.Text)
                        {
                            picPawnNet1.Image = pawn;
                            picKingNet1.Image = king;
                            picKingNet1.Tag = king;
                        }
                        else
                        {
                            picPawnNet2.Image = pawn;
                            picKingNet2.Image = king;
                            picKingNet2.Tag = king;
                        }
                    }
                    RefreshNetImages();
                    break;
                case ClientMessage.ChatMessage:
                    string name = br.ReadString();
                    string message = br.ReadString();
                    BroadcastMessage(name, message, client);
                    PlaySound(CheckersSounds.ReceiveMessage);
                    break;
                case ClientMessage.BeginGame:
                    throw new IOException("A wrong message was received");
                default:
                    throw new IOException("An unknown message was received");
            }
            br.Close();
            bw.Close();
            return true;
        }

        #endregion

        #region Network Game Helper Functions

        private int GetNetPlayerCount()
        {
            int playerCount = 0;
            if(isSelfPlayer)
                playerCount++;
            foreach(TcpClient client in clients)
            {
                if((client.Tag != null) && (((PlayerInfo)client.Tag).IsPlayer))
                    playerCount++;
            }
            return playerCount;
        }

        private int GetClientCount()
        {
            int count = 0;
            foreach(TcpClient client in clients)
            {
                if(client.Tag != null)
                    count++;
            }
            return count;
        }

        private TcpClient GetClientByName(string name)
        {
            foreach(TcpClient client in clients)
            {
                if((client.Tag != null) && (((PlayerInfo)client.Tag).Name == name))
                    return client;
            }
            return null;
        }

        private string GetPlayer1Name()
        {
            if((isSelfPlayer) && (txtPlayerNameNet.Text == cmbFirstMoveNet.Text))
                return txtPlayerNameNet.Text;
            foreach(TcpClient client in clients)
            {
                if((client.Tag == null) || (!((PlayerInfo)client.Tag).IsPlayer))
                    continue;
                if(((PlayerInfo)client.Tag).Name == cmbFirstMoveNet.Text)
                    return ((PlayerInfo)client.Tag).Name;
            }
            return "";
        }

        private string GetPlayer2Name()
        {
            if((isSelfPlayer) && (txtPlayerNameNet.Text != cmbFirstMoveNet.Text))
                return txtPlayerNameNet.Text;
            foreach(TcpClient client in clients)
            {
                if((client.Tag == null) || (!((PlayerInfo)client.Tag).IsPlayer))
                    continue;
                if(((PlayerInfo)client.Tag).Name != cmbFirstMoveNet.Text)
                    return ((PlayerInfo)client.Tag).Name;
            }
            return "";
        }

        private void BroadcastMessage(string name, string message, TcpClient exceptClient)
        {
            AppendMessage(name, message);
            foreach(TcpClient client in clients)
            {
                if((exceptClient != null) && (client == exceptClient))
                    continue;          // Do not send message to sender
                if(client.Tag == null)
                    continue;
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
                    bw.Write((byte)ClientMessage.ChatMessage);
                    bw.Write(name);
                    bw.Write(message);
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
            }
        }

        private void AppendMessage(string name, string message)
        {
            txtChat.Select(txtChat.TextLength, 0);
            if(name == "")
            {
                txtChat.SelectionColor = Color.Red;
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
            }
            else if(name == txtPlayerNameNet.Text)
            {
                txtChat.SelectionColor = Color.Red;
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
                txtChat.AppendText(" " + name + ": ");
                txtChat.SelectionColor = Color.Black;
            }
            else
            {
                txtChat.SelectionColor = Color.Blue;
                txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
                txtChat.AppendText(" " + name + ": ");
                txtChat.SelectionColor = Color.Black;
            }
            txtChat.AppendText(message);
            Control activeControl = ActiveControl;
            txtChat.AppendText("\n");
            txtChat.ScrollToCaret();
            int min = 0, max = 0;
            GetScrollRange(txtChat.Handle, 1, ref min, ref max);
            SendMessage(txtChat.Handle, EM_SETSCROLLPOS, 0, new Win32Point(0, max - txtChat.Height));
        }

        private void RefreshPlayerInfo()
        {
            if(clientListener == null)
                return;
            if(refreshingPlayerInfo)
                return;
            refreshingPlayerInfo = true;

            string sFirstMove = cmbFirstMoveNet.Text;
            lstPlayersNet.Items.Clear();
            cmbFirstMoveNet.Items.Clear();
            ListViewItem item = new ListViewItem(new string[] { txtPlayerNameNet.Text, (isSelfPlayer) ? ("Player") : ("--") });
            item.Checked = isSelfPlayer;
            if(isSelfPlayer)
                cmbFirstMoveNet.Items.Add(txtPlayerNameNet.Text);
            lstPlayersNet.Items.Add(item);

            // Get data from client info
            for(int i = 0; i < clients.Count; i++)
            {
                TcpClient client = clients[i];
                if(client.Tag == null)
                    continue;
                PlayerInfo info = (PlayerInfo)client.Tag;
                item = new ListViewItem(new string[] { info.Name, (info.IsPlayer) ? ("Player") : ("--") });
                item.Checked = info.IsPlayer;
                if(info.IsPlayer)
                    cmbFirstMoveNet.Items.Add(info.Name);
                lstPlayersNet.Items.Add(item);
            }
            cmbFirstMoveNet.SelectedIndex = cmbFirstMoveNet.Items.IndexOf(sFirstMove);
            if((cmbFirstMoveNet.SelectedIndex == -1) && (cmbFirstMoveNet.Items.Count > 0))
                cmbFirstMoveNet.SelectedIndex = 0;

            lblImageNet1.Text = GetPlayer1Name();
            lblImageNet2.Text = GetPlayer2Name();

            // Write data to clients
            for(int i = 0; i < clients.Count; i++)
            {
                TcpClient client = clients[i];
                if(client.Tag == null)
                    continue;
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
                    bw.Write((byte)ClientMessage.RefreshPlayerInfo);
                    bw.Write(true);
                    bw.Write(txtPlayerNameNet.Text);
                    bw.Write(isSelfPlayer);
                    for(int n = 0; n < clients.Count; n++)
                    {
                        if(clients[n].Tag == null)
                            continue;
                        PlayerInfo info = (PlayerInfo)clients[n].Tag;
                        bw.Write(true);
                        bw.Write(info.Name);
                        bw.Write(info.IsPlayer);
                    }
                    bw.Write(false);
                    bw.Write(cmbFirstMoveNet.Text);
                    bw.Write(chkLockImagesNet.Checked);
                    continue;
                }
                catch(SocketException ex)
                {
                    MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch(IOException ex)
                {
                    MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch(InvalidOperationException ex)
                {
                    MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                client.Close();
                clients.Remove(client);
                i--;
            }
            refreshingPlayerInfo = false;
        }

        private void RefreshNetImages()
        {
            if(hostClient != null)
            {
                Image pawn = ((lblImageNet1.Text == txtPlayerNameNet.Text) ? (picPawnNet1.Image) : (picPawnNet2.Image));
                Image king = ((lblImageNet1.Text == txtPlayerNameNet.Text) ? (picKingNet1.Image) : (picKingNet2.Image));
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
                    bw.Write((byte)ClientMessage.RefreshNetImages);
                    MemoryStream mem;
                    mem = new MemoryStream();
                    pawn.Save(mem, ImageFormat.Png);
                    bw.Write((int)mem.Length);
                    bw.Write(mem.ToArray());
                    mem = new MemoryStream();
                    king.Save(mem, ImageFormat.Png);
                    bw.Write((int)mem.Length);
                    bw.Write(mem.ToArray());
                    bw.Close();
                    return;
                }
                catch(SocketException ex)
                {
                    CloseNetGame();
                    MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch(IOException ex)
                {
                    CloseNetGame();
                    MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch(InvalidOperationException ex)
                {
                    CloseNetGame();
                    MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }

            if(clientListener == null)
                return;
            // Write data to clients
            for(int i = 0; i < clients.Count; i++)
            {
                TcpClient client = clients[i];
                if(client.Tag == null)
                    continue;
                PlayerInfo info = (PlayerInfo)client.Tag;
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
                    bw.Write((byte)ClientMessage.RefreshNetImages);
                    MemoryStream mem;
                    mem = new MemoryStream();
                    picPawnNet1.Image.Save(mem, ImageFormat.Png);
                    bw.Write((int)mem.Length);
                    bw.Write(mem.ToArray());
                    mem = new MemoryStream();
                    picKingNet1.Image.Save(mem, ImageFormat.Png);
                    bw.Write((int)mem.Length);
                    bw.Write(mem.ToArray());
                    mem = new MemoryStream();
                    picPawnNet2.Image.Save(mem, ImageFormat.Png);
                    bw.Write((int)mem.Length);
                    bw.Write(mem.ToArray());
                    mem = new MemoryStream();
                    picKingNet2.Image.Save(mem, ImageFormat.Png);
                    bw.Write((int)mem.Length);
                    bw.Write(mem.ToArray());
                    bw.Close();
                    continue;
                }
                catch(SocketException ex)
                {
                    MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch(IOException ex)
                {
                    MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch(InvalidOperationException ex)
                {
                    MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                client.Close();
                clients.Remove(client);
                i--;
            }
        }

        private void CloseNetSockets()
        {
            tmrConnection.Stop();
            checkingSockets = false;
            if(hostClient != null)
            {
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
                    bw.Write((byte)ClientMessage.Closed);
                    bw.Close();
                }
                catch(SocketException)
                {
                }
                catch(IOException)
                {
                }
                catch(InvalidOperationException)
                {
                }
                hostClient.Close();
                hostClient = null;
            }
            if(clientListener != null)
            {
                clientListener.Stop();
                clientListener = null;
            }
            while(clients.Count > 0)
            {
                try
                {
                    BinaryWriter bw = new BinaryWriter(new NetworkStream(clients[0].Socket, false));
                    bw.Write((byte)ClientMessage.Closed);
                    bw.Write((byte)0);
                    bw.Close();
                }
                catch(SocketException)
                {
                }
                catch(IOException)
                {
                }
                catch(InvalidOperationException)
                {
                }
                clients[0].Close();
                clients.RemoveAt(0);
            }
        }

        private void CloseNetGame()
        {
            CloseNetSockets();
            lstPlayersNet.Items.Clear();
            panNet.Show();
            panNet.BringToFront();
            panNetSettings.Hide();
            cmbNetGameType.Select();
            txtChat.Text = "";
            txtSend.Text = "";
            lblGameNet.Text = "Net Game";
        }

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

        #endregion

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

        [DllImport("winmm.dll", EntryPoint = "PlaySound", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool sndPlaySound(string pszSound, IntPtr hMod, SoundFlags sf);

        [Flags]
        private enum SoundFlags
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

        private CheckersSettings settings;
        private PictureBox selectedPicture;
        private CheckersGameType gameType;
        private int firstMove;
        private int agentIndex;
        private Image[] imageSet;
        private string player1Name;
        private string player2Name;
        private TcpClient hostClient = null;
        private TcpListener clientListener = null;
        private TcpClientCollection clients = new TcpClientCollection();
        private bool isSelfPlayer = false;
        private TcpClient remotePlayer = null;
        private bool checkingSockets = false;
        private bool refreshingPlayerInfo = false;
    }
}
