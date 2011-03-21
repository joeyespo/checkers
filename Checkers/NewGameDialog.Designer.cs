namespace Checkers
{
    partial class NewGameDialog
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
                {
                    components.Dispose();
                }
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewGameDialog));
            this.imlGameType = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabGame = new System.Windows.Forms.TabControl();
            this.tabGame1P = new System.Windows.Forms.TabPage();
            this.grpPlayerSettings1P = new System.Windows.Forms.GroupBox();
            this.txtPlayerName1P = new System.Windows.Forms.TextBox();
            this.lblPlayerName1P = new System.Windows.Forms.Label();
            this.grpGameSettings1P = new System.Windows.Forms.GroupBox();
            this.panImageSet1P = new System.Windows.Forms.Panel();
            this.lblImage1P2 = new System.Windows.Forms.Label();
            this.picPawn1P1 = new System.Windows.Forms.PictureBox();
            this.picPawn1P2 = new System.Windows.Forms.PictureBox();
            this.lblImage1P1 = new System.Windows.Forms.Label();
            this.picImageSwap1P = new System.Windows.Forms.PictureBox();
            this.picKing1P1 = new System.Windows.Forms.PictureBox();
            this.picKing1P2 = new System.Windows.Forms.PictureBox();
            this.lblFirstMove1P = new System.Windows.Forms.Label();
            this.cmbFirstMove1P = new System.Windows.Forms.ComboBox();
            this.cmbImageSet1P = new System.Windows.Forms.ComboBox();
            this.lblImageSet1P = new System.Windows.Forms.Label();
            this.lblGame1P = new System.Windows.Forms.Label();
            this.cmbAgent1P = new System.Windows.Forms.ComboBox();
            this.lblAgent1P = new System.Windows.Forms.Label();
            this.tabGame2P = new System.Windows.Forms.TabPage();
            this.grpPlayerSettings2P = new System.Windows.Forms.GroupBox();
            this.txtPlayerName2P1 = new System.Windows.Forms.TextBox();
            this.lblPlayerName2P1 = new System.Windows.Forms.Label();
            this.lblPlayerName2P2 = new System.Windows.Forms.Label();
            this.txtPlayerName2P2 = new System.Windows.Forms.TextBox();
            this.grpGameSettings2P = new System.Windows.Forms.GroupBox();
            this.panImageSet2P = new System.Windows.Forms.Panel();
            this.picKing2P1 = new System.Windows.Forms.PictureBox();
            this.picKing2P2 = new System.Windows.Forms.PictureBox();
            this.lblImage2P2 = new System.Windows.Forms.Label();
            this.picPawn2P1 = new System.Windows.Forms.PictureBox();
            this.picPawn2P2 = new System.Windows.Forms.PictureBox();
            this.lblImage2P1 = new System.Windows.Forms.Label();
            this.picImageSwap2P = new System.Windows.Forms.PictureBox();
            this.cmbImageSet2P = new System.Windows.Forms.ComboBox();
            this.lblImageSet2P = new System.Windows.Forms.Label();
            this.lblFirstMove2P = new System.Windows.Forms.Label();
            this.cmbFirstMove2P = new System.Windows.Forms.ComboBox();
            this.lblGame2P = new System.Windows.Forms.Label();
            this.tabGameNet = new System.Windows.Forms.TabPage();
            this.lblGameNet = new System.Windows.Forms.Label();
            this.panNet = new System.Windows.Forms.Panel();
            this.panGamesNet = new System.Windows.Forms.Panel();
            this.lstGamesNet = new System.Windows.Forms.ListBox();
            this.panConnectNet = new System.Windows.Forms.Panel();
            this.txtRemoteHostNet = new System.Windows.Forms.TextBox();
            this.lblRemoteHostNet = new System.Windows.Forms.Label();
            this.lblGamesNet = new System.Windows.Forms.Label();
            this.grpLocalInfoNet = new System.Windows.Forms.GroupBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lnkIPAddress = new System.Windows.Forms.LinkLabel();
            this.txtJoinNameNet = new System.Windows.Forms.TextBox();
            this.lblJoinNameNet = new System.Windows.Forms.Label();
            this.lblNetGameType = new System.Windows.Forms.Label();
            this.cmbNetGameType = new System.Windows.Forms.ComboBox();
            this.btnCreateNet = new System.Windows.Forms.Button();
            this.btnJoinNet = new System.Windows.Forms.Button();
            this.panNetSettings = new System.Windows.Forms.Panel();
            this.txtGameNameNet = new System.Windows.Forms.TextBox();
            this.btnBackNet = new System.Windows.Forms.Button();
            this.lblPlayersNet = new System.Windows.Forms.Label();
            this.lblChat = new System.Windows.Forms.Label();
            this.lstPlayersNet = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colPosition = new System.Windows.Forms.ColumnHeader();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.menuChat = new System.Windows.Forms.ContextMenu();
            this.menuChatCopy = new System.Windows.Forms.MenuItem();
            this.menuChatLine01 = new System.Windows.Forms.MenuItem();
            this.menuChatClear = new System.Windows.Forms.MenuItem();
            this.menuChatLine02 = new System.Windows.Forms.MenuItem();
            this.menuChatSelectAll = new System.Windows.Forms.MenuItem();
            this.grpPlayerSettingsNet = new System.Windows.Forms.GroupBox();
            this.panImageSetNet = new System.Windows.Forms.Panel();
            this.picKingNet1 = new System.Windows.Forms.PictureBox();
            this.picKingNet2 = new System.Windows.Forms.PictureBox();
            this.lblImageNet2 = new System.Windows.Forms.Label();
            this.picPawnNet1 = new System.Windows.Forms.PictureBox();
            this.picPawnNet2 = new System.Windows.Forms.PictureBox();
            this.lblImageNet1 = new System.Windows.Forms.Label();
            this.picImageSwapNet = new System.Windows.Forms.PictureBox();
            this.lblFirstMoveNet = new System.Windows.Forms.Label();
            this.chkLockImagesNet = new System.Windows.Forms.CheckBox();
            this.txtFirstMoveNet = new System.Windows.Forms.TextBox();
            this.cmbFirstMoveNet = new System.Windows.Forms.ComboBox();
            this.txtPlayerNameNet = new System.Windows.Forms.TextBox();
            this.imlImageSet = new System.Windows.Forms.ImageList(this.components);
            this.menuImage = new System.Windows.Forms.ContextMenu();
            this.menuImageDefault = new System.Windows.Forms.MenuItem();
            this.menuImageLine = new System.Windows.Forms.MenuItem();
            this.menuImagePreset = new System.Windows.Forms.MenuItem();
            this.menuImagePresetsBuiltIn = new System.Windows.Forms.MenuItem();
            this.menuImagePresetRed = new System.Windows.Forms.MenuItem();
            this.menuImagePresetBlack = new System.Windows.Forms.MenuItem();
            this.menuImagePresetWhite = new System.Windows.Forms.MenuItem();
            this.menuImagePresetGold = new System.Windows.Forms.MenuItem();
            this.menuImagePresetLine = new System.Windows.Forms.MenuItem();
            this.menuImagePresetsNone = new System.Windows.Forms.MenuItem();
            this.menuImageBrowse = new System.Windows.Forms.MenuItem();
            this.menuImageChooseColor = new System.Windows.Forms.MenuItem();
            this.dlgOpenImage = new System.Windows.Forms.OpenFileDialog();
            this.dlgSelectColor = new System.Windows.Forms.ColorDialog();
            this.imlKing = new System.Windows.Forms.ImageList(this.components);
            this.tmrConnection = new System.Windows.Forms.Timer(this.components);
            this.tabGame.SuspendLayout();
            this.tabGame1P.SuspendLayout();
            this.grpPlayerSettings1P.SuspendLayout();
            this.grpGameSettings1P.SuspendLayout();
            this.panImageSet1P.SuspendLayout();
            this.tabGame2P.SuspendLayout();
            this.grpPlayerSettings2P.SuspendLayout();
            this.grpGameSettings2P.SuspendLayout();
            this.panImageSet2P.SuspendLayout();
            this.tabGameNet.SuspendLayout();
            this.panNet.SuspendLayout();
            this.panGamesNet.SuspendLayout();
            this.panConnectNet.SuspendLayout();
            this.grpLocalInfoNet.SuspendLayout();
            this.panNetSettings.SuspendLayout();
            this.grpPlayerSettingsNet.SuspendLayout();
            this.panImageSetNet.SuspendLayout();
            this.SuspendLayout();
            // 
            // imlGameType
            // 
            this.imlGameType.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlGameType.ImageSize = new System.Drawing.Size(32, 32);
            this.imlGameType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlGameType.ImageStream")));
            this.imlGameType.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(512, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 36);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(416, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 36);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            // 
            // tabGame
            // 
            this.tabGame.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
              | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.tabGame.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabGame.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.tabGame1P,
                                                                          this.tabGame2P,
                                                                          this.tabGameNet});
            this.tabGame.ImageList = this.imlGameType;
            this.tabGame.ItemSize = new System.Drawing.Size(38, 38);
            this.tabGame.Location = new System.Drawing.Point(4, 12);
            this.tabGame.Name = "tabGame";
            this.tabGame.SelectedIndex = 0;
            this.tabGame.ShowToolTips = true;
            this.tabGame.Size = new System.Drawing.Size(600, 324);
            this.tabGame.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabGame.TabIndex = 1;
            this.tabGame.TabStop = false;
            this.tabGame.SelectedIndexChanged += new System.EventHandler(this.tabGame_SelectedIndexChanged);
            // 
            // tabGame1P
            // 
            this.tabGame1P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.grpPlayerSettings1P,
                                                                            this.grpGameSettings1P,
                                                                            this.lblGame1P,
                                                                            this.cmbAgent1P,
                                                                            this.lblAgent1P});
            this.tabGame1P.ImageIndex = 0;
            this.tabGame1P.Location = new System.Drawing.Point(4, 42);
            this.tabGame1P.Name = "tabGame1P";
            this.tabGame1P.Size = new System.Drawing.Size(592, 278);
            this.tabGame1P.TabIndex = 0;
            this.tabGame1P.ToolTipText = "Single Player Game";
            // 
            // grpPlayerSettings1P
            // 
            this.grpPlayerSettings1P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.txtPlayerName1P,
                                                                                      this.lblPlayerName1P});
            this.grpPlayerSettings1P.Location = new System.Drawing.Point(280, 32);
            this.grpPlayerSettings1P.Name = "grpPlayerSettings1P";
            this.grpPlayerSettings1P.Size = new System.Drawing.Size(268, 72);
            this.grpPlayerSettings1P.TabIndex = 2;
            this.grpPlayerSettings1P.TabStop = false;
            this.grpPlayerSettings1P.Text = "Player Settings";
            // 
            // txtPlayerName1P
            // 
            this.txtPlayerName1P.Location = new System.Drawing.Point(8, 44);
            this.txtPlayerName1P.MaxLength = 128;
            this.txtPlayerName1P.Name = "txtPlayerName1P";
            this.txtPlayerName1P.Size = new System.Drawing.Size(252, 20);
            this.txtPlayerName1P.TabIndex = 1;
            this.txtPlayerName1P.Text = "Player";
            // 
            // lblPlayerName1P
            // 
            this.lblPlayerName1P.Location = new System.Drawing.Point(8, 24);
            this.lblPlayerName1P.Name = "lblPlayerName1P";
            this.lblPlayerName1P.Size = new System.Drawing.Size(252, 20);
            this.lblPlayerName1P.TabIndex = 0;
            this.lblPlayerName1P.Text = "Player Name:";
            // 
            // grpGameSettings1P
            // 
            this.grpGameSettings1P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.panImageSet1P,
                                                                                    this.lblFirstMove1P,
                                                                                    this.cmbFirstMove1P,
                                                                                    this.cmbImageSet1P,
                                                                                    this.lblImageSet1P});
            this.grpGameSettings1P.Location = new System.Drawing.Point(0, 32);
            this.grpGameSettings1P.Name = "grpGameSettings1P";
            this.grpGameSettings1P.Size = new System.Drawing.Size(268, 200);
            this.grpGameSettings1P.TabIndex = 1;
            this.grpGameSettings1P.TabStop = false;
            this.grpGameSettings1P.Text = "Game Settings";
            // 
            // panImageSet1P
            // 
            this.panImageSet1P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.lblImage1P2,
                                                                                this.picPawn1P1,
                                                                                this.picPawn1P2,
                                                                                this.lblImage1P1,
                                                                                this.picImageSwap1P,
                                                                                this.picKing1P1,
                                                                                this.picKing1P2});
            this.panImageSet1P.Location = new System.Drawing.Point(8, 72);
            this.panImageSet1P.Name = "panImageSet1P";
            this.panImageSet1P.Size = new System.Drawing.Size(252, 92);
            this.panImageSet1P.TabIndex = 6;
            // 
            // lblImage1P2
            // 
            this.lblImage1P2.Location = new System.Drawing.Point(168, 20);
            this.lblImage1P2.Name = "lblImage1P2";
            this.lblImage1P2.Size = new System.Drawing.Size(80, 20);
            this.lblImage1P2.TabIndex = 1;
            this.lblImage1P2.Text = "Opponent";
            this.lblImage1P2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPawn1P1
            // 
            this.picPawn1P1.BackColor = System.Drawing.Color.White;
            this.picPawn1P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawn1P1.Location = new System.Drawing.Point(88, 4);
            this.picPawn1P1.Name = "picPawn1P1";
            this.picPawn1P1.Size = new System.Drawing.Size(34, 34);
            this.picPawn1P1.TabIndex = 11;
            this.picPawn1P1.TabStop = false;
            this.picPawn1P1.Click += new System.EventHandler(this.picImage_Click);
            this.picPawn1P1.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picPawn1P1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // picPawn1P2
            // 
            this.picPawn1P2.BackColor = System.Drawing.Color.White;
            this.picPawn1P2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawn1P2.Location = new System.Drawing.Point(128, 4);
            this.picPawn1P2.Name = "picPawn1P2";
            this.picPawn1P2.Size = new System.Drawing.Size(34, 34);
            this.picPawn1P2.TabIndex = 11;
            this.picPawn1P2.TabStop = false;
            this.picPawn1P2.Click += new System.EventHandler(this.picImage_Click);
            this.picPawn1P2.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picPawn1P2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // lblImage1P1
            // 
            this.lblImage1P1.Location = new System.Drawing.Point(4, 4);
            this.lblImage1P1.Name = "lblImage1P1";
            this.lblImage1P1.Size = new System.Drawing.Size(80, 20);
            this.lblImage1P1.TabIndex = 0;
            this.lblImage1P1.Text = "Player";
            this.lblImage1P1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picImageSwap1P
            // 
            this.picImageSwap1P.Image = ((System.Drawing.Bitmap)(resources.GetObject("picImageSwap1P.Image")));
            this.picImageSwap1P.Location = new System.Drawing.Point(92, 40);
            this.picImageSwap1P.Name = "picImageSwap1P";
            this.picImageSwap1P.Size = new System.Drawing.Size(65, 8);
            this.picImageSwap1P.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImageSwap1P.TabIndex = 14;
            this.picImageSwap1P.TabStop = false;
            this.picImageSwap1P.Click += new System.EventHandler(this.picImageSwap1P_Click);
            // 
            // picKing1P1
            // 
            this.picKing1P1.BackColor = System.Drawing.Color.White;
            this.picKing1P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKing1P1.Location = new System.Drawing.Point(88, 52);
            this.picKing1P1.Name = "picKing1P1";
            this.picKing1P1.Size = new System.Drawing.Size(34, 34);
            this.picKing1P1.TabIndex = 11;
            this.picKing1P1.TabStop = false;
            this.picKing1P1.Visible = false;
            this.picKing1P1.Click += new System.EventHandler(this.picImage_Click);
            this.picKing1P1.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picKing1P1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // picKing1P2
            // 
            this.picKing1P2.BackColor = System.Drawing.Color.White;
            this.picKing1P2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKing1P2.Location = new System.Drawing.Point(128, 52);
            this.picKing1P2.Name = "picKing1P2";
            this.picKing1P2.Size = new System.Drawing.Size(34, 34);
            this.picKing1P2.TabIndex = 11;
            this.picKing1P2.TabStop = false;
            this.picKing1P2.Visible = false;
            this.picKing1P2.Click += new System.EventHandler(this.picImage_Click);
            this.picKing1P2.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picKing1P2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // lblFirstMove1P
            // 
            this.lblFirstMove1P.Location = new System.Drawing.Point(8, 24);
            this.lblFirstMove1P.Name = "lblFirstMove1P";
            this.lblFirstMove1P.Size = new System.Drawing.Size(84, 20);
            this.lblFirstMove1P.TabIndex = 0;
            this.lblFirstMove1P.Text = "First Move:";
            this.lblFirstMove1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbFirstMove1P
            // 
            this.cmbFirstMove1P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFirstMove1P.Items.AddRange(new object[] {
                                                        "Player",
                                                        "Computer"});
            this.cmbFirstMove1P.Location = new System.Drawing.Point(96, 24);
            this.cmbFirstMove1P.Name = "cmbFirstMove1P";
            this.cmbFirstMove1P.Size = new System.Drawing.Size(164, 21);
            this.cmbFirstMove1P.TabIndex = 1;
            // 
            // cmbImageSet1P
            // 
            this.cmbImageSet1P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageSet1P.Items.AddRange(new object[] {
                                                       "Standard",
                                                       "Standard (Black)",
                                                       "Tournament",
                                                       "Custom"});
            this.cmbImageSet1P.Location = new System.Drawing.Point(96, 48);
            this.cmbImageSet1P.Name = "cmbImageSet1P";
            this.cmbImageSet1P.Size = new System.Drawing.Size(164, 21);
            this.cmbImageSet1P.TabIndex = 5;
            this.cmbImageSet1P.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
            // 
            // lblImageSet1P
            // 
            this.lblImageSet1P.Location = new System.Drawing.Point(8, 48);
            this.lblImageSet1P.Name = "lblImageSet1P";
            this.lblImageSet1P.Size = new System.Drawing.Size(84, 20);
            this.lblImageSet1P.TabIndex = 4;
            this.lblImageSet1P.Text = "Image Set:";
            this.lblImageSet1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGame1P
            // 
            this.lblGame1P.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblGame1P.BackColor = System.Drawing.SystemColors.Info;
            this.lblGame1P.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGame1P.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblGame1P.Name = "lblGame1P";
            this.lblGame1P.Size = new System.Drawing.Size(608, 20);
            this.lblGame1P.TabIndex = 0;
            this.lblGame1P.Text = "Single Player";
            this.lblGame1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbAgent1P
            // 
            this.cmbAgent1P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgent1P.Location = new System.Drawing.Point(280, 132);
            this.cmbAgent1P.Name = "cmbAgent1P";
            this.cmbAgent1P.Size = new System.Drawing.Size(252, 21);
            this.cmbAgent1P.TabIndex = 1;
            // 
            // lblAgent1P
            // 
            this.lblAgent1P.Location = new System.Drawing.Point(280, 112);
            this.lblAgent1P.Name = "lblAgent1P";
            this.lblAgent1P.Size = new System.Drawing.Size(252, 20);
            this.lblAgent1P.TabIndex = 0;
            this.lblAgent1P.Text = "Difficulty:";
            this.lblAgent1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabGame2P
            // 
            this.tabGame2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.grpPlayerSettings2P,
                                                                            this.grpGameSettings2P,
                                                                            this.lblGame2P});
            this.tabGame2P.ImageIndex = 1;
            this.tabGame2P.Location = new System.Drawing.Point(4, 42);
            this.tabGame2P.Name = "tabGame2P";
            this.tabGame2P.Size = new System.Drawing.Size(592, 278);
            this.tabGame2P.TabIndex = 2;
            this.tabGame2P.ToolTipText = "Multiplayer Game";
            // 
            // grpPlayerSettings2P
            // 
            this.grpPlayerSettings2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.txtPlayerName2P1,
                                                                                      this.lblPlayerName2P1,
                                                                                      this.lblPlayerName2P2,
                                                                                      this.txtPlayerName2P2});
            this.grpPlayerSettings2P.Location = new System.Drawing.Point(280, 32);
            this.grpPlayerSettings2P.Name = "grpPlayerSettings2P";
            this.grpPlayerSettings2P.Size = new System.Drawing.Size(268, 128);
            this.grpPlayerSettings2P.TabIndex = 2;
            this.grpPlayerSettings2P.TabStop = false;
            this.grpPlayerSettings2P.Text = "Player Settings";
            // 
            // txtPlayerName2P1
            // 
            this.txtPlayerName2P1.Location = new System.Drawing.Point(8, 44);
            this.txtPlayerName2P1.MaxLength = 128;
            this.txtPlayerName2P1.Name = "txtPlayerName2P1";
            this.txtPlayerName2P1.Size = new System.Drawing.Size(252, 20);
            this.txtPlayerName2P1.TabIndex = 1;
            this.txtPlayerName2P1.Text = "Player 1";
            // 
            // lblPlayerName2P1
            // 
            this.lblPlayerName2P1.Location = new System.Drawing.Point(8, 24);
            this.lblPlayerName2P1.Name = "lblPlayerName2P1";
            this.lblPlayerName2P1.Size = new System.Drawing.Size(252, 20);
            this.lblPlayerName2P1.TabIndex = 0;
            this.lblPlayerName2P1.Text = "Player 1 Name:";
            // 
            // lblPlayerName2P2
            // 
            this.lblPlayerName2P2.Location = new System.Drawing.Point(8, 76);
            this.lblPlayerName2P2.Name = "lblPlayerName2P2";
            this.lblPlayerName2P2.Size = new System.Drawing.Size(252, 20);
            this.lblPlayerName2P2.TabIndex = 2;
            this.lblPlayerName2P2.Text = "Player 2 Name:";
            // 
            // txtPlayerName2P2
            // 
            this.txtPlayerName2P2.Location = new System.Drawing.Point(8, 96);
            this.txtPlayerName2P2.MaxLength = 128;
            this.txtPlayerName2P2.Name = "txtPlayerName2P2";
            this.txtPlayerName2P2.Size = new System.Drawing.Size(252, 20);
            this.txtPlayerName2P2.TabIndex = 3;
            this.txtPlayerName2P2.Text = "Player 2";
            // 
            // grpGameSettings2P
            // 
            this.grpGameSettings2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.panImageSet2P,
                                                                                    this.cmbImageSet2P,
                                                                                    this.lblImageSet2P,
                                                                                    this.lblFirstMove2P,
                                                                                    this.cmbFirstMove2P});
            this.grpGameSettings2P.Location = new System.Drawing.Point(0, 32);
            this.grpGameSettings2P.Name = "grpGameSettings2P";
            this.grpGameSettings2P.Size = new System.Drawing.Size(268, 200);
            this.grpGameSettings2P.TabIndex = 1;
            this.grpGameSettings2P.TabStop = false;
            this.grpGameSettings2P.Text = "Game Settings";
            // 
            // panImageSet2P
            // 
            this.panImageSet2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.picKing2P1,
                                                                                this.picKing2P2,
                                                                                this.lblImage2P2,
                                                                                this.picPawn2P1,
                                                                                this.picPawn2P2,
                                                                                this.lblImage2P1,
                                                                                this.picImageSwap2P});
            this.panImageSet2P.Location = new System.Drawing.Point(8, 72);
            this.panImageSet2P.Name = "panImageSet2P";
            this.panImageSet2P.Size = new System.Drawing.Size(252, 92);
            this.panImageSet2P.TabIndex = 6;
            // 
            // picKing2P1
            // 
            this.picKing2P1.BackColor = System.Drawing.Color.White;
            this.picKing2P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKing2P1.Location = new System.Drawing.Point(88, 52);
            this.picKing2P1.Name = "picKing2P1";
            this.picKing2P1.Size = new System.Drawing.Size(34, 34);
            this.picKing2P1.TabIndex = 16;
            this.picKing2P1.TabStop = false;
            this.picKing2P1.Visible = false;
            this.picKing2P1.Click += new System.EventHandler(this.picImage_Click);
            this.picKing2P1.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picKing2P1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // picKing2P2
            // 
            this.picKing2P2.BackColor = System.Drawing.Color.White;
            this.picKing2P2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKing2P2.Location = new System.Drawing.Point(128, 52);
            this.picKing2P2.Name = "picKing2P2";
            this.picKing2P2.Size = new System.Drawing.Size(34, 34);
            this.picKing2P2.TabIndex = 15;
            this.picKing2P2.TabStop = false;
            this.picKing2P2.Visible = false;
            this.picKing2P2.Click += new System.EventHandler(this.picImage_Click);
            this.picKing2P2.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picKing2P2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // lblImage2P2
            // 
            this.lblImage2P2.Location = new System.Drawing.Point(168, 20);
            this.lblImage2P2.Name = "lblImage2P2";
            this.lblImage2P2.Size = new System.Drawing.Size(80, 20);
            this.lblImage2P2.TabIndex = 1;
            this.lblImage2P2.Text = "Player 2";
            this.lblImage2P2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPawn2P1
            // 
            this.picPawn2P1.BackColor = System.Drawing.Color.White;
            this.picPawn2P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawn2P1.Location = new System.Drawing.Point(88, 4);
            this.picPawn2P1.Name = "picPawn2P1";
            this.picPawn2P1.Size = new System.Drawing.Size(34, 34);
            this.picPawn2P1.TabIndex = 11;
            this.picPawn2P1.TabStop = false;
            this.picPawn2P1.Click += new System.EventHandler(this.picImage_Click);
            this.picPawn2P1.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picPawn2P1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // picPawn2P2
            // 
            this.picPawn2P2.BackColor = System.Drawing.Color.White;
            this.picPawn2P2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawn2P2.Location = new System.Drawing.Point(128, 4);
            this.picPawn2P2.Name = "picPawn2P2";
            this.picPawn2P2.Size = new System.Drawing.Size(34, 34);
            this.picPawn2P2.TabIndex = 11;
            this.picPawn2P2.TabStop = false;
            this.picPawn2P2.Click += new System.EventHandler(this.picImage_Click);
            this.picPawn2P2.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picPawn2P2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // lblImage2P1
            // 
            this.lblImage2P1.Location = new System.Drawing.Point(4, 4);
            this.lblImage2P1.Name = "lblImage2P1";
            this.lblImage2P1.Size = new System.Drawing.Size(80, 20);
            this.lblImage2P1.TabIndex = 0;
            this.lblImage2P1.Text = "Player 1";
            this.lblImage2P1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picImageSwap2P
            // 
            this.picImageSwap2P.Image = ((System.Drawing.Bitmap)(resources.GetObject("picImageSwap2P.Image")));
            this.picImageSwap2P.Location = new System.Drawing.Point(92, 40);
            this.picImageSwap2P.Name = "picImageSwap2P";
            this.picImageSwap2P.Size = new System.Drawing.Size(65, 8);
            this.picImageSwap2P.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImageSwap2P.TabIndex = 14;
            this.picImageSwap2P.TabStop = false;
            this.picImageSwap2P.Click += new System.EventHandler(this.picImageSwap2P_Click);
            // 
            // cmbImageSet2P
            // 
            this.cmbImageSet2P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageSet2P.Items.AddRange(new object[] {
                                                       "Standard",
                                                       "Standard (Black)",
                                                       "Tournament",
                                                       "Custom"});
            this.cmbImageSet2P.Location = new System.Drawing.Point(96, 48);
            this.cmbImageSet2P.Name = "cmbImageSet2P";
            this.cmbImageSet2P.Size = new System.Drawing.Size(164, 21);
            this.cmbImageSet2P.TabIndex = 5;
            this.cmbImageSet2P.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
            // 
            // lblImageSet2P
            // 
            this.lblImageSet2P.Location = new System.Drawing.Point(8, 48);
            this.lblImageSet2P.Name = "lblImageSet2P";
            this.lblImageSet2P.Size = new System.Drawing.Size(84, 20);
            this.lblImageSet2P.TabIndex = 4;
            this.lblImageSet2P.Text = "Image Set:";
            this.lblImageSet2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFirstMove2P
            // 
            this.lblFirstMove2P.Location = new System.Drawing.Point(8, 24);
            this.lblFirstMove2P.Name = "lblFirstMove2P";
            this.lblFirstMove2P.Size = new System.Drawing.Size(84, 20);
            this.lblFirstMove2P.TabIndex = 0;
            this.lblFirstMove2P.Text = "First Move:";
            this.lblFirstMove2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbFirstMove2P
            // 
            this.cmbFirstMove2P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFirstMove2P.Items.AddRange(new object[] {
                                                        "Player 1",
                                                        "Player 2"});
            this.cmbFirstMove2P.Location = new System.Drawing.Point(96, 24);
            this.cmbFirstMove2P.Name = "cmbFirstMove2P";
            this.cmbFirstMove2P.Size = new System.Drawing.Size(164, 21);
            this.cmbFirstMove2P.TabIndex = 1;
            // 
            // lblGame2P
            // 
            this.lblGame2P.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblGame2P.BackColor = System.Drawing.SystemColors.Info;
            this.lblGame2P.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGame2P.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblGame2P.Name = "lblGame2P";
            this.lblGame2P.Size = new System.Drawing.Size(592, 20);
            this.lblGame2P.TabIndex = 0;
            this.lblGame2P.Text = "Multiplayer";
            this.lblGame2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabGameNet
            // 
            this.tabGameNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.lblGameNet,
                                                                             this.panNet,
                                                                             this.panNetSettings});
            this.tabGameNet.ImageIndex = 2;
            this.tabGameNet.Location = new System.Drawing.Point(4, 42);
            this.tabGameNet.Name = "tabGameNet";
            this.tabGameNet.Size = new System.Drawing.Size(592, 278);
            this.tabGameNet.TabIndex = 3;
            this.tabGameNet.ToolTipText = "Net Game";
            // 
            // lblGameNet
            // 
            this.lblGameNet.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblGameNet.BackColor = System.Drawing.SystemColors.Info;
            this.lblGameNet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGameNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblGameNet.Name = "lblGameNet";
            this.lblGameNet.Size = new System.Drawing.Size(592, 20);
            this.lblGameNet.TabIndex = 0;
            this.lblGameNet.Text = "Net Game";
            this.lblGameNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panNet
            // 
            this.panNet.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
              | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.panNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                         this.panGamesNet,
                                                                         this.grpLocalInfoNet,
                                                                         this.lblNetGameType,
                                                                         this.cmbNetGameType,
                                                                         this.btnCreateNet,
                                                                         this.btnJoinNet});
            this.panNet.Location = new System.Drawing.Point(0, 20);
            this.panNet.Name = "panNet";
            this.panNet.Size = new System.Drawing.Size(592, 260);
            this.panNet.TabIndex = 1;
            this.panNet.Enter += new System.EventHandler(this.panNet_Enter);
            this.panNet.Leave += new System.EventHandler(this.panNet_Leave);
            // 
            // panGamesNet
            // 
            this.panGamesNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                              this.lstGamesNet,
                                                                              this.panConnectNet,
                                                                              this.lblGamesNet});
            this.panGamesNet.Location = new System.Drawing.Point(0, 60);
            this.panGamesNet.Name = "panGamesNet";
            this.panGamesNet.Size = new System.Drawing.Size(296, 136);
            this.panGamesNet.TabIndex = 2;
            // 
            // lstGamesNet
            // 
            this.lstGamesNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstGamesNet.IntegralHeight = false;
            this.lstGamesNet.Location = new System.Drawing.Point(0, 16);
            this.lstGamesNet.Name = "lstGamesNet";
            this.lstGamesNet.Size = new System.Drawing.Size(296, 76);
            this.lstGamesNet.TabIndex = 1;
            this.lstGamesNet.SelectedIndexChanged += new System.EventHandler(this.lstGamesNet_SelectedIndexChanged);
            // 
            // panConnectNet
            // 
            this.panConnectNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.txtRemoteHostNet,
                                                                                this.lblRemoteHostNet});
            this.panConnectNet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panConnectNet.Location = new System.Drawing.Point(0, 92);
            this.panConnectNet.Name = "panConnectNet";
            this.panConnectNet.Size = new System.Drawing.Size(296, 44);
            this.panConnectNet.TabIndex = 2;
            this.panConnectNet.Visible = false;
            // 
            // txtRemoteHostNet
            // 
            this.txtRemoteHostNet.Location = new System.Drawing.Point(0, 24);
            this.txtRemoteHostNet.Name = "txtRemoteHostNet";
            this.txtRemoteHostNet.Size = new System.Drawing.Size(296, 20);
            this.txtRemoteHostNet.TabIndex = 1;
            this.txtRemoteHostNet.Text = "";
            this.txtRemoteHostNet.TextChanged += new System.EventHandler(this.txtRemoteHostNet_TextChanged);
            // 
            // lblRemoteHostNet
            // 
            this.lblRemoteHostNet.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.lblRemoteHostNet.Location = new System.Drawing.Point(0, 8);
            this.lblRemoteHostNet.Name = "lblRemoteHostNet";
            this.lblRemoteHostNet.Size = new System.Drawing.Size(296, 16);
            this.lblRemoteHostNet.TabIndex = 0;
            this.lblRemoteHostNet.Text = "Remote Host:";
            // 
            // lblGamesNet
            // 
            this.lblGamesNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGamesNet.Name = "lblGamesNet";
            this.lblGamesNet.Size = new System.Drawing.Size(296, 16);
            this.lblGamesNet.TabIndex = 0;
            this.lblGamesNet.Text = "Games:";
            // 
            // grpLocalInfoNet
            // 
            this.grpLocalInfoNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.lblIPAddress,
                                                                                  this.lnkIPAddress,
                                                                                  this.txtJoinNameNet,
                                                                                  this.lblJoinNameNet});
            this.grpLocalInfoNet.Location = new System.Drawing.Point(308, 12);
            this.grpLocalInfoNet.Name = "grpLocalInfoNet";
            this.grpLocalInfoNet.Size = new System.Drawing.Size(240, 116);
            this.grpLocalInfoNet.TabIndex = 5;
            this.grpLocalInfoNet.TabStop = false;
            this.grpLocalInfoNet.Text = "Local Information";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.Location = new System.Drawing.Point(12, 24);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(68, 16);
            this.lblIPAddress.TabIndex = 0;
            this.lblIPAddress.Text = "IP Address:";
            // 
            // lnkIPAddress
            // 
            this.lnkIPAddress.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkIPAddress.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkIPAddress.LinkColor = System.Drawing.SystemColors.WindowText;
            this.lnkIPAddress.Location = new System.Drawing.Point(84, 24);
            this.lnkIPAddress.Name = "lnkIPAddress";
            this.lnkIPAddress.Size = new System.Drawing.Size(148, 16);
            this.lnkIPAddress.TabIndex = 1;
            this.lnkIPAddress.TabStop = true;
            this.lnkIPAddress.Text = "x.x.x.x";
            this.lnkIPAddress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkIPAddress.VisitedLinkColor = System.Drawing.SystemColors.WindowText;
            this.lnkIPAddress.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkIPAddress_LinkClicked);
            // 
            // txtJoinNameNet
            // 
            this.txtJoinNameNet.Location = new System.Drawing.Point(84, 48);
            this.txtJoinNameNet.MaxLength = 128;
            this.txtJoinNameNet.Name = "txtJoinNameNet";
            this.txtJoinNameNet.Size = new System.Drawing.Size(148, 20);
            this.txtJoinNameNet.TabIndex = 3;
            this.txtJoinNameNet.Text = "";
            // 
            // lblJoinNameNet
            // 
            this.lblJoinNameNet.Location = new System.Drawing.Point(12, 48);
            this.lblJoinNameNet.Name = "lblJoinNameNet";
            this.lblJoinNameNet.Size = new System.Drawing.Size(68, 20);
            this.lblJoinNameNet.TabIndex = 2;
            this.lblJoinNameNet.Text = "Name:";
            this.lblJoinNameNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetGameType
            // 
            this.lblNetGameType.Location = new System.Drawing.Point(0, 12);
            this.lblNetGameType.Name = "lblNetGameType";
            this.lblNetGameType.Size = new System.Drawing.Size(296, 16);
            this.lblNetGameType.TabIndex = 0;
            this.lblNetGameType.Text = "Game Type (Protocol):";
            // 
            // cmbNetGameType
            // 
            this.cmbNetGameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetGameType.Items.AddRange(new object[] {
                                                        "Internet TCP/IP Connection",
                                                        "Local (LAN) TCP/IP Connection"});
            this.cmbNetGameType.Location = new System.Drawing.Point(0, 28);
            this.cmbNetGameType.Name = "cmbNetGameType";
            this.cmbNetGameType.Size = new System.Drawing.Size(296, 21);
            this.cmbNetGameType.TabIndex = 1;
            this.cmbNetGameType.SelectedIndexChanged += new System.EventHandler(this.cmbNetGameType_SelectedIndexChanged);
            // 
            // btnCreateNet
            // 
            this.btnCreateNet.Location = new System.Drawing.Point(100, 204);
            this.btnCreateNet.Name = "btnCreateNet";
            this.btnCreateNet.Size = new System.Drawing.Size(96, 32);
            this.btnCreateNet.TabIndex = 3;
            this.btnCreateNet.Text = "&Create";
            this.btnCreateNet.Click += new System.EventHandler(this.btnCreateNet_Click);
            // 
            // btnJoinNet
            // 
            this.btnJoinNet.Enabled = false;
            this.btnJoinNet.Location = new System.Drawing.Point(200, 204);
            this.btnJoinNet.Name = "btnJoinNet";
            this.btnJoinNet.Size = new System.Drawing.Size(96, 32);
            this.btnJoinNet.TabIndex = 4;
            this.btnJoinNet.Text = "&Join";
            this.btnJoinNet.Click += new System.EventHandler(this.btnJoinNet_Click);
            // 
            // panNetSettings
            // 
            this.panNetSettings.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
              | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right);
            this.panNetSettings.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.txtGameNameNet,
                                                                                 this.btnBackNet,
                                                                                 this.lblPlayersNet,
                                                                                 this.lblChat,
                                                                                 this.lstPlayersNet,
                                                                                 this.btnSend,
                                                                                 this.txtSend,
                                                                                 this.txtChat,
                                                                                 this.grpPlayerSettingsNet,
                                                                                 this.txtPlayerNameNet});
            this.panNetSettings.Location = new System.Drawing.Point(0, 20);
            this.panNetSettings.Name = "panNetSettings";
            this.panNetSettings.Size = new System.Drawing.Size(592, 260);
            this.panNetSettings.TabIndex = 9;
            this.panNetSettings.Enter += new System.EventHandler(this.panNetSettings_Enter);
            this.panNetSettings.Leave += new System.EventHandler(this.panNetSettings_Leave);
            // 
            // txtGameNameNet
            // 
            this.txtGameNameNet.BackColor = System.Drawing.SystemColors.Info;
            this.txtGameNameNet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGameNameNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.txtGameNameNet.Location = new System.Drawing.Point(0, 8);
            this.txtGameNameNet.Name = "txtGameNameNet";
            this.txtGameNameNet.ReadOnly = true;
            this.txtGameNameNet.Size = new System.Drawing.Size(288, 20);
            this.txtGameNameNet.TabIndex = 9;
            this.txtGameNameNet.Text = "";
            this.txtGameNameNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBackNet
            // 
            this.btnBackNet.Location = new System.Drawing.Point(504, 220);
            this.btnBackNet.Name = "btnBackNet";
            this.btnBackNet.Size = new System.Drawing.Size(88, 36);
            this.btnBackNet.TabIndex = 7;
            this.btnBackNet.Text = "&Back";
            this.btnBackNet.Click += new System.EventHandler(this.btnBackNet_Click);
            // 
            // lblPlayersNet
            // 
            this.lblPlayersNet.Location = new System.Drawing.Point(0, 36);
            this.lblPlayersNet.Name = "lblPlayersNet";
            this.lblPlayersNet.Size = new System.Drawing.Size(288, 16);
            this.lblPlayersNet.TabIndex = 0;
            this.lblPlayersNet.Text = "Connected Players:";
            // 
            // lblChat
            // 
            this.lblChat.Location = new System.Drawing.Point(0, 140);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(288, 16);
            this.lblChat.TabIndex = 2;
            this.lblChat.Text = "Discussion:";
            // 
            // lstPlayersNet
            // 
            this.lstPlayersNet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    this.colName,
                                                                                    this.colPosition});
            this.lstPlayersNet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstPlayersNet.Location = new System.Drawing.Point(0, 52);
            this.lstPlayersNet.Name = "lstPlayersNet";
            this.lstPlayersNet.Size = new System.Drawing.Size(288, 80);
            this.lstPlayersNet.TabIndex = 1;
            this.lstPlayersNet.View = System.Windows.Forms.View.Details;
            this.lstPlayersNet.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstPlayersNet_ItemCheck);
            // 
            // colName
            // 
            this.colName.Text = "Player";
            this.colName.Width = 120;
            // 
            // colPosition
            // 
            this.colPosition.Text = "Position";
            this.colPosition.Width = 120;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(244, 236);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(44, 20);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "&Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.txtSend.Location = new System.Drawing.Point(0, 236);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(240, 21);
            this.txtSend.TabIndex = 4;
            this.txtSend.Text = "";
            this.txtSend.Leave += new System.EventHandler(this.txtSend_Leave);
            this.txtSend.Enter += new System.EventHandler(this.txtSend_Enter);
            // 
            // txtChat
            // 
            this.txtChat.ContextMenu = this.menuChat;
            this.txtChat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.txtChat.Location = new System.Drawing.Point(0, 156);
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.Size = new System.Drawing.Size(288, 76);
            this.txtChat.TabIndex = 3;
            this.txtChat.Text = "";
            // 
            // menuChat
            // 
            this.menuChat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuChatCopy,
                                                                             this.menuChatLine01,
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
            // menuChatClear
            // 
            this.menuChatClear.Index = 2;
            this.menuChatClear.Text = "&Clear Window";
            this.menuChatClear.Click += new System.EventHandler(this.menuChatClear_Click);
            // 
            // menuChatLine02
            // 
            this.menuChatLine02.Index = 3;
            this.menuChatLine02.Text = "-";
            // 
            // menuChatSelectAll
            // 
            this.menuChatSelectAll.Index = 4;
            this.menuChatSelectAll.Text = "Select &All";
            this.menuChatSelectAll.Click += new System.EventHandler(this.menuChatSelectAll_Click);
            // 
            // grpPlayerSettingsNet
            // 
            this.grpPlayerSettingsNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.panImageSetNet,
                                                                                       this.lblFirstMoveNet,
                                                                                       this.chkLockImagesNet,
                                                                                       this.txtFirstMoveNet,
                                                                                       this.cmbFirstMoveNet});
            this.grpPlayerSettingsNet.Location = new System.Drawing.Point(304, 36);
            this.grpPlayerSettingsNet.Name = "grpPlayerSettingsNet";
            this.grpPlayerSettingsNet.Size = new System.Drawing.Size(288, 176);
            this.grpPlayerSettingsNet.TabIndex = 6;
            this.grpPlayerSettingsNet.TabStop = false;
            this.grpPlayerSettingsNet.Text = "Game Settings";
            // 
            // panImageSetNet
            // 
            this.panImageSetNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.picKingNet1,
                                                                                 this.picKingNet2,
                                                                                 this.lblImageNet2,
                                                                                 this.picPawnNet1,
                                                                                 this.picPawnNet2,
                                                                                 this.lblImageNet1,
                                                                                 this.picImageSwapNet});
            this.panImageSetNet.Location = new System.Drawing.Point(8, 52);
            this.panImageSetNet.Name = "panImageSetNet";
            this.panImageSetNet.Size = new System.Drawing.Size(248, 92);
            this.panImageSetNet.TabIndex = 3;
            // 
            // picKingNet1
            // 
            this.picKingNet1.BackColor = System.Drawing.Color.White;
            this.picKingNet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKingNet1.Location = new System.Drawing.Point(88, 52);
            this.picKingNet1.Name = "picKingNet1";
            this.picKingNet1.Size = new System.Drawing.Size(34, 34);
            this.picKingNet1.TabIndex = 18;
            this.picKingNet1.TabStop = false;
            this.picKingNet1.Click += new System.EventHandler(this.picImage_Click);
            this.picKingNet1.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picKingNet1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // picKingNet2
            // 
            this.picKingNet2.BackColor = System.Drawing.Color.White;
            this.picKingNet2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKingNet2.Location = new System.Drawing.Point(128, 52);
            this.picKingNet2.Name = "picKingNet2";
            this.picKingNet2.Size = new System.Drawing.Size(34, 34);
            this.picKingNet2.TabIndex = 17;
            this.picKingNet2.TabStop = false;
            this.picKingNet2.Click += new System.EventHandler(this.picImage_Click);
            this.picKingNet2.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picKingNet2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // lblImageNet2
            // 
            this.lblImageNet2.Location = new System.Drawing.Point(168, 20);
            this.lblImageNet2.Name = "lblImageNet2";
            this.lblImageNet2.Size = new System.Drawing.Size(80, 20);
            this.lblImageNet2.TabIndex = 1;
            this.lblImageNet2.Text = "Opponent";
            this.lblImageNet2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPawnNet1
            // 
            this.picPawnNet1.BackColor = System.Drawing.Color.White;
            this.picPawnNet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawnNet1.Location = new System.Drawing.Point(88, 4);
            this.picPawnNet1.Name = "picPawnNet1";
            this.picPawnNet1.Size = new System.Drawing.Size(34, 34);
            this.picPawnNet1.TabIndex = 11;
            this.picPawnNet1.TabStop = false;
            this.picPawnNet1.Click += new System.EventHandler(this.picImage_Click);
            this.picPawnNet1.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picPawnNet1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // picPawnNet2
            // 
            this.picPawnNet2.BackColor = System.Drawing.Color.White;
            this.picPawnNet2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPawnNet2.Location = new System.Drawing.Point(128, 4);
            this.picPawnNet2.Name = "picPawnNet2";
            this.picPawnNet2.Size = new System.Drawing.Size(34, 34);
            this.picPawnNet2.TabIndex = 11;
            this.picPawnNet2.TabStop = false;
            this.picPawnNet2.Click += new System.EventHandler(this.picImage_Click);
            this.picPawnNet2.EnabledChanged += new System.EventHandler(this.picImage_EnabledChanged);
            this.picPawnNet2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            // 
            // lblImageNet1
            // 
            this.lblImageNet1.Location = new System.Drawing.Point(4, 4);
            this.lblImageNet1.Name = "lblImageNet1";
            this.lblImageNet1.Size = new System.Drawing.Size(80, 20);
            this.lblImageNet1.TabIndex = 0;
            this.lblImageNet1.Text = "Player";
            this.lblImageNet1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picImageSwapNet
            // 
            this.picImageSwapNet.Image = ((System.Drawing.Bitmap)(resources.GetObject("picImageSwapNet.Image")));
            this.picImageSwapNet.Location = new System.Drawing.Point(92, 40);
            this.picImageSwapNet.Name = "picImageSwapNet";
            this.picImageSwapNet.Size = new System.Drawing.Size(65, 8);
            this.picImageSwapNet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImageSwapNet.TabIndex = 14;
            this.picImageSwapNet.TabStop = false;
            this.picImageSwapNet.Click += new System.EventHandler(this.picImageSwapNet_Click);
            // 
            // lblFirstMoveNet
            // 
            this.lblFirstMoveNet.Location = new System.Drawing.Point(8, 24);
            this.lblFirstMoveNet.Name = "lblFirstMoveNet";
            this.lblFirstMoveNet.Size = new System.Drawing.Size(84, 20);
            this.lblFirstMoveNet.TabIndex = 0;
            this.lblFirstMoveNet.Text = "First Move:";
            this.lblFirstMoveNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkLockImagesNet
            // 
            this.chkLockImagesNet.Location = new System.Drawing.Point(8, 152);
            this.chkLockImagesNet.Name = "chkLockImagesNet";
            this.chkLockImagesNet.Size = new System.Drawing.Size(252, 16);
            this.chkLockImagesNet.TabIndex = 4;
            this.chkLockImagesNet.Text = "Lock Images";
            this.chkLockImagesNet.Visible = false;
            this.chkLockImagesNet.CheckedChanged += new System.EventHandler(this.chkLockImagesNet_CheckedChanged);
            // 
            // txtFirstMoveNet
            // 
            this.txtFirstMoveNet.Location = new System.Drawing.Point(96, 24);
            this.txtFirstMoveNet.Name = "txtFirstMoveNet";
            this.txtFirstMoveNet.ReadOnly = true;
            this.txtFirstMoveNet.Size = new System.Drawing.Size(160, 20);
            this.txtFirstMoveNet.TabIndex = 1;
            this.txtFirstMoveNet.Text = "";
            this.txtFirstMoveNet.Visible = false;
            // 
            // cmbFirstMoveNet
            // 
            this.cmbFirstMoveNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFirstMoveNet.Items.AddRange(new object[] {
                                                         "Host",
                                                         "Opponent"});
            this.cmbFirstMoveNet.Location = new System.Drawing.Point(96, 24);
            this.cmbFirstMoveNet.Name = "cmbFirstMoveNet";
            this.cmbFirstMoveNet.Size = new System.Drawing.Size(160, 21);
            this.cmbFirstMoveNet.TabIndex = 2;
            this.cmbFirstMoveNet.Visible = false;
            this.cmbFirstMoveNet.SelectedIndexChanged += new System.EventHandler(this.cmbFirstMoveNet_SelectedIndexChanged);
            // 
            // txtPlayerNameNet
            // 
            this.txtPlayerNameNet.BackColor = System.Drawing.SystemColors.Window;
            this.txtPlayerNameNet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlayerNameNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.txtPlayerNameNet.Location = new System.Drawing.Point(304, 8);
            this.txtPlayerNameNet.MaxLength = 128;
            this.txtPlayerNameNet.Name = "txtPlayerNameNet";
            this.txtPlayerNameNet.ReadOnly = true;
            this.txtPlayerNameNet.Size = new System.Drawing.Size(288, 20);
            this.txtPlayerNameNet.TabIndex = 1;
            this.txtPlayerNameNet.Text = "";
            this.txtPlayerNameNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // imlImageSet
            // 
            this.imlImageSet.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlImageSet.ImageSize = new System.Drawing.Size(32, 32);
            this.imlImageSet.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlImageSet.ImageStream")));
            this.imlImageSet.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuImage
            // 
            this.menuImage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuImageDefault,
                                                                              this.menuImageLine,
                                                                              this.menuImagePreset,
                                                                              this.menuImageBrowse,
                                                                              this.menuImageChooseColor});
            this.menuImage.Popup += new System.EventHandler(this.menuImage_Popup);
            // 
            // menuImageDefault
            // 
            this.menuImageDefault.Index = 0;
            this.menuImageDefault.Text = "&Default Image";
            this.menuImageDefault.Click += new System.EventHandler(this.menuImageDefault_Click);
            // 
            // menuImageLine
            // 
            this.menuImageLine.Index = 1;
            this.menuImageLine.Text = "-";
            // 
            // menuImagePreset
            // 
            this.menuImagePreset.Index = 2;
            this.menuImagePreset.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.menuImagePresetsBuiltIn,
                                                                                    this.menuImagePresetLine,
                                                                                    this.menuImagePresetsNone});
            this.menuImagePreset.Text = "&Preset Image";
            // 
            // menuImagePresetsBuiltIn
            // 
            this.menuImagePresetsBuiltIn.Index = 0;
            this.menuImagePresetsBuiltIn.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                            this.menuImagePresetRed,
                                                                                            this.menuImagePresetBlack,
                                                                                            this.menuImagePresetWhite,
                                                                                            this.menuImagePresetGold});
            this.menuImagePresetsBuiltIn.Text = "&Built-in";
            // 
            // menuImagePresetRed
            // 
            this.menuImagePresetRed.Index = 0;
            this.menuImagePresetRed.Text = "&Red Set";
            this.menuImagePresetRed.Click += new System.EventHandler(this.menuImagePresetRed_Click);
            // 
            // menuImagePresetBlack
            // 
            this.menuImagePresetBlack.Index = 1;
            this.menuImagePresetBlack.Text = "&Black Set";
            this.menuImagePresetBlack.Click += new System.EventHandler(this.menuImagePresetBlack_Click);
            // 
            // menuImagePresetWhite
            // 
            this.menuImagePresetWhite.Index = 2;
            this.menuImagePresetWhite.Text = "&White Set";
            this.menuImagePresetWhite.Click += new System.EventHandler(this.menuImagePresetWhite_Click);
            // 
            // menuImagePresetGold
            // 
            this.menuImagePresetGold.Index = 3;
            this.menuImagePresetGold.Text = "&Gold Set";
            this.menuImagePresetGold.Click += new System.EventHandler(this.menuImagePresetGold_Click);
            // 
            // menuImagePresetLine
            // 
            this.menuImagePresetLine.Index = 1;
            this.menuImagePresetLine.Text = "-";
            // 
            // menuImagePresetsNone
            // 
            this.menuImagePresetsNone.Enabled = false;
            this.menuImagePresetsNone.Index = 2;
            this.menuImagePresetsNone.Text = "(No Presets)";
            // 
            // menuImageBrowse
            // 
            this.menuImageBrowse.Index = 3;
            this.menuImageBrowse.Text = "&Browse for Image...";
            this.menuImageBrowse.Click += new System.EventHandler(this.menuImageBrowse_Click);
            // 
            // menuImageChooseColor
            // 
            this.menuImageChooseColor.Index = 4;
            this.menuImageChooseColor.Text = "&Choose Color...";
            this.menuImageChooseColor.Click += new System.EventHandler(this.menuImageChooseColor_Click);
            // 
            // dlgOpenImage
            // 
            this.dlgOpenImage.Filter = "Supported Image Files (*.bmp;*.gif;*.jpg;*.jpeg;*.tiff;*.png)|*.bmp;*.gif;*.jpg;*" +
              ".jpeg;*.tiff;*.png|All Files (*.*)|*.*";
            this.dlgOpenImage.Title = "Open Custom Image";
            // 
            // dlgSelectColor
            // 
            this.dlgSelectColor.AnyColor = true;
            this.dlgSelectColor.FullOpen = true;
            // 
            // imlKing
            // 
            this.imlKing.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlKing.ImageSize = new System.Drawing.Size(32, 32);
            this.imlKing.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlKing.ImageStream")));
            this.imlKing.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tmrConnection
            // 
            this.tmrConnection.Interval = 10;
            this.tmrConnection.Tick += new System.EventHandler(this.tmrConnection_Tick);
            // 
            // frmNewGame
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(606, 387);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.btnCancel,
                                                                  this.btnOK,
                                                                  this.tabGame});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmNewGame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Checkers Game";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmNewGame_Closing);
            this.Load += new System.EventHandler(this.frmNewGame_Load);
            this.Activated += new System.EventHandler(this.frmNewGame_Activated);
            this.tabGame.ResumeLayout(false);
            this.tabGame1P.ResumeLayout(false);
            this.grpPlayerSettings1P.ResumeLayout(false);
            this.grpGameSettings1P.ResumeLayout(false);
            this.panImageSet1P.ResumeLayout(false);
            this.tabGame2P.ResumeLayout(false);
            this.grpPlayerSettings2P.ResumeLayout(false);
            this.grpGameSettings2P.ResumeLayout(false);
            this.panImageSet2P.ResumeLayout(false);
            this.tabGameNet.ResumeLayout(false);
            this.panNet.ResumeLayout(false);
            this.panGamesNet.ResumeLayout(false);
            this.panConnectNet.ResumeLayout(false);
            this.grpLocalInfoNet.ResumeLayout(false);
            this.panNetSettings.ResumeLayout(false);
            this.grpPlayerSettingsNet.ResumeLayout(false);
            this.panImageSetNet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        private System.Windows.Forms.ImageList imlGameType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabGame;
        private System.Windows.Forms.TabPage tabGame1P;
        private System.Windows.Forms.TabPage tabGame2P;
        private System.Windows.Forms.TabPage tabGameNet;
        private System.Windows.Forms.Label lblGame1P;
        private System.Windows.Forms.Label lblGame2P;
        private System.Windows.Forms.Label lblGameNet;
        private System.Windows.Forms.ComboBox cmbFirstMove2P;
        private System.Windows.Forms.Label lblFirstMove2P;
        private System.Windows.Forms.Panel panNetSettings;
        private System.Windows.Forms.Panel panNet;
        private System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cmbNetGameType;
        private System.Windows.Forms.GroupBox grpGameSettings1P;
        private System.Windows.Forms.GroupBox grpGameSettings2P;
        private System.Windows.Forms.GroupBox grpPlayerSettingsNet;
        private System.Windows.Forms.Label lblFirstMoveNet;
        private System.Windows.Forms.ComboBox cmbFirstMoveNet;
        private System.Windows.Forms.ListView lstPlayersNet;
        private System.Windows.Forms.Label lblChat;
        private System.Windows.Forms.Label lblPlayersNet;
        private System.Windows.Forms.Label lblNetGameType;
        private System.Windows.Forms.GroupBox grpLocalInfoNet;
        private System.Windows.Forms.LinkLabel lnkIPAddress;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.ComboBox cmbImageSet2P;
        private System.Windows.Forms.Label lblImageSet2P;
        private System.Windows.Forms.ComboBox cmbImageSet1P;
        private System.Windows.Forms.Label lblImageSet1P;
        private System.Windows.Forms.Label lblFirstMove1P;
        private System.Windows.Forms.ComboBox cmbFirstMove1P;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPosition;
        private System.Windows.Forms.ImageList imlImageSet;
        private System.Windows.Forms.Label lblImage1P1;
        private System.Windows.Forms.ContextMenu menuImage;
        private System.Windows.Forms.MenuItem menuImageBrowse;
        private System.Windows.Forms.MenuItem menuImageChooseColor;
        private System.Windows.Forms.OpenFileDialog dlgOpenImage;
        private System.Windows.Forms.ColorDialog dlgSelectColor;
        private System.Windows.Forms.Panel panImageSet1P;
        private System.Windows.Forms.Label lblImage1P2;
        private System.Windows.Forms.Panel panImageSet2P;
        private System.Windows.Forms.PictureBox picImageSwap1P;
        private System.Windows.Forms.Label lblImage2P2;
        private System.Windows.Forms.Label lblImage2P1;
        private System.Windows.Forms.PictureBox picImageSwap2P;
        private System.Windows.Forms.Panel panImageSetNet;
        private System.Windows.Forms.PictureBox picImageSwapNet;
        private System.Windows.Forms.ImageList imlKing;
        private System.Windows.Forms.Label lblImageNet2;
        private System.Windows.Forms.Label lblImageNet1;
        private System.Windows.Forms.PictureBox picPawn1P1;
        private System.Windows.Forms.PictureBox picPawn1P2;
        private System.Windows.Forms.PictureBox picKing1P1;
        private System.Windows.Forms.PictureBox picKing1P2;
        private System.Windows.Forms.PictureBox picPawn2P1;
        private System.Windows.Forms.PictureBox picPawn2P2;
        private System.Windows.Forms.PictureBox picPawnNet1;
        private System.Windows.Forms.PictureBox picPawnNet2;
        private System.Windows.Forms.PictureBox picKing2P1;
        private System.Windows.Forms.PictureBox picKing2P2;
        private System.Windows.Forms.PictureBox picKingNet1;
        private System.Windows.Forms.PictureBox picKingNet2;
        private System.Windows.Forms.MenuItem menuImageDefault;
        private System.Windows.Forms.MenuItem menuImageLine;
        private System.Windows.Forms.GroupBox grpPlayerSettings1P;
        private System.Windows.Forms.GroupBox grpPlayerSettings2P;
        private System.Windows.Forms.Label lblPlayerName1P;
        private System.Windows.Forms.Label lblPlayerName2P1;
        private System.Windows.Forms.Label lblPlayerName2P2;
        private System.Windows.Forms.TextBox txtPlayerName1P;
        private System.Windows.Forms.TextBox txtPlayerName2P1;
        private System.Windows.Forms.TextBox txtPlayerName2P2;
        private System.Windows.Forms.MenuItem menuImagePreset;
        private System.Windows.Forms.MenuItem menuImagePresetRed;
        private System.Windows.Forms.MenuItem menuImagePresetBlack;
        private System.Windows.Forms.MenuItem menuImagePresetWhite;
        private System.Windows.Forms.MenuItem menuImagePresetGold;
        private System.Windows.Forms.MenuItem menuImagePresetLine;
        private System.Windows.Forms.MenuItem menuImagePresetsNone;
        private System.Windows.Forms.Button btnBackNet;
        private System.Windows.Forms.Label lblGamesNet;
        private System.Windows.Forms.ListBox lstGamesNet;
        private System.Windows.Forms.Panel panConnectNet;
        private System.Windows.Forms.TextBox txtRemoteHostNet;
        private System.Windows.Forms.Label lblRemoteHostNet;
        private System.Windows.Forms.Button btnCreateNet;
        private System.Windows.Forms.Button btnJoinNet;
        private System.Windows.Forms.Panel panGamesNet;
        private System.Windows.Forms.Timer tmrConnection;
        private System.Windows.Forms.TextBox txtJoinNameNet;
        private System.Windows.Forms.Label lblJoinNameNet;
        private System.Windows.Forms.TextBox txtGameNameNet;
        private System.Windows.Forms.TextBox txtPlayerNameNet;
        private System.Windows.Forms.TextBox txtFirstMoveNet;
        private System.Windows.Forms.CheckBox chkLockImagesNet;
        private System.Windows.Forms.ContextMenu menuChat;
        private System.Windows.Forms.MenuItem menuChatCopy;
        private System.Windows.Forms.MenuItem menuChatSelectAll;
        private System.Windows.Forms.MenuItem menuChatLine01;
        private System.Windows.Forms.MenuItem menuChatClear;
        private System.Windows.Forms.MenuItem menuChatLine02;
        private System.Windows.Forms.MenuItem menuImagePresetsBuiltIn;
        private System.Windows.Forms.ComboBox cmbAgent1P;
        private System.Windows.Forms.Label lblAgent1P;
    }
}
