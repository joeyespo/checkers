namespace Checkers
{
    partial class MainForm
    {
        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
            this.CheckersUI = new Checkers.UI.CheckersUI();
            this.panGame = new System.Windows.Forms.Panel();
            this.panGameInfo = new System.Windows.Forms.Panel();
            this.txtTimePassed = new System.Windows.Forms.TextBox();
            this.picTurn = new System.Windows.Forms.PictureBox();
            this.txtJumpsP1 = new System.Windows.Forms.TextBox();
            this.picPawnP1 = new System.Windows.Forms.PictureBox();
            this.lblNameP1 = new System.Windows.Forms.Label();
            this.lblJumpsP1 = new System.Windows.Forms.Label();
            this.lblRemainingP1 = new System.Windows.Forms.Label();
            this.txtRemainingP1 = new System.Windows.Forms.TextBox();
            this.picPawnP2 = new System.Windows.Forms.PictureBox();
            this.lblNameP2 = new System.Windows.Forms.Label();
            this.txtRemainingP2 = new System.Windows.Forms.TextBox();
            this.lblJumpsP2 = new System.Windows.Forms.Label();
            this.lblRemainingP2 = new System.Windows.Forms.Label();
            this.txtJumpsP2 = new System.Windows.Forms.TextBox();
            this.lblTimePassed = new System.Windows.Forms.Label();
            this.lblGameType = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MainMenu();
            this.menuGame = new System.Windows.Forms.MenuItem();
            this.menuGameNew = new System.Windows.Forms.MenuItem();
            this.menuGameEnd = new System.Windows.Forms.MenuItem();
            this.menuGameLine01 = new System.Windows.Forms.MenuItem();
            this.menuGameExit = new System.Windows.Forms.MenuItem();
            this.menuView = new System.Windows.Forms.MenuItem();
            this.menuViewGamePanel = new System.Windows.Forms.MenuItem();
            this.menuViewNetPanel = new System.Windows.Forms.MenuItem();
            this.menuViewLine01 = new System.Windows.Forms.MenuItem();
            this.menuViewHint = new System.Windows.Forms.MenuItem();
            this.menuViewLastMoved = new System.Windows.Forms.MenuItem();
            this.menuViewLine02 = new System.Windows.Forms.MenuItem();
            this.menuViewPreferences = new System.Windows.Forms.MenuItem();
            this.menuHelp = new System.Windows.Forms.MenuItem();
            this.menuHelpAbout = new System.Windows.Forms.MenuItem();
            this.panOnline = new System.Windows.Forms.Panel();
            this.panNet = new System.Windows.Forms.Panel();
            this.lnkRemoteIP = new System.Windows.Forms.LinkLabel();
            this.lblRemoteIP = new System.Windows.Forms.Label();
            this.lnkLocalIP = new System.Windows.Forms.LinkLabel();
            this.lblLocalIP = new System.Windows.Forms.Label();
            this.splChat = new System.Windows.Forms.Splitter();
            this.panChat = new System.Windows.Forms.Panel();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.menuChat = new System.Windows.Forms.ContextMenu();
            this.menuChatCopy = new System.Windows.Forms.MenuItem();
            this.menuChatLine01 = new System.Windows.Forms.MenuItem();
            this.menuChatSave = new System.Windows.Forms.MenuItem();
            this.menuChatClear = new System.Windows.Forms.MenuItem();
            this.menuChatLine02 = new System.Windows.Forms.MenuItem();
            this.menuChatSelectAll = new System.Windows.Forms.MenuItem();
            this.imlTurn = new System.Windows.Forms.ImageList(this.components);
            this.tmrTimePassed = new System.Windows.Forms.Timer(this.components);
            this.tmrFlashWindow = new System.Windows.Forms.Timer(this.components);
            this.tmrTextDisplay = new System.Windows.Forms.Timer(this.components);
            this.tmrConnection = new System.Windows.Forms.Timer(this.components);
            this.dlgSaveChat = new System.Windows.Forms.SaveFileDialog();
            this.panGame.SuspendLayout();
            this.panGameInfo.SuspendLayout();
            this.panOnline.SuspendLayout();
            this.panNet.SuspendLayout();
            this.panChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckersUI
            // 
            this.CheckersUI.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.CheckersUI.ForeColor = System.Drawing.Color.White;
            this.CheckersUI.Location = new System.Drawing.Point(4, 4);
            this.CheckersUI.Name = "CheckersUI";
            this.CheckersUI.Size = new System.Drawing.Size(270, 270);
            this.CheckersUI.TabIndex = 1;
            this.CheckersUI.PieceDeselected += new System.EventHandler(this.CheckersUI_PieceDeselected);
            this.CheckersUI.PieceBadMove += new Checkers.UI.MoveEventHandler(this.CheckersUI_PieceBadMove);
            this.CheckersUI.PieceMovedPartial += new Checkers.UI.MoveEventHandler(this.CheckersUI_PieceMovedPartial);
            this.CheckersUI.GameStopped += new System.EventHandler(this.CheckersUI_GameStopped);
            this.CheckersUI.GameStarted += new System.EventHandler(this.CheckersUI_GameStarted);
            this.CheckersUI.PiecePickedUp += new System.EventHandler(this.CheckersUI_PiecePickedUp);
            this.CheckersUI.WinnerDeclared += new System.EventHandler(this.CheckersUI_WinnerDeclared);
            this.CheckersUI.TurnChanged += new System.EventHandler(this.CheckersUI_TurnChanged);
            this.CheckersUI.PieceMoved += new Checkers.UI.MoveEventHandler(this.CheckersUI_PieceMoved);
            // 
            // panGame
            // 
            this.panGame.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.panGame.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.panGameInfo,
                                                                          this.lblGameType});
            this.panGame.Location = new System.Drawing.Point(280, 4);
            this.panGame.Name = "panGame";
            this.panGame.Size = new System.Drawing.Size(144, 272);
            this.panGame.TabIndex = 1;
            // 
            // panGameInfo
            // 
            this.panGameInfo.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
              | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.panGameInfo.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                              this.txtTimePassed,
                                                                              this.picTurn,
                                                                              this.txtJumpsP1,
                                                                              this.picPawnP1,
                                                                              this.lblNameP1,
                                                                              this.lblJumpsP1,
                                                                              this.lblRemainingP1,
                                                                              this.txtRemainingP1,
                                                                              this.picPawnP2,
                                                                              this.lblNameP2,
                                                                              this.txtRemainingP2,
                                                                              this.lblJumpsP2,
                                                                              this.lblRemainingP2,
                                                                              this.txtJumpsP2,
                                                                              this.lblTimePassed});
            this.panGameInfo.Location = new System.Drawing.Point(0, 16);
            this.panGameInfo.Name = "panGameInfo";
            this.panGameInfo.Size = new System.Drawing.Size(144, 256);
            this.panGameInfo.TabIndex = 1;
            this.panGameInfo.Visible = false;
            // 
            // txtTimePassed
            // 
            this.txtTimePassed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTimePassed.Location = new System.Drawing.Point(72, 0);
            this.txtTimePassed.Name = "txtTimePassed";
            this.txtTimePassed.ReadOnly = true;
            this.txtTimePassed.Size = new System.Drawing.Size(52, 13);
            this.txtTimePassed.TabIndex = 1;
            this.txtTimePassed.Text = "0:00";
            this.txtTimePassed.WordWrap = false;
            // 
            // picTurn
            // 
            this.picTurn.Location = new System.Drawing.Point(44, 32);
            this.picTurn.Name = "picTurn";
            this.picTurn.Size = new System.Drawing.Size(64, 32);
            this.picTurn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTurn.TabIndex = 16;
            this.picTurn.TabStop = false;
            this.picTurn.Visible = false;
            // 
            // txtJumpsP1
            // 
            this.txtJumpsP1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJumpsP1.Location = new System.Drawing.Point(72, 100);
            this.txtJumpsP1.Name = "txtJumpsP1";
            this.txtJumpsP1.ReadOnly = true;
            this.txtJumpsP1.Size = new System.Drawing.Size(52, 13);
            this.txtJumpsP1.TabIndex = 6;
            this.txtJumpsP1.Text = "0";
            // 
            // picPawnP1
            // 
            this.picPawnP1.BackColor = System.Drawing.Color.White;
            this.picPawnP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawnP1.Location = new System.Drawing.Point(0, 32);
            this.picPawnP1.Name = "picPawnP1";
            this.picPawnP1.Size = new System.Drawing.Size(34, 34);
            this.picPawnP1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPawnP1.TabIndex = 9;
            this.picPawnP1.TabStop = false;
            // 
            // lblNameP1
            // 
            this.lblNameP1.Font = new System.Drawing.Font("Tahoma", 8.25F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblNameP1.Location = new System.Drawing.Point(0, 68);
            this.lblNameP1.Name = "lblNameP1";
            this.lblNameP1.Size = new System.Drawing.Size(124, 16);
            this.lblNameP1.TabIndex = 2;
            this.lblNameP1.Text = "Player";
            // 
            // lblJumpsP1
            // 
            this.lblJumpsP1.Location = new System.Drawing.Point(0, 100);
            this.lblJumpsP1.Name = "lblJumpsP1";
            this.lblJumpsP1.Size = new System.Drawing.Size(68, 16);
            this.lblJumpsP1.TabIndex = 5;
            this.lblJumpsP1.Text = "Jumps:";
            this.lblJumpsP1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemainingP1
            // 
            this.lblRemainingP1.Location = new System.Drawing.Point(0, 84);
            this.lblRemainingP1.Name = "lblRemainingP1";
            this.lblRemainingP1.Size = new System.Drawing.Size(68, 16);
            this.lblRemainingP1.TabIndex = 3;
            this.lblRemainingP1.Text = "Remaining:";
            this.lblRemainingP1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRemainingP1
            // 
            this.txtRemainingP1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemainingP1.Location = new System.Drawing.Point(72, 84);
            this.txtRemainingP1.Name = "txtRemainingP1";
            this.txtRemainingP1.ReadOnly = true;
            this.txtRemainingP1.Size = new System.Drawing.Size(52, 13);
            this.txtRemainingP1.TabIndex = 4;
            this.txtRemainingP1.Text = "0";
            // 
            // picPawnP2
            // 
            this.picPawnP2.BackColor = System.Drawing.Color.White;
            this.picPawnP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawnP2.Location = new System.Drawing.Point(0, 136);
            this.picPawnP2.Name = "picPawnP2";
            this.picPawnP2.Size = new System.Drawing.Size(34, 34);
            this.picPawnP2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPawnP2.TabIndex = 10;
            this.picPawnP2.TabStop = false;
            // 
            // lblNameP2
            // 
            this.lblNameP2.Font = new System.Drawing.Font("Tahoma", 8.25F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblNameP2.Location = new System.Drawing.Point(0, 172);
            this.lblNameP2.Name = "lblNameP2";
            this.lblNameP2.Size = new System.Drawing.Size(124, 16);
            this.lblNameP2.TabIndex = 7;
            this.lblNameP2.Text = "Opponent";
            // 
            // txtRemainingP2
            // 
            this.txtRemainingP2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemainingP2.Location = new System.Drawing.Point(72, 188);
            this.txtRemainingP2.Name = "txtRemainingP2";
            this.txtRemainingP2.ReadOnly = true;
            this.txtRemainingP2.Size = new System.Drawing.Size(52, 13);
            this.txtRemainingP2.TabIndex = 9;
            this.txtRemainingP2.Text = "0";
            // 
            // lblJumpsP2
            // 
            this.lblJumpsP2.Location = new System.Drawing.Point(0, 204);
            this.lblJumpsP2.Name = "lblJumpsP2";
            this.lblJumpsP2.Size = new System.Drawing.Size(68, 16);
            this.lblJumpsP2.TabIndex = 10;
            this.lblJumpsP2.Text = "Jumps:";
            this.lblJumpsP2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemainingP2
            // 
            this.lblRemainingP2.Location = new System.Drawing.Point(0, 188);
            this.lblRemainingP2.Name = "lblRemainingP2";
            this.lblRemainingP2.Size = new System.Drawing.Size(68, 16);
            this.lblRemainingP2.TabIndex = 8;
            this.lblRemainingP2.Text = "Remaining:";
            this.lblRemainingP2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtJumpsP2
            // 
            this.txtJumpsP2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJumpsP2.Location = new System.Drawing.Point(72, 204);
            this.txtJumpsP2.Name = "txtJumpsP2";
            this.txtJumpsP2.ReadOnly = true;
            this.txtJumpsP2.Size = new System.Drawing.Size(52, 13);
            this.txtJumpsP2.TabIndex = 11;
            this.txtJumpsP2.Text = "0";
            // 
            // lblTimePassed
            // 
            this.lblTimePassed.Name = "lblTimePassed";
            this.lblTimePassed.Size = new System.Drawing.Size(76, 16);
            this.lblTimePassed.TabIndex = 0;
            this.lblTimePassed.Text = "Time Passed:";
            // 
            // lblGameType
            // 
            this.lblGameType.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblGameType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblGameType.Name = "lblGameType";
            this.lblGameType.Size = new System.Drawing.Size(144, 16);
            this.lblGameType.TabIndex = 0;
            this.lblGameType.Text = "Game Panel";
            // 
            // menuMain
            // 
            this.menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuGame,
                                                                             this.menuView,
                                                                             this.menuHelp});
            // 
            // menuGame
            // 
            this.menuGame.Index = 0;
            this.menuGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuGameNew,
                                                                             this.menuGameEnd,
                                                                             this.menuGameLine01,
                                                                             this.menuGameExit});
            this.menuGame.Text = "&Game";
            // 
            // menuGameNew
            // 
            this.menuGameNew.Index = 0;
            this.menuGameNew.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuGameNew.Text = "&New Game...";
            this.menuGameNew.Click += new System.EventHandler(this.menuGameNew_Click);
            // 
            // menuGameEnd
            // 
            this.menuGameEnd.Enabled = false;
            this.menuGameEnd.Index = 1;
            this.menuGameEnd.Text = "&End Game";
            this.menuGameEnd.Click += new System.EventHandler(this.menuGameEnd_Click);
            // 
            // menuGameLine01
            // 
            this.menuGameLine01.Index = 2;
            this.menuGameLine01.Text = "-";
            // 
            // menuGameExit
            // 
            this.menuGameExit.Index = 3;
            this.menuGameExit.Text = "E&xit";
            this.menuGameExit.Click += new System.EventHandler(this.menuGameExit_Click);
            // 
            // menuView
            // 
            this.menuView.Index = 1;
            this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuViewGamePanel,
                                                                             this.menuViewNetPanel,
                                                                             this.menuViewLine01,
                                                                             this.menuViewHint,
                                                                             this.menuViewLastMoved,
                                                                             this.menuViewLine02,
                                                                             this.menuViewPreferences});
            this.menuView.Text = "&View";
            this.menuView.Popup += new System.EventHandler(this.menuView_Popup);
            // 
            // menuViewGamePanel
            // 
            this.menuViewGamePanel.Checked = true;
            this.menuViewGamePanel.Index = 0;
            this.menuViewGamePanel.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.menuViewGamePanel.Text = "&Game Panel";
            this.menuViewGamePanel.Click += new System.EventHandler(this.menuViewGamePanel_Click);
            // 
            // menuViewNetPanel
            // 
            this.menuViewNetPanel.Index = 1;
            this.menuViewNetPanel.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.menuViewNetPanel.Text = "Ne&t Panel";
            this.menuViewNetPanel.Click += new System.EventHandler(this.menuViewNetPanel_Click);
            // 
            // menuViewLine01
            // 
            this.menuViewLine01.Index = 2;
            this.menuViewLine01.Text = "-";
            // 
            // menuViewHint
            // 
            this.menuViewHint.Index = 3;
            this.menuViewHint.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.menuViewHint.Text = "&Hint";
            this.menuViewHint.Click += new System.EventHandler(this.menuViewHint_Click);
            // 
            // menuViewLastMoved
            // 
            this.menuViewLastMoved.Index = 4;
            this.menuViewLastMoved.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuViewLastMoved.Text = "&Last Moved";
            this.menuViewLastMoved.Click += new System.EventHandler(this.menuViewLastMoved_Click);
            // 
            // menuViewLine02
            // 
            this.menuViewLine02.Index = 5;
            this.menuViewLine02.Text = "-";
            // 
            // menuViewPreferences
            // 
            this.menuViewPreferences.Index = 6;
            this.menuViewPreferences.Text = "&Preferences...";
            this.menuViewPreferences.Click += new System.EventHandler(this.menuViewPreferences_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.Index = 2;
            this.menuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuHelpAbout});
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Index = 0;
            this.menuHelpAbout.Text = "&About...";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // panOnline
            // 
            this.panOnline.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
              | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.panOnline.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.panNet,
                                                                            this.splChat,
                                                                            this.panChat});
            this.panOnline.Location = new System.Drawing.Point(4, 280);
            this.panOnline.Name = "panOnline";
            this.panOnline.Size = new System.Drawing.Size(420, 80);
            this.panOnline.TabIndex = 2;
            // 
            // panNet
            // 
            this.panNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                         this.lnkRemoteIP,
                                                                         this.lblRemoteIP,
                                                                         this.lnkLocalIP,
                                                                         this.lblLocalIP});
            this.panNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panNet.Location = new System.Drawing.Point(276, 0);
            this.panNet.Name = "panNet";
            this.panNet.Size = new System.Drawing.Size(144, 80);
            this.panNet.TabIndex = 2;
            this.panNet.Visible = false;
            // 
            // lnkRemoteIP
            // 
            this.lnkRemoteIP.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkRemoteIP.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lnkRemoteIP.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkRemoteIP.LinkColor = System.Drawing.SystemColors.ControlText;
            this.lnkRemoteIP.Location = new System.Drawing.Point(0, 52);
            this.lnkRemoteIP.Name = "lnkRemoteIP";
            this.lnkRemoteIP.Size = new System.Drawing.Size(144, 16);
            this.lnkRemoteIP.TabIndex = 3;
            this.lnkRemoteIP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRemoteIP_LinkClicked);
            // 
            // lblRemoteIP
            // 
            this.lblRemoteIP.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblRemoteIP.Location = new System.Drawing.Point(0, 36);
            this.lblRemoteIP.Name = "lblRemoteIP";
            this.lblRemoteIP.Size = new System.Drawing.Size(144, 16);
            this.lblRemoteIP.TabIndex = 2;
            this.lblRemoteIP.Text = "Opponent IP:";
            // 
            // lnkLocalIP
            // 
            this.lnkLocalIP.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkLocalIP.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lnkLocalIP.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkLocalIP.LinkColor = System.Drawing.SystemColors.ControlText;
            this.lnkLocalIP.Location = new System.Drawing.Point(0, 16);
            this.lnkLocalIP.Name = "lnkLocalIP";
            this.lnkLocalIP.Size = new System.Drawing.Size(144, 16);
            this.lnkLocalIP.TabIndex = 1;
            this.lnkLocalIP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLocalIP_LinkClicked);
            // 
            // lblLocalIP
            // 
            this.lblLocalIP.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblLocalIP.Name = "lblLocalIP";
            this.lblLocalIP.Size = new System.Drawing.Size(144, 16);
            this.lblLocalIP.TabIndex = 0;
            this.lblLocalIP.Text = "Local IP:";
            // 
            // splChat
            // 
            this.splChat.Location = new System.Drawing.Point(270, 0);
            this.splChat.Name = "splChat";
            this.splChat.Size = new System.Drawing.Size(6, 80);
            this.splChat.TabIndex = 1;
            this.splChat.TabStop = false;
            // 
            // panChat
            // 
            this.panChat.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.txtSend,
                                                                          this.btnSend,
                                                                          this.txtChat});
            this.panChat.Dock = System.Windows.Forms.DockStyle.Left;
            this.panChat.Name = "panChat";
            this.panChat.Size = new System.Drawing.Size(270, 80);
            this.panChat.TabIndex = 0;
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.txtSend.Location = new System.Drawing.Point(0, 60);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(226, 20);
            this.txtSend.TabIndex = 1;
            this.txtSend.Text = "";
            this.txtSend.Leave += new System.EventHandler(this.txtSend_Leave);
            this.txtSend.Enter += new System.EventHandler(this.txtSend_Enter);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnSend.Location = new System.Drawing.Point(228, 60);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(42, 20);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtChat
            // 
            this.txtChat.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
              | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.txtChat.ContextMenu = this.menuChat;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.Size = new System.Drawing.Size(270, 58);
            this.txtChat.TabIndex = 0;
            this.txtChat.Text = "";
            // 
            // menuChat
            // 
            this.menuChat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuChatCopy,
                                                                             this.menuChatLine01,
                                                                             this.menuChatSave,
                                                                             this.menuChatClear,
                                                                             this.menuChatLine02,
                                                                             this.menuChatSelectAll});
            this.menuChat.Popup += new System.EventHandler(this.menuChat_Popup);
            // 
            // menuChatCopy
            // 
            this.menuChatCopy.Index = 0;
            this.menuChatCopy.Text = "&Copy";
            this.menuChatCopy.Click += new System.EventHandler(this.menuChatCopy_Click);
            // 
            // menuChatLine01
            // 
            this.menuChatLine01.Index = 1;
            this.menuChatLine01.Text = "-";
            // 
            // menuChatSave
            // 
            this.menuChatSave.Index = 2;
            this.menuChatSave.Text = "&Save...";
            this.menuChatSave.Click += new System.EventHandler(this.menuChatSave_Click);
            // 
            // menuChatClear
            // 
            this.menuChatClear.Index = 3;
            this.menuChatClear.Text = "&Clear Window";
            this.menuChatClear.Click += new System.EventHandler(this.menuChatClear_Click);
            // 
            // menuChatLine02
            // 
            this.menuChatLine02.Index = 4;
            this.menuChatLine02.Text = "-";
            // 
            // menuChatSelectAll
            // 
            this.menuChatSelectAll.Index = 5;
            this.menuChatSelectAll.Text = "Select &All";
            this.menuChatSelectAll.Click += new System.EventHandler(this.menuChatSelectAll_Click);
            // 
            // imlTurn
            // 
            this.imlTurn.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlTurn.ImageSize = new System.Drawing.Size(64, 32);
            this.imlTurn.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTurn.ImageStream")));
            this.imlTurn.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tmrTimePassed
            // 
            this.tmrTimePassed.Tick += new System.EventHandler(this.tmrTimePassed_Tick);
            // 
            // tmrFlashWindow
            // 
            this.tmrFlashWindow.Interval = 1000;
            this.tmrFlashWindow.Tick += new System.EventHandler(this.tmrFlashWindow_Tick);
            // 
            // tmrTextDisplay
            // 
            this.tmrTextDisplay.Interval = 2000;
            this.tmrTextDisplay.Tick += new System.EventHandler(this.tmrTextDisplay_Tick);
            // 
            // tmrConnection
            // 
            this.tmrConnection.Interval = 10;
            this.tmrConnection.Tick += new System.EventHandler(this.tmrConnection_Tick);
            // 
            // dlgSaveChat
            // 
            this.dlgSaveChat.DefaultExt = "rtf";
            this.dlgSaveChat.FileName = "Chat";
            this.dlgSaveChat.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
            this.dlgSaveChat.FilterIndex = 2;
            this.dlgSaveChat.Title = "Save Output";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(428, 365);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.panOnline,
                                                                  this.panGame,
                                                                  this.CheckersUI});
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(286, 324);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.Deactivate += new System.EventHandler(this.frmMain_Deactivate);
            this.panGame.ResumeLayout(false);
            this.panGameInfo.ResumeLayout(false);
            this.panOnline.ResumeLayout(false);
            this.panNet.ResumeLayout(false);
            this.panChat.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        private Checkers.UI.CheckersUI CheckersUI;
        private System.Windows.Forms.Label lblRemoteIP;
        private System.Windows.Forms.LinkLabel lnkRemoteIP;
        private System.Windows.Forms.ContextMenu menuChat;
        private System.Windows.Forms.MenuItem menuChatSave;
        private System.Windows.Forms.MenuItem menuChatLine01;
        private System.Windows.Forms.MenuItem menuChatCopy;
        private System.Windows.Forms.MenuItem menuChatSelectAll;
        private System.Windows.Forms.MenuItem menuChatLine02;
        private System.Windows.Forms.SaveFileDialog dlgSaveChat;
        private System.Windows.Forms.LinkLabel lnkLocalIP;
        private System.Windows.Forms.Label lblLocalIP;
        private System.Windows.Forms.MenuItem menuChatClear;
        private System.Windows.Forms.Timer tmrConnection;
        private System.Windows.Forms.Timer tmrTextDisplay;
        private System.Windows.Forms.MenuItem menuViewLastMoved;
        private System.Windows.Forms.MenuItem menuViewLine02;
        private System.Windows.Forms.Timer tmrFlashWindow;
        private System.Windows.Forms.Panel panGame;
        private System.Windows.Forms.MainMenu menuMain;
        private System.Windows.Forms.Panel panOnline;
        private System.Windows.Forms.MenuItem menuGame;
        private System.Windows.Forms.MenuItem menuGameNew;
        private System.Windows.Forms.MenuItem menuGameLine01;
        private System.Windows.Forms.MenuItem menuGameExit;
        private System.Windows.Forms.MenuItem menuHelp;
        private System.Windows.Forms.MenuItem menuViewGamePanel;
        private System.Windows.Forms.MenuItem menuViewNetPanel;
        private System.Windows.Forms.MenuItem menuView;
        private System.Windows.Forms.Splitter splChat;
        private System.Windows.Forms.Panel panNet;
        private System.Windows.Forms.Panel panChat;
        private System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Label lblTimePassed;
        private System.Windows.Forms.TextBox txtTimePassed;
        private System.Windows.Forms.Label lblGameType;
        private System.Windows.Forms.Panel panGameInfo;
        private System.Windows.Forms.TextBox txtJumpsP1;
        private System.Windows.Forms.PictureBox picPawnP1;
        private System.Windows.Forms.Label lblNameP1;
        private System.Windows.Forms.Label lblJumpsP1;
        private System.Windows.Forms.PictureBox picPawnP2;
        private System.Windows.Forms.Label lblNameP2;
        private System.Windows.Forms.Label lblJumpsP2;
        private System.Windows.Forms.TextBox txtJumpsP2;
        private System.Windows.Forms.MenuItem menuViewLine01;
        private System.Windows.Forms.MenuItem menuViewPreferences;
        private System.Windows.Forms.PictureBox picTurn;
        private System.Windows.Forms.ImageList imlTurn;
        private System.Windows.Forms.MenuItem menuGameEnd;
        private System.Windows.Forms.Label lblRemainingP1;
        private System.Windows.Forms.TextBox txtRemainingP1;
        private System.Windows.Forms.TextBox txtRemainingP2;
        private System.Windows.Forms.Label lblRemainingP2;
        private System.Windows.Forms.Timer tmrTimePassed;
        private System.Windows.Forms.MenuItem menuHelpAbout;
    }
}
