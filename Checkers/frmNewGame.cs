using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
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
  
  enum ClientMessage : byte
  {
    Closed = 0,
    Header = 1,
    RefreshPlayerInfo = 2,
    RefreshNetImages = 3,
    ChatMessage = 4,
    BeginGame = 5,
  }
  
  public class frmNewGame : System.Windows.Forms.Form
  {
    private PictureBox selectedPicture;
    private CheckersGameType gameType;
    private int firstMove;
    private int difficulty;
    private Image [] imageSet;
    private string player1Name;
    private string player2Name;
    private TcpClient hostClient = null;
    private TcpListener clientListener = null;
    private TcpClientCollection clients = new TcpClientCollection();
    private bool isSelfPlayer = false;
    private TcpClient remotePlayer = null;
    
    #region Class Variables

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
    private System.Windows.Forms.Label lblDifficulty1P;
    private System.Windows.Forms.ComboBox cmbDifficulty1P;
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
    private Pabo.MozBar.MozPane mozGameType;
    private Pabo.MozBar.MozItem mozGameType1P;
    private Pabo.MozBar.MozItem mozGameType2P;
    private Pabo.MozBar.MozItem mozGameTypeNet;
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
    private System.Windows.Forms.MenuItem menuItem1;
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
    private System.ComponentModel.IContainer components;
    
    #endregion
    
    #region Class Construction
    
    public frmNewGame ()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      imageSet = new Image [4];
    }
    
    #region Windows Form Designer generated code
    
    /// <summary> Clean up any resources being used. </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }
    
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmNewGame));
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
      this.cmbDifficulty1P = new System.Windows.Forms.ComboBox();
      this.lblDifficulty1P = new System.Windows.Forms.Label();
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
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menuImagePresetRed = new System.Windows.Forms.MenuItem();
      this.menuImagePresetBlack = new System.Windows.Forms.MenuItem();
      this.menuImagePresetWhite = new System.Windows.Forms.MenuItem();
      this.menuImagePresetGold = new System.Windows.Forms.MenuItem();
      this.menuImagePresetLine = new System.Windows.Forms.MenuItem();
      this.menuImagePresetsNone = new System.Windows.Forms.MenuItem();
      this.menuImageBrowse = new System.Windows.Forms.MenuItem();
      this.menuImageChooseColor = new System.Windows.Forms.MenuItem();
      this.mozGameType = new Pabo.MozBar.MozPane();
      this.mozGameType1P = new Pabo.MozBar.MozItem();
      this.mozGameType2P = new Pabo.MozBar.MozItem();
      this.mozGameTypeNet = new Pabo.MozBar.MozItem();
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
      ((System.ComponentModel.ISupportInitialize)(this.mozGameType)).BeginInit();
      this.mozGameType.SuspendLayout();
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
                                                                            this.cmbDifficulty1P,
                                                                            this.lblDifficulty1P});
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
      // cmbDifficulty1P
      // 
      this.cmbDifficulty1P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDifficulty1P.Items.AddRange(new object[] {
                                                         "Beginner",
                                                         "Intermediate",
                                                         "Advanced",
                                                         "Expert"});
      this.cmbDifficulty1P.Location = new System.Drawing.Point(280, 132);
      this.cmbDifficulty1P.Name = "cmbDifficulty1P";
      this.cmbDifficulty1P.Size = new System.Drawing.Size(252, 21);
      this.cmbDifficulty1P.TabIndex = 1;
      // 
      // lblDifficulty1P
      // 
      this.lblDifficulty1P.Location = new System.Drawing.Point(280, 112);
      this.lblDifficulty1P.Name = "lblDifficulty1P";
      this.lblDifficulty1P.Size = new System.Drawing.Size(252, 20);
      this.lblDifficulty1P.TabIndex = 0;
      this.lblDifficulty1P.Text = "Difficulty:";
      this.lblDifficulty1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
      this.txtChat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtChat.Location = new System.Drawing.Point(0, 156);
      this.txtChat.Name = "txtChat";
      this.txtChat.ReadOnly = true;
      this.txtChat.Size = new System.Drawing.Size(288, 76);
      this.txtChat.TabIndex = 3;
      this.txtChat.Text = "";
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
                                                                                    this.menuItem1,
                                                                                    this.menuImagePresetLine,
                                                                                    this.menuImagePresetsNone});
      this.menuImagePreset.Text = "&Preset Image";
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 0;
      this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuImagePresetRed,
                                                                              this.menuImagePresetBlack,
                                                                              this.menuImagePresetWhite,
                                                                              this.menuImagePresetGold});
      this.menuItem1.Text = "&Built-in";
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
      // mozGameType
      // 
      this.mozGameType.BackColor = System.Drawing.SystemColors.Control;
      this.mozGameType.BorderColor = System.Drawing.Color.Black;
      this.mozGameType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
      this.mozGameType.ImageList = this.imlGameType;
      this.mozGameType.ItemBorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozGameType.ItemBorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.None;
      this.mozGameType.ItemBorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozGameType.ItemColors.Background = System.Drawing.SystemColors.Control;
      this.mozGameType.ItemColors.Border = System.Drawing.Color.Black;
      this.mozGameType.ItemColors.Divider = System.Drawing.Color.Black;
      this.mozGameType.ItemColors.FocusBackground = System.Drawing.SystemColors.Control;
      this.mozGameType.ItemColors.FocusBorder = System.Drawing.SystemColors.ControlDarkDark;
      this.mozGameType.ItemColors.SelectedBackground = System.Drawing.SystemColors.ControlLightLight;
      this.mozGameType.ItemColors.SelectedBorder = System.Drawing.SystemColors.ControlDark;
      this.mozGameType.ItemColors.Text = System.Drawing.Color.Black;
      this.mozGameType.Items.AddRange(new Pabo.MozBar.MozItem[] {
                                                                  this.mozGameType1P,
                                                                  this.mozGameType2P,
                                                                  this.mozGameTypeNet});
      this.mozGameType.Location = new System.Drawing.Point(4, 6);
      this.mozGameType.MaxSelectedItems = 1;
      this.mozGameType.Name = "mozGameType";
      this.mozGameType.Padding.Horizontal = 4;
      this.mozGameType.Padding.Vertical = 2;
      this.mozGameType.Size = new System.Drawing.Size(598, 44);
      this.mozGameType.Style = Pabo.MozBar.paneStyle.Horizontal;
      this.mozGameType.TabIndex = 0;
      this.mozGameType.TabStop = false;
      this.mozGameType.Toggle = false;
      // 
      // mozGameType1P
      // 
      this.mozGameType1P.Images.Focus = -1;
      this.mozGameType1P.Images.Normal = 0;
      this.mozGameType1P.Images.Selected = -1;
      this.mozGameType1P.ItemStyle = Pabo.MozBar.itemStyle.Picture;
      this.mozGameType1P.Location = new System.Drawing.Point(4, 2);
      this.mozGameType1P.Name = "mozGameType1P";
      this.mozGameType1P.Size = new System.Drawing.Size(40, 40);
      this.mozGameType1P.TabIndex = 0;
      this.mozGameType1P.TabStop = false;
      this.mozGameType1P.TextAlign = Pabo.MozBar.textAlign.Bottom;
      this.mozGameType1P.Click += new System.EventHandler(this.mozGameType1P_Click);
      // 
      // mozGameType2P
      // 
      this.mozGameType2P.Images.Focus = -1;
      this.mozGameType2P.Images.Normal = 1;
      this.mozGameType2P.Images.Selected = -1;
      this.mozGameType2P.ItemStyle = Pabo.MozBar.itemStyle.Picture;
      this.mozGameType2P.Location = new System.Drawing.Point(48, 2);
      this.mozGameType2P.Name = "mozGameType2P";
      this.mozGameType2P.Size = new System.Drawing.Size(40, 40);
      this.mozGameType2P.TabIndex = 1;
      this.mozGameType2P.TabStop = false;
      this.mozGameType2P.TextAlign = Pabo.MozBar.textAlign.Bottom;
      this.mozGameType2P.Click += new System.EventHandler(this.mozGameType2P_Click);
      // 
      // mozGameTypeNet
      // 
      this.mozGameTypeNet.Images.Focus = -1;
      this.mozGameTypeNet.Images.Normal = 2;
      this.mozGameTypeNet.Images.Selected = -1;
      this.mozGameTypeNet.ItemStyle = Pabo.MozBar.itemStyle.Picture;
      this.mozGameTypeNet.Location = new System.Drawing.Point(92, 2);
      this.mozGameTypeNet.Name = "mozGameTypeNet";
      this.mozGameTypeNet.Size = new System.Drawing.Size(40, 40);
      this.mozGameTypeNet.TabIndex = 2;
      this.mozGameTypeNet.TabStop = false;
      this.mozGameTypeNet.TextAlign = Pabo.MozBar.textAlign.Bottom;
      this.mozGameTypeNet.Click += new System.EventHandler(this.mozGameTypeNet_Click);
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
                                                                  this.mozGameType,
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
      ((System.ComponentModel.ISupportInitialize)(this.mozGameType)).EndInit();
      this.mozGameType.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    
    #endregion
    
    #endregion
    
    #region Public Properties
    
    public CheckersGameType GameType
    {
      get { return gameType; }
      set { gameType = value; }
    }
    
    public int FirstMove
    { get { return firstMove; } }
    
    public int Difficulty
    { get { return difficulty; } }
    
    public Image [] ImageSet
    { get { return imageSet; } }
    
    public string Player1Name
    {
      get { return player1Name; }
      set { player1Name = value; }
    }
    public string Player2Name
    {
      get { return player2Name; }
      set { player2Name = value; }
    }
    
    public TcpClient RemotePlayer
    {
      get { return remotePlayer; }
      set { remotePlayer = value; }
    }
    
    #endregion
    
    #region Image Set Functions
    
    private void OnCustomImageSet ()
    {
      if ((selectedPicture == picPawn1P1) || (selectedPicture == picKing1P1) || (selectedPicture == picPawn1P2) || (selectedPicture == picKing1P2)) cmbImageSet1P.SelectedIndex = 3;
      else if ((selectedPicture == picPawn2P1) || (selectedPicture == picKing2P1) || (selectedPicture == picPawn2P2) || (selectedPicture == picKing2P2)) cmbImageSet2P.SelectedIndex = 3;
    }
    
    private void SwapImageSet (PictureBox pic1, PictureBox pic2)
    {
      Image temp = pic1.Image; pic1.Image = pic2.Image; pic2.Image = temp;
      object tag = pic1.Tag; pic1.Tag = pic2.Tag; pic2.Tag = tag;
    }
    
    private void SetImageSet (ComboBox sender)
    {
      if (sender == cmbImageSet1P) SetImageSet(cmbImageSet1P.SelectedIndex, picPawn1P1, picKing1P1, picPawn1P2, picKing1P2);
      else if (sender == cmbImageSet2P) SetImageSet(cmbImageSet2P.SelectedIndex, picPawn2P1, picKing2P1, picPawn2P2, picKing2P2);
    }
    private void SetImageSet (int setIndex, PictureBox pawnPic1, PictureBox kingPic1, PictureBox pawnPic2, PictureBox kingPic2)
    {
      switch (setIndex)
      {
        case 0:
          pawnPic1.Image = imlImageSet.Images[0]; pawnPic1.Tag = Color.Firebrick; kingPic1.Image = imlImageSet.Images[1]; kingPic1.Tag = kingPic1.Image;
          pawnPic2.Image = imlImageSet.Images[2]; pawnPic1.Tag = Color.LightGray; kingPic2.Image = imlImageSet.Images[3]; kingPic2.Tag = kingPic2.Image;
          kingPic1.Visible = false; kingPic2.Visible = false;
          break;
        case 1:
          pawnPic1.Image = imlImageSet.Images[4]; pawnPic1.Tag = Color.DimGray; kingPic1.Image = imlImageSet.Images[5]; kingPic1.Tag = kingPic1.Image;
          pawnPic2.Image = imlImageSet.Images[2]; pawnPic1.Tag = Color.LightGray; kingPic2.Image = imlImageSet.Images[3]; kingPic2.Tag = kingPic2.Image;
          kingPic1.Visible = false; kingPic2.Visible = false;
          break;
        case 2:
          pawnPic1.Image = imlImageSet.Images[8]; pawnPic1.Tag = Color.Firebrick; kingPic1.Image = imlImageSet.Images[9]; kingPic1.Tag = kingPic1.Image;
          pawnPic2.Image = imlImageSet.Images[10]; pawnPic1.Tag = Color.LightGray; kingPic2.Image = imlImageSet.Images[11]; kingPic2.Tag = kingPic2.Image;
          kingPic1.Visible = false; kingPic2.Visible = false;
          break;
        default:
          kingPic1.Visible = true;
          kingPic2.Visible = true;
          break;
      }
    }
    
    private void SetDefaultImage ()
    {
      if (selectedPicture.Tag == null) return;
      if (selectedPicture == picKing1P1) picKing1P1.Image = (Image)picKing1P1.Tag;
      else if (selectedPicture == picKing1P2) picKing1P2.Image = (Image)picKing1P2.Tag;
      else if (selectedPicture == picKing2P1) picKing2P1.Image = (Image)picKing2P1.Tag;
      else if (selectedPicture == picKing2P2) picKing2P2.Image = (Image)picKing2P2.Tag;
      else if (selectedPicture == picKingNet1) { picKingNet1.Image = (Image)picKingNet1.Tag; RefreshNetImages(); }
      else if (selectedPicture == picKingNet2) { picKingNet2.Image = (Image)picKingNet2.Tag; RefreshNetImages(); }
      OnCustomImageSet();
    }
    private void BrowseCustomImage ()
    {
      if (selectedPicture == picPawn1P1) BrowseCustomPawn(picPawn1P1, picKing1P1);
      else if (selectedPicture == picKing1P1) BrowseCustomKing(picKing1P1);
      else if (selectedPicture == picPawn1P2) BrowseCustomPawn(picPawn1P2, picKing1P2);
      else if (selectedPicture == picKing1P2) BrowseCustomKing(picKing1P2);
      else if (selectedPicture == picPawn2P1) BrowseCustomPawn(picPawn2P1, picKing2P1);
      else if (selectedPicture == picKing2P1) BrowseCustomKing(picKing2P1);
      else if (selectedPicture == picPawn2P2) BrowseCustomPawn(picPawn2P2, picKing2P2);
      else if (selectedPicture == picKing2P2) BrowseCustomKing(picKing2P2);
      else if (selectedPicture == picPawnNet1) { BrowseCustomPawn(picPawnNet1, picKingNet1); RefreshNetImages(); }
      else if (selectedPicture == picKingNet1) { BrowseCustomKing(picKingNet1); RefreshNetImages(); }
      else if (selectedPicture == picPawnNet2) { BrowseCustomPawn(picPawnNet2, picKingNet2); RefreshNetImages(); }
      else if (selectedPicture == picKingNet2) { BrowseCustomKing(picKingNet2); RefreshNetImages(); }
      OnCustomImageSet();
    }
    private void BrowseCustomPawn (PictureBox pawnPic, PictureBox kingPic)
    {
      // Open pawn image
      dlgOpenImage.Title = "Open Custom Pawn Image";
      if (dlgOpenImage.ShowDialog(this) == DialogResult.Cancel) return;
      string pawnFileName = dlgOpenImage.FileName;
      // Create piece images
      Image pawn = new Bitmap(Image.FromFile(pawnFileName), 32, 32);
      Image king = new Bitmap(Image.FromFile(pawnFileName), 32, 32);
      DrawKingIcon(king);
      // Set custom pawn.king images
      pawnPic.Image = pawn; pawnPic.Tag = null;
      kingPic.Image = king; kingPic.Tag = kingPic.Image;
    }
    private void BrowseCustomKing (PictureBox kingPic)
    {
      // Open king image
      dlgOpenImage.Title = "Open Custom King Image";
      if (dlgOpenImage.ShowDialog(this) == DialogResult.Cancel) return;
      string kingFileName = dlgOpenImage.FileName;
      // Create piece images
      Image king = new Bitmap(Image.FromFile(kingFileName), 32, 32);
      // Set custom pawn.king images
      kingPic.Image = king;
    }
    
    private void ChooseCustomImageColor ()
    {
      if (selectedPicture == picPawn1P1) ChooseCustomPawnColor(picPawn1P1, picKing1P1);
      else if (selectedPicture == picKing1P1) ChooseCustomKingColor(picPawn1P1, picKing1P1);
      else if (selectedPicture == picPawn1P2) ChooseCustomPawnColor(picPawn1P2, picKing1P2);
      else if (selectedPicture == picKing1P2) ChooseCustomKingColor(picPawn1P1, picKing1P2);
      else if (selectedPicture == picPawn2P1) ChooseCustomPawnColor(picPawn2P1, picKing2P1);
      else if (selectedPicture == picKing2P1) ChooseCustomKingColor(picPawn2P1, picKing2P1);
      else if (selectedPicture == picPawn2P2) ChooseCustomPawnColor(picPawn2P2, picKing2P2);
      else if (selectedPicture == picKing2P2) ChooseCustomKingColor(picPawn2P1, picKing2P2);
      else if (selectedPicture == picPawnNet1) { ChooseCustomPawnColor(picPawnNet1, picKingNet1); RefreshNetImages(); }
      else if (selectedPicture == picKingNet1) { ChooseCustomKingColor(picPawnNet1, picKingNet1); RefreshNetImages(); }
      else if (selectedPicture == picPawnNet2) { ChooseCustomPawnColor(picPawnNet2, picKingNet2); RefreshNetImages(); }
      else if (selectedPicture == picKingNet2) { ChooseCustomKingColor(picPawnNet1, picKingNet2); RefreshNetImages(); }
      OnCustomImageSet();
    }
    private void ChooseCustomPawnColor (PictureBox pawnPic, PictureBox kingPic)
    {
      dlgSelectColor.Color = (( pawnPic.Tag != null )?( (Color)pawnPic.Tag ):( Color.Gray ));
      if (dlgSelectColor.ShowDialog(this) == DialogResult.Cancel) return;
      pawnPic.Image = CreatePieceImage(dlgSelectColor.Color, false); pawnPic.Tag = dlgSelectColor.Color;
      kingPic.Image = CreatePieceImage(dlgSelectColor.Color, true);  kingPic.Tag = kingPic.Image;
    }
    private void ChooseCustomKingColor (PictureBox pawnPic, PictureBox kingPic)
    {
      dlgSelectColor.Color = (( pawnPic.Tag != null )?( (Color)pawnPic.Tag ):( Color.Gray ));
      if (dlgSelectColor.ShowDialog(this) == DialogResult.Cancel) return;
      kingPic.Image = CreatePieceImage(dlgSelectColor.Color, true);
    }
    
    private void SetPresetImage (Image pawn, Image king, Color color)
    {
      if (selectedPicture == picPawn1P1) SetPresetPawn(picPawn1P1, picKing1P1, pawn, king, color);
      else if (selectedPicture == picKing1P1) SetPresetKing(picKing1P1, king);
      else if (selectedPicture == picPawn1P2) SetPresetPawn(picPawn1P2, picKing1P2, pawn, king, color);
      else if (selectedPicture == picKing1P2) SetPresetKing(picKing1P2, king);
      else if (selectedPicture == picPawn2P1) SetPresetPawn(picPawn2P1, picKing2P1, pawn, king, color);
      else if (selectedPicture == picKing2P1) SetPresetKing(picKing2P1, king);
      else if (selectedPicture == picPawn2P2) SetPresetPawn(picPawn2P2, picKing2P2, pawn, king, color);
      else if (selectedPicture == picKing2P2) SetPresetKing(picKing2P2, king);
      else if (selectedPicture == picPawnNet1) { SetPresetPawn(picPawnNet1, picKingNet1, pawn, king, color); RefreshNetImages(); }
      else if (selectedPicture == picKingNet1) { SetPresetKing(picKingNet1, king); RefreshNetImages(); }
      else if (selectedPicture == picPawnNet2) { SetPresetPawn(picPawnNet2, picKingNet2, pawn, king, color); RefreshNetImages(); }
      else if (selectedPicture == picKingNet2) { SetPresetKing(picKingNet2, king); RefreshNetImages(); }
      OnCustomImageSet();
    }
    private void SetPresetPawn (PictureBox pawnPic, PictureBox kingPic, Image pawn, Image king, Color color)
    {
      pawnPic.Image = pawn; pawnPic.Tag = (( color.IsEmpty )?( null ):( (object)color ));
      kingPic.Image = king; kingPic.Tag = kingPic.Image;
    }
    private void SetPresetKing (PictureBox kingPic, Image king)
    { kingPic.Image = king; }
    
    
    private void EnumPresetMenuItems ()
    { menuImagePresetsNone.Visible = (!EnumPresetMenuItems(menuImagePreset, "")); }
    private bool EnumPresetMenuItems (MenuItem item, string subPath)
    {
      string presetsPath = Path.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath) + "\\Presets" + subPath);
      if (!Directory.Exists(presetsPath)) return false;
      
      // Enumerate presets into the menu
      string [] presetPathList = Directory.GetDirectories(presetsPath);
      // Add sub-categories
      foreach (string presetPath in presetPathList)
      {
        if (Directory.GetDirectories(presetPath).Length > 0)
          EnumPresetMenuItems(item.MenuItems.Add(Path.GetFileName(presetPath)), subPath + "\\" + Path.GetFileName(presetPath));
      }
      // Add presets
      foreach (string presetPath in presetPathList)
      {
        if (Directory.GetDirectories(presetPath).Length == 0)
          item.MenuItems.Add(Path.GetFileName(presetPath), new EventHandler(menuImagePreset_Click));
      }
      return (presetPathList.Length != 0);
    }
    
    private Bitmap CreatePieceImage (Color color, bool isKing)
    {
      Bitmap pieceImage = new Bitmap(32, 32);
      pieceImage.MakeTransparent();
      Graphics g = Graphics.FromImage(pieceImage);
      Brush fillBrush = new SolidBrush(color);
      Pen ringColor = new Pen(Color.FromArgb( (( color.R+0x28 > 0xFF )?( 0xFF ):( color.R+0x28 )), (( color.G+0x28 > 0xFF )?( 0xFF ):( color.G+0x28 )), (( color.B+0x28 > 0xFF )?( 0xFF ):( color.B+0x28 )) ));
      g.FillEllipse(fillBrush, 2, 2, 32-5, 32-5);
      g.DrawEllipse(ringColor, 3, 3, 32-7, 32-7);
      g.DrawEllipse(Pens.Black, 2, 2, 32-5, 32-5);
      if (isKing) DrawKingIcon(g);
      ringColor.Dispose(); fillBrush.Dispose();
      g.Dispose();
      return pieceImage;
    }
    private void DrawKingIcon (Image image)
    {
      Graphics g = Graphics.FromImage(image);
      DrawKingIcon(g);
      g.Dispose();
    }
    
    private void DrawKingIcon (Graphics g)
    { g.DrawImage(imlKing.Images[0], 0, 0, 32, 32); }
    
    #endregion
    
    #region Image Control Functions
    
    private void menuImageDefault_Click(object sender, System.EventArgs e)
    { SetDefaultImage(); }
    private void menuImageBrowse_Click (object sender, System.EventArgs e)
    { BrowseCustomImage(); }
    private void menuImageChooseColor_Click (object sender, System.EventArgs e)
    { ChooseCustomImageColor(); }
    private void menuImagePresetRed_Click (object sender, System.EventArgs e)
    { SetPresetImage(imlImageSet.Images[0], imlImageSet.Images[1], Color.Firebrick); }
    private void menuImagePresetBlack_Click (object sender, System.EventArgs e)
    { SetPresetImage(imlImageSet.Images[4], imlImageSet.Images[5], Color.DimGray); }
    private void menuImagePresetWhite_Click (object sender, System.EventArgs e)
    { SetPresetImage(imlImageSet.Images[2], imlImageSet.Images[3], Color.LightGray); }
    private void menuImagePresetGold_Click (object sender, System.EventArgs e)
    { SetPresetImage(imlImageSet.Images[6], imlImageSet.Images[7], Color.Gold); }
    private void menuImagePreset_Click (object sender, System.EventArgs e)
    {
      // Load preset images
      string presetsPath = Path.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath) + "\\Presets");
      string presetPath = "";
      for (MenuItem item = (MenuItem)sender; item != menuImagePreset; item = (MenuItem)item.Parent)
        presetPath = "\\" + item.Text + presetPath;
      presetPath = presetsPath + presetPath;
      // Be sure preset still exists
      if (Directory.Exists(presetPath))
      {
        string pawnPreset = "", kingPreset = "";
        // Get the pawn and king filenames
        foreach (string file in Directory.GetFiles(presetPath))
        {
          if (Path.GetFileNameWithoutExtension(file).ToLower() == "pawn") pawnPreset = file;
          if (Path.GetFileNameWithoutExtension(file).ToLower() == "king") kingPreset = file;
        }
        if ((pawnPreset != "") && (kingPreset != ""))
        {
          Image pawn = null, king = null;
          try
          {
            // Load the preset image set
            pawn = new Bitmap(Image.FromFile(pawnPreset), 32, 32);
            king = new Bitmap(Image.FromFile(kingPreset), 32, 32);
          }
          catch (OutOfMemoryException)
          { MessageBox.Show(this, "One or both presets were not supported!", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error); }
          // Set the preset image set
          if ((pawn != null) && (king != null))
            SetPresetImage(pawn, king, Color.Empty);
        }
        else
        { MessageBox.Show(this, "Preset not properly set up!\n\nPreset directories should contain:\nAn image file named 'Pawn' and\nAn image file named 'King'", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error); }
      }
      else
      { MessageBox.Show(this, "Preset not found!", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    
    private void picImageSwap1P_Click (object sender, System.EventArgs e)
    { SwapImageSet(picPawn1P1, picPawn1P2); SwapImageSet(picKing1P1, picKing1P2); }
    private void picImageSwap2P_Click (object sender, System.EventArgs e)
    { SwapImageSet(picPawn2P1, picPawn2P2); SwapImageSet(picKing2P1, picKing2P2); }
    private void picImageSwapNet_Click (object sender, System.EventArgs e)
    {
      SwapImageSet(picPawnNet1, picPawnNet2);
      SwapImageSet(picKingNet1, picKingNet2);
      RefreshNetImages();
    }
    
    private void cmbImageSet_SelectedIndexChanged (object sender, System.EventArgs e)
    { SetImageSet((ComboBox)sender); }
    
    private void picImage_Click (object sender, System.EventArgs e)
    {
      PictureBox pictureBox = sender as PictureBox;
      selectedPicture = pictureBox;
      menuImage.Show(pictureBox.Parent, new Point(pictureBox.Left, pictureBox.Top+pictureBox.Height));
    }
    private void picImage_EnabledChanged (object sender, System.EventArgs e)
    { ((PictureBox)sender).BackColor = (( ((PictureBox)sender).Enabled )?( Color.White ):( Color.FromKnownColor(KnownColor.Control) )); }
    private void picImage_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
    { picImage_Click(sender, EventArgs.Empty); }
    private void menuImage_Popup (object sender, System.EventArgs e)
    {
      menuImageDefault.Visible = ((selectedPicture == picKing1P1) || (selectedPicture == picKing1P2) || (selectedPicture == picKing2P1) || (selectedPicture == picKing2P2) || (selectedPicture == picKingNet1) || (selectedPicture == picKingNet2));
      menuImageLine.Visible = menuImageDefault.Visible;
    }
    
    #endregion
    
    public new DialogResult ShowDialog (IWin32Window owner)
    {
      // Set control properties
      if ((player1Name != "") && (player1Name != "Player") && (player1Name != "Player 1")) txtPlayerName1P.Text = txtPlayerName2P1.Text = txtJoinNameNet.Text = player1Name;
      if ((player2Name != "") && (gameType == CheckersGameType.Multiplayer)) txtPlayerName2P2.Text = player2Name;
      // Set net player name if not already set
      if (txtJoinNameNet.Text == "")
      {
        string fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\RecentNet.ini";
        StreamReader fs = new StreamReader(( File.Exists(fileName) )?( File.OpenRead(fileName) ):( File.Create(fileName) ), Encoding.UTF8);
        txtJoinNameNet.Text = fs.ReadLine();
        fs.Close();
      }
      
      // Show dialog
      DialogResult result = base.ShowDialog(owner);
      
      // Set properties
      if (result != DialogResult.Cancel)
      {
        switch (tabGame.SelectedIndex)
        {
          case 0:
            gameType = CheckersGameType.SinglePlayer;
            firstMove = (( cmbFirstMove1P.SelectedIndex == 0 )?( 1 ):( 2 ));
            difficulty = (( cmbDifficulty1P.SelectedIndex == -1 )?( 0 ):( cmbDifficulty1P.SelectedIndex ));
            player1Name = (( txtPlayerName1P.Text.Trim() != "" )?( txtPlayerName1P.Text.Trim() ):( "Player" ));
            player2Name = "Opponent";
            imageSet[0] = picPawn1P1.Image; imageSet[1] = picKing1P1.Image;
            imageSet[2] = picPawn1P2.Image; imageSet[3] = picKing1P2.Image;
            break;
          case 1:
            gameType = CheckersGameType.Multiplayer;
            firstMove = (( cmbFirstMove2P.SelectedIndex == 0 )?( 1 ):( 2 ));
            difficulty = 0;
            player1Name = (( txtPlayerName2P1.Text.Trim() != "" )?( txtPlayerName2P1.Text.Trim() ):( "Player 1" ));
            player2Name = (( txtPlayerName2P2.Text.Trim() != "" )?( txtPlayerName2P2.Text.Trim() ):( "Player 2" ));
            imageSet[0] = picPawn2P1.Image; imageSet[1] = picKing2P1.Image;
            imageSet[2] = picPawn2P2.Image; imageSet[3] = picKing2P2.Image;
            break;
          case 2:
            gameType = CheckersGameType.NetGame;
            difficulty = 0;
            // Client
            if (remotePlayer != null)
            {
              try
              {
                BinaryReader br = new BinaryReader(new NetworkStream(remotePlayer.Socket, false));
                // Player 1 info
                player1Name = br.ReadString();
                firstMove = (( br.ReadBoolean() )?( 1 ):( 2 ));
                MemoryStream mem; int len;
                len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); imageSet[0] = Image.FromStream(mem);
                len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); imageSet[1] = Image.FromStream(mem);
                // Player 2 info
                player2Name = br.ReadString();
                len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); imageSet[2] = Image.FromStream(mem);
                len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); imageSet[3] = Image.FromStream(mem);
                br.Close();
                break;
              }
              catch (SocketException ex)
              { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
              catch (IOException ex)
              { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
              catch (InvalidOperationException ex)
              { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
              gameType = CheckersGameType.None;
              break;
            }
            // Server
            if (GetNetPlayerCount() < 2)
            {
              gameType = CheckersGameType.None;
              break;
            }
            
            // Set up server game
            TcpClient client1 = null, client2 = null;
            foreach (TcpClient client in clients)
            {
              if (client.Tag == null) continue;
              PlayerInfo info = ((PlayerInfo)client.Tag);
              if (info.IsPlayer)
              {
                if (client1 == null) client1 = client;
                else client2 = client;
                continue;
              }
            }
            
            // Two cases .. either server is a player or it is not
            if (isSelfPlayer)
            {
              // Player 1 (self) info
              player1Name = txtPlayerNameNet.Text;
              firstMove = (( player1Name == cmbFirstMoveNet.Text )?( 1 ):( 2 ));
              imageSet[0] = (( lblImageNet1.Text == player1Name )?( picPawnNet1.Image ):( picPawnNet2.Image ));
              imageSet[1] = (( lblImageNet1.Text == player1Name )?( picKingNet1.Image ):( picKingNet2.Image ));
              // Player 2 info
              PlayerInfo info = (PlayerInfo)client1.Tag;
              player2Name = info.Name;
              imageSet[2] = (( lblImageNet1.Text == player2Name )?( picPawnNet1.Image ):( picPawnNet2.Image ));
              imageSet[3] = (( lblImageNet1.Text == player2Name )?( picKingNet1.Image ):( picKingNet2.Image ));
              remotePlayer = client1;
              foreach (TcpClient client in clients)
              { if (client1 == client) { clients.Remove(client); break; } }
            }
            else
            {
              // Get first player
              if (((PlayerInfo)client1.Tag).Name != cmbFirstMoveNet.Text)
              { TcpClient temp = client1; client1 = client2; client2 = temp; }
              PlayerInfo info1 = (PlayerInfo)client1.Tag;
              PlayerInfo info2 = (PlayerInfo)client2.Tag;
              // Player 1 info
              player1Name = info1.Name;
              firstMove = (( player1Name == cmbFirstMoveNet.Text )?( 1 ):( 2 ));
              imageSet[0] = (( lblImageNet1.Text == player1Name )?( picPawnNet1.Image ):( picPawnNet2.Image ));
              imageSet[1] = (( lblImageNet1.Text == player1Name )?( picKingNet1.Image ):( picKingNet2.Image ));
              // Player 2 info
              player1Name = info2.Name;
              imageSet[2] = (( lblImageNet1.Text == player2Name )?( picPawnNet1.Image ):( picPawnNet2.Image ));
              imageSet[3] = (( lblImageNet1.Text == player2Name )?( picKingNet1.Image ):( picKingNet2.Image ));
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
    
    
    
    private void frmNewGame_Load (object sender, System.EventArgs e)
    {
      // Add tournament images
      imlImageSet.Images.Add(CreatePieceImage(Color.Firebrick, false));
      imlImageSet.Images.Add(CreatePieceImage(Color.Firebrick, true));
      imlImageSet.Images.Add(CreatePieceImage(Color.LightGray, false));
      imlImageSet.Images.Add(CreatePieceImage(Color.LightGray, true));
      
      // Load presets
      EnumPresetMenuItems();
      
      // Select initial tab
      mozGameType.Items[0].SelectItem();
      
      // Set initial combobox values
      cmbFirstMove1P.SelectedIndex = 0;
      cmbImageSet1P.SelectedIndex = 0;
      cmbDifficulty1P.SelectedIndex = 0;
      
      cmbFirstMove2P.SelectedIndex = 0;
      cmbImageSet2P.SelectedIndex = 0;
      
      cmbNetGameType.SelectedIndex = 0;
      cmbFirstMoveNet.SelectedIndex = 0;
      SetImageSet(0, picPawnNet1, picKingNet1, picPawnNet2, picKingNet2);
      picKingNet1.Visible = true; picKingNet2.Visible = true;
      panNet.BringToFront();
      
      // Select initial control
      cmbDifficulty1P.Select();
    }
    
    private void frmNewGame_Closing (object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (DialogResult != DialogResult.OK) return;
      switch (tabGame.SelectedIndex)
      {
        case 0:
          break;
        case 1:
          if ((txtPlayerName2P1.Text.Trim() != "") &&(txtPlayerName2P1.Text.ToLower().Trim() == txtPlayerName2P2.Text.ToLower().Trim()))
          {
            MessageBox.Show(this, "Player names must not be the same", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtPlayerName2P2.SelectAll(); txtPlayerName2P2.Select();
          }
          else break;
          e.Cancel = true;
          break;
        case 2:
          if (remotePlayer != null) break;
          if (panNet.Visible)
            MessageBox.Show(this, "You must create or join a game before you can play", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
          else if ((hostClient != null) || (clientListener == null))
            MessageBox.Show(this, "Only the owner of this Net Game may begin the game", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
          else if (GetNetPlayerCount() < 2)
            MessageBox.Show(this, "Cannot start a net game without first selecting two players", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
          else if (GetNetPlayerCount() == 2)
          {
            // Server now must send game info to all connected clients (but not to self, even if self is playing)
            foreach (TcpClient client in clients)
            {
              if (client.Tag == null) continue;
              PlayerInfo info = ((PlayerInfo)client.Tag);
              try
              {
                BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
                bw.Write((byte)ClientMessage.BeginGame);
                if (!info.IsPlayer)
                {
                  bw.Write(false);          // NOT playing; just exit
                  bw.Close(); continue;
                }
                bw.Write(true);             // Playing; continue with game data
                // Write player 1 data
                bw.Write(info.Name);
                bw.Write(info.Name == cmbFirstMoveNet.Text);    // Has first move?
                MemoryStream mem;
                Image pawn = (( lblImageNet1.Text == info.Name )?( picPawnNet1.Image ):( picPawnNet2.Image ));
                Image king = (( lblImageNet1.Text == info.Name )?( picKingNet1.Image ):( picKingNet2.Image ));
                mem = new MemoryStream(); pawn.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
                mem = new MemoryStream(); king.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
                // Either server is a player or it is not
                if (isSelfPlayer)
                {
                  // Write player 2 data
                  bw.Write(txtPlayerNameNet.Text);
                  pawn = (( lblImageNet1.Text == txtPlayerNameNet.Text )?( picPawnNet1.Image ):( picPawnNet2.Image ));
                  king = (( lblImageNet1.Text == txtPlayerNameNet.Text )?( picKingNet1.Image ):( picKingNet2.Image ));
                  mem = new MemoryStream(); pawn.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
                  mem = new MemoryStream(); king.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
                }
                else
                {
                  foreach (TcpClient client2 in clients)
                  {
                    if (client2 == client) continue;
                    if (client2.Tag == null) continue;
                    PlayerInfo info2 = ((PlayerInfo)client2.Tag);
                    if (!info2.IsPlayer) continue;
                    // Write player 2 data
                    bw.Write(info2.Name);
                    pawn = (( lblImageNet1.Text == info2.Name )?( picPawnNet1.Image ):( picPawnNet2.Image ));
                    king = (( lblImageNet1.Text == info2.Name )?( picKingNet1.Image ):( picKingNet2.Image ));
                    mem = new MemoryStream(); pawn.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
                    mem = new MemoryStream(); king.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
                  }
                }
                bw.Close();
                return;
              }
              catch (SocketException ex)
              { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
              catch (IOException ex)
              { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
              catch (InvalidOperationException ex)
              { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
              e.Cancel = true;
              break;
            }
          }
          else break;
          e.Cancel = true;
          break;
      }
    }
    
    
    private void tabGame_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if (tabGame.SelectedIndex == -1) return;
      mozGameType.Items[tabGame.SelectedIndex].SelectItem();
      
      CloseNetGame();
      switch (tabGame.SelectedIndex)
      {
        case 0: cmbDifficulty1P.Select(); break;
        case 1: txtPlayerName2P1.Select(); break;
        case 2:
          lnkIPAddress.Text = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
          cmbNetGameType.Select();
          break;
      }
    }
    private void mozGameType1P_Click (object sender, System.EventArgs e)
    { tabGame.SelectedTab = tabGame1P; }
    private void mozGameType2P_Click (object sender, System.EventArgs e)
    { tabGame.SelectedTab = tabGame2P; }
    private void mozGameTypeNet_Click (object sender, System.EventArgs e)
    { tabGame.SelectedTab = tabGameNet; }
    
    private void frmNewGame_Activated (object sender, System.EventArgs e)
    {
      if ((tabGame.SelectedIndex == 2) && (panNetSettings.Visible))
      { txtChat.Select(); txtSend.Select(); }
    }
    
    private void LoadRecentGames ()
    {
      lstGamesNet.Items.Clear();
      // Load recent games list
      string fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\RecentNet.ini";
      StreamReader fs = new StreamReader(( File.Exists(fileName) )?( File.OpenRead(fileName) ):( File.Create(fileName) ), Encoding.UTF8);
      fs.ReadLine();          // Read player name
      while (fs.Peek() != -1)
      {
        string line = fs.ReadLine().Trim();
        if (line == "") continue;
        lstGamesNet.Items.Add(line);
      }
      fs.Close();
    }
    private void SaveRecentGames ()
    {
      if (cmbNetGameType.SelectedIndex == 1) return;
      // Save recent games and player name
      string fileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\RecentNet.ini";
      StreamWriter fs = File.CreateText(fileName);
      fs.WriteLine(txtPlayerNameNet.Text);
      bool inList = false;
      // Append list of hosts
      string remoteHost = txtRemoteHostNet.Text.Trim();
      foreach (string item in lstGamesNet.Items)
      {
        if (remoteHost == item) inList = true;
        fs.WriteLine(item);
      }
      if ((!inList) && (txtRemoteHostNet.Text != "")) fs.WriteLine(remoteHost);
      fs.Close();
    }
    
    #region Net Game Control Functions
    
    private void tmrConnection_Tick (object sender, System.EventArgs e)
    { CheckSockets(); }
    
    private void cmbNetGameType_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      switch (cmbNetGameType.SelectedIndex)
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
    
    private void lnkIPAddress_LinkClicked (object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    { Clipboard.SetDataObject(lnkIPAddress.Text, true); }
    
    private void panNet_Enter (object sender, System.EventArgs e)
    { AcceptButton = btnJoinNet; }
    private void panNet_Leave (object sender, System.EventArgs e)
    { AcceptButton = btnOK; }
    private void panNetSettings_Enter (object sender, System.EventArgs e)
    { CancelButton = btnBackNet; btnBackNet.DialogResult = DialogResult.None; }
    private void panNetSettings_Leave (object sender, System.EventArgs e)
    { CancelButton = btnCancel; btnCancel.DialogResult = DialogResult.Cancel; }
    
    private void txtSend_Enter (object sender, System.EventArgs e)
    { AcceptButton = btnSend; }
    private void txtSend_Leave (object sender, System.EventArgs e)
    { AcceptButton = btnOK; }
    
    private void lstGamesNet_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if ((cmbNetGameType.SelectedIndex == 0) && (lstGamesNet.SelectedIndex != -1))
        txtRemoteHostNet.Text = lstGamesNet.SelectedItem.ToString();
    }
    
    private void btnCreateNet_Click (object sender, System.EventArgs e)
    {
      if (txtJoinNameNet.Text.Trim() == "")
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
      catch (SocketException ex)
      {
        CloseNetGame();
        MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      
      isSelfPlayer = true;
      txtPlayerNameNet.Text = txtJoinNameNet.Text.Trim();
      lstPlayersNet.CheckBoxes = true;
      txtGameNameNet.Text = txtPlayerNameNet.Text + "'s Game";
      lblImageNet1.Text = "You"; lblImageNet2.Text = "Opponent";
      cmbFirstMoveNet.Items.Clear();
      lstPlayersNet.Items.Clear();
      cmbFirstMoveNet.Visible = true; txtFirstMoveNet.Visible = false;
      picPawnNet1.Enabled = true; picKingNet1.Enabled = true;
      picPawnNet2.Enabled = true; picKingNet2.Enabled = true;
      chkLockImagesNet.Visible = true;
      picImageSwapNet.Visible = true;
      // Save recent games and new player name
      SaveRecentGames();
      
      panNetSettings.Show();
      panNetSettings.BringToFront();
      panNet.Hide();
      AppendMessage("", "*** Game created");
      RefreshPlayerInfo(); RefreshNetImages();
      tmrConnection.Start();
      txtSend.Select();
    }
    
    private void btnJoinNet_Click (object sender, System.EventArgs e)
    {
      CloseNetSockets();
      
      switch (cmbNetGameType.SelectedIndex)
      {
        case 0:
          if (txtRemoteHostNet.Text == "")
          {
            MessageBox.Show(this, "Remote host must not be empty", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtRemoteHostNet.Select();
            return;
          }
          
          try
          { hostClient = new TcpClient(txtRemoteHostNet.Text, CheckersSettings.Port); }
          catch (SocketException ex)
          {
            MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtRemoteHostNet.SelectAll(); txtRemoteHostNet.Select();
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
      catch (SocketException ex)
      { MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
      catch (IOException ex)
      { MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
      catch (InvalidOperationException ex)
      { MessageBox.Show(this, ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
      
      txtPlayerNameNet.Text = txtJoinNameNet.Text.Trim();
      lstPlayersNet.CheckBoxes = false;
      tmrConnection.Start();
      
      lstPlayersNet.Items.Clear();
      lblImageNet1.Text = "You"; lblImageNet2.Text = "Opponent";
      cmbFirstMoveNet.Items.Clear();
      cmbFirstMoveNet.Visible = false; txtFirstMoveNet.Visible = true;
      picPawnNet1.Enabled = false; picKingNet1.Enabled = false;
      picPawnNet2.Enabled = false; picKingNet2.Enabled = false;
      chkLockImagesNet.Visible = false;
      picImageSwapNet.Visible = false;
      
      panNetSettings.Show();
      panNetSettings.BringToFront();
      panNet.Hide();
      AppendMessage("", "Entered room");
      txtSend.Select();
    }
    
    private void btnBackNet_Click (object sender, System.EventArgs e)
    {
      CloseNetGame();
      cmbNetGameType_SelectedIndexChanged(cmbNetGameType, EventArgs.Empty);
    }
    
    private void btnSend_Click (object sender, System.EventArgs e)
    {
      if (txtSend.Text.Trim() == "") return;
      
      if (hostClient != null)
      {
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
          bw.Write((byte)ClientMessage.ChatMessage);
          bw.Write(txtPlayerNameNet.Text);
          bw.Write(txtSend.Text.Trim());
          bw.Close();
        }
        catch (IOException) {}
        catch (SocketException) {}
        catch (InvalidOperationException) {}
      }
      
      foreach (TcpClient client in clients)
      {
        if (client.Tag == null) continue;
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
          bw.Write((byte)ClientMessage.ChatMessage);
          bw.Write(txtPlayerNameNet.Text);
          bw.Write(txtSend.Text.Trim());
          bw.Close();
        }
        catch (IOException) {}
        catch (SocketException) {}
        catch (InvalidOperationException) {}
      }
      
      AppendMessage(txtPlayerNameNet.Text, txtSend.Text);
      txtSend.Text = ""; txtSend.Select();
    }
    
    private void cmbFirstMoveNet_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if (cmbFirstMoveNet.SelectedIndex == -1) return;
      if (clientListener != null)
      { RefreshPlayerInfo(); }
    }
    private void chkLockImagesNet_CheckedChanged (object sender, System.EventArgs e)
    { if (clientListener != null) RefreshPlayerInfo(); }
    
    private void lstPlayersNet_ItemCheck (object sender, System.Windows.Forms.ItemCheckEventArgs e)
    {
      if (clientListener == null) return;
      if (e.CurrentValue == e.NewValue) return;
      if (refreshingPlayerInfo) return;
      
      ListViewItem item = lstPlayersNet.Items[e.Index];
      bool newIsPlayer = (e.NewValue != CheckState.Unchecked);
      // Be sure there are still players to become available
      if (item.Text == txtPlayerNameNet.Text)
      {
        e.NewValue = CheckState.Checked;
        MessageBox.Show(this, "Host must be a player", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      if ((newIsPlayer == true) && (GetNetPlayerCount() >= 2))
      {
        e.NewValue = CheckState.Unchecked;
        MessageBox.Show(this, "Cannot have more than two players", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      
      // Update player info
      if (item.Text == txtPlayerNameNet.Text)
      { isSelfPlayer = newIsPlayer; }
      else
      {
        TcpClient client = GetClientByName(item.Text);
        if ((client == null) || (client.Tag == null)) return;
        PlayerInfo info = (PlayerInfo)client.Tag;
        info.IsPlayer = (e.NewValue != CheckState.Unchecked);
        client.Tag = info;
      }
      // Refresh settings
      RefreshPlayerInfo();
    }
    
    #endregion
    
    #region Net Game Socket Functions
    
    bool checkingSockets = false;
    private void CheckSockets ()
    {
      if (checkingSockets) return;
      checkingSockets = true;
      
      // Check sockets for as long as there is data
      bool doCheck = true;
      while (doCheck)
      {
        doCheck = false;
        if (clients.Count > 0)
        {
          for (int i = 0; i < clients.Count; i++)
          {
            string closedName = "", message = "";
            try
            {
              if (CheckClientForMessage(clients[i])) doCheck = true;
              continue;
            }
            catch (IOException)
            {
              closedName = (( clients[i].Tag != null )?( ((PlayerInfo)clients[i].Tag).Name ):( "" ));
              message = "";
            }
            catch (SocketException ex)
            {
              closedName = (( clients[i].Tag != null )?( ((PlayerInfo)clients[i].Tag).Name ):( "" ));
              message = closedName + " disconnected unexpectedly:\n\n" + ex.Message;
            }
            catch (InvalidOperationException ex)
            {
              closedName = (( clients[i].Tag != null )?( ((PlayerInfo)clients[i].Tag).Name ):( "" ));
              message = closedName + " disconnected unexpectedly:\n\n" + ex.Message;
            }
            clients[i].Close(); clients.RemoveAt(i); i--;
            if (closedName != "") BroadcastMessage("", "*** " + closedName + " has left the room", null);
            RefreshPlayerInfo();
            if (message != "") MessageBox.Show(this, message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
      
        if (hostClient != null)
        {
          try
          { if (CheckHostForMessage()) doCheck = true; }
          catch (SocketException ex)
          { CloseNetGame(); MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
          catch (IOException ex)
          { CloseNetGame(); MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
          catch (InvalidOperationException ex)
          { CloseNetGame(); MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
      
        if (clientListener != null)
        {
          if (clientListener.Pending())
          {
            // Add to clients, in an uninitialized state (tag = null)
            clients.Add(new TcpClient(clientListener.AcceptSocket()));
          }
        }
        Application.DoEvents();
      }
      
      checkingSockets = false;
    }
    
    private bool CheckHostForMessage ()
    {
      NetworkStream ns = new NetworkStream(hostClient.Socket, false);
      if (!ns.DataAvailable) return false;
      BinaryReader br = new BinaryReader(ns);
      BinaryWriter bw = new BinaryWriter(ns);
      switch ((ClientMessage)br.ReadByte())
      {
        case ClientMessage.Header:
          string hostName = br.ReadString();
          txtGameNameNet.Text = hostName + "'s Game";
          SaveRecentGames();
          break;
        case ClientMessage.Closed:
          int closeReason = -1;
          if (ns.DataAvailable) closeReason = (int)br.ReadByte();
          CloseNetGame();
          // Show reason for close
          if (true)     // Workaround Visual Studio's text reformat visual error
          {
            switch (closeReason)
            {
              case 0: MessageBox.Show(this, "Disconnected by remote host", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
              case 1: MessageBox.Show(this, "Checkers versions do not match", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
              case 2: MessageBox.Show(this, "Name is already in use", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
              default: MessageBox.Show(this, "Unexpectedly disconnected from host:\n\nDisconnect message sent from host without a reason", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
            }
          }
          break;
        case ClientMessage.RefreshPlayerInfo:
          isSelfPlayer = false;
          lstPlayersNet.BeginUpdate();
          lstPlayersNet.Items.Clear();
          int count = 0;
          string playerName1 = "", playerName2 = "";
          while (br.ReadBoolean())
          {
            PlayerInfo info = new PlayerInfo();
            info.Name = br.ReadString();
            info.IsPlayer = br.ReadBoolean();
            ListViewItem item = new ListViewItem(new string [] { info.Name, (( info.IsPlayer )?( "Player" ):( (( count == 0 )?( "Dedicated Server" ):( "--" )) )) });
            lstPlayersNet.Items.Add(item);
            // Get player name
            if (info.IsPlayer)
            {
              if (playerName1 == "") playerName1 = info.Name;
              else playerName2 = info.Name;
            }
            // See if self is playing
            if ((info.IsPlayer) && (info.Name == txtPlayerNameNet.Text)) isSelfPlayer = true;
            count++;
          }
          lstPlayersNet.EndUpdate();
          txtFirstMoveNet.Text = br.ReadString();
          if (isSelfPlayer) panImageSetNet.Enabled = true;
          bool lockedImages = br.ReadBoolean();
          
          // Decide who is displayed first
          if (playerName2 == txtFirstMoveNet.Text)
          { string temp = playerName1; playerName1 = playerName2; playerName2 = temp; }
          
          // Show correct Custom Images order and enable if player
          lblImageNet1.Text = playerName1;
          lblImageNet2.Text = playerName2;
          picPawnNet1.Enabled = picKingNet1.Enabled = ((!lockedImages) && (isSelfPlayer) && (playerName1 == txtPlayerNameNet.Text));
          picPawnNet2.Enabled = picKingNet2.Enabled = ((!lockedImages) && (isSelfPlayer) && (playerName2 == txtPlayerNameNet.Text));
          break;
        case ClientMessage.RefreshNetImages:
          MemoryStream mem; int len;
          len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); picPawnNet1.Image = Image.FromStream(mem);
          len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); picKingNet1.Image = Image.FromStream(mem);
          len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); picPawnNet2.Image = Image.FromStream(mem);
          len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); picKingNet2.Image = Image.FromStream(mem);
          break;
        case ClientMessage.ChatMessage:
          string name = br.ReadString();
          string message = br.ReadString();
          AppendMessage(name, message);
          break;
        case ClientMessage.BeginGame:
          // Attact to client and close the form
          if (!br.ReadBoolean()) throw new IOException("Game started; disconnected from host");
          remotePlayer = hostClient;
          hostClient = null;
          // Close the form
          DialogResult = DialogResult.OK;
          this.Close();
          break;
        default:
          throw new IOException("An unknown message was received");
      }
      br.Close(); bw.Close();
      return true;
    }
    
    private bool CheckClientForMessage (TcpClient client)
    {
      NetworkStream ns = new NetworkStream(client.Socket, false);
      if (!ns.DataAvailable) return false;
      BinaryReader br = new BinaryReader(ns);
      BinaryWriter bw = new BinaryWriter(ns);
      PlayerInfo info;
      switch ((ClientMessage)br.ReadByte())
      {
        case ClientMessage.Header:
          if (client.Tag != null) throw new IOException("A bad message was received");
          if (!((new string(br.ReadChars(8)) == "CHECKERS") && (br.ReadByte() == 1)))
          {
            bw.Write((byte)ClientMessage.Closed); bw.Write((byte)1); bw.Close();
            throw new IOException();
          }
          info = new PlayerInfo();
          info.Name = br.ReadString().Trim();
          bool validName = (txtPlayerNameNet.Text.ToLower() != info.Name.ToLower());
          foreach (TcpClient checkClient in clients)
          {
            if ((checkClient.Tag != null) && ((PlayerInfo)checkClient.Tag).Name.ToLower() == info.Name.ToLower())
            { validName = false; break; }
          }
          if (!validName)
          {
            bw.Write((byte)ClientMessage.Closed); bw.Write((byte)2); bw.Close();
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
          if (client.Tag == null) throw new IOException("A wrong message was received");
          info = ((PlayerInfo)client.Tag);
          Image pawn, king;
          MemoryStream mem; int len;
          len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); pawn = Image.FromStream(mem);
          len = br.ReadInt32(); mem = new MemoryStream(br.ReadBytes(len)); king = Image.FromStream(mem);
          if ((!chkLockImagesNet.Checked) && (info.IsPlayer))
          {
            if (info.Name == cmbFirstMoveNet.Text)
            { picPawnNet1.Image = pawn; picKingNet1.Image = king; picKingNet1.Tag = king; }
            else
            { picPawnNet2.Image = pawn; picKingNet2.Image = king; picKingNet2.Tag = king; }
          }
          RefreshNetImages();
          break;
        case ClientMessage.ChatMessage:
          string name = br.ReadString();
          string message = br.ReadString();
          BroadcastMessage(name, message, client);
          break;
        case ClientMessage.BeginGame:
          throw new IOException("A wrong message was received");
        default:
          throw new IOException("An unknown message was received");
      }
      br.Close(); bw.Close();
      return true;
    }
    
    #endregion
    
    #region Net Game General Functions
    
    private int GetNetPlayerCount ()
    {
      int playerCount = 0;
      if (isSelfPlayer) playerCount++;
      foreach (TcpClient client in clients)
      { if ((client.Tag != null) && (((PlayerInfo)client.Tag).IsPlayer)) playerCount++; }
      return playerCount;
    }
    private int GetClientCount ()
    {
      int count = 0;
      foreach (TcpClient client in clients)
      { if (client.Tag != null) count++; }
      return count;
    }
    private TcpClient GetClientByName (string name)
    {
      foreach (TcpClient client in clients)
      { if ((client.Tag != null) && (((PlayerInfo)client.Tag).Name == name)) return client; }
      return null;
    }
    private string GetPlayer1Name ()
    {
      if ((isSelfPlayer) && (txtPlayerNameNet.Text == cmbFirstMoveNet.Text)) return txtPlayerNameNet.Text;
      foreach (TcpClient client in clients)
      {
        if ((client.Tag == null) || (!((PlayerInfo)client.Tag).IsPlayer)) continue;
        if (((PlayerInfo)client.Tag).Name == cmbFirstMoveNet.Text) return ((PlayerInfo)client.Tag).Name;
      }
      return "";
    }
    private string GetPlayer2Name ()
    {
      if ((isSelfPlayer) && (txtPlayerNameNet.Text != cmbFirstMoveNet.Text)) return txtPlayerNameNet.Text;
      foreach (TcpClient client in clients)
      {
        if ((client.Tag == null) || (!((PlayerInfo)client.Tag).IsPlayer)) continue;
        if (((PlayerInfo)client.Tag).Name != cmbFirstMoveNet.Text) return ((PlayerInfo)client.Tag).Name;
      }
      return "";
    }
    
    private void BroadcastMessage (string name, string message, TcpClient exceptClient)
    {
      AppendMessage(name, message);
      foreach (TcpClient client in clients)
      {
        if ((exceptClient != null) && (client == exceptClient)) continue;          // Do not send message to sender
        if (client.Tag == null) continue;
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
          bw.Write((byte)ClientMessage.ChatMessage);
          bw.Write(name);
          bw.Write(message);
          bw.Close();
        }
        catch (IOException) {}
        catch (SocketException) {}
        catch (InvalidOperationException) {}
      }
    }
    
    private void AppendMessage (string name, string message)
    {
      txtChat.Select(txtChat.TextLength, 0);
      if (name == "")
      {
        txtChat.SelectionColor = Color.Red;
        txtChat.AppendText("[" + DateTime.Now.ToShortTimeString() + "] ");
      }
      else if (name == txtPlayerNameNet.Text)
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
      txtChat.Select();
      txtChat.AppendText("\n");
      txtChat.ScrollToCaret();
      activeControl.Select();
    }
    
    bool refreshingPlayerInfo = false;
    private void RefreshPlayerInfo ()
    {
      if (clientListener == null) return;
      if (refreshingPlayerInfo) return;
      refreshingPlayerInfo = true;
      
      string sFirstMove = cmbFirstMoveNet.Text;
      lstPlayersNet.Items.Clear();
      cmbFirstMoveNet.Items.Clear();
      ListViewItem item = new ListViewItem(new string [] { txtPlayerNameNet.Text, ( isSelfPlayer )?( "Player" ):( "--" ) });
      item.Checked = isSelfPlayer;
      if (isSelfPlayer) cmbFirstMoveNet.Items.Add(txtPlayerNameNet.Text);
      lstPlayersNet.Items.Add(item);
      
      // Get data from client info
      for (int i = 0; i < clients.Count; i++)
      {
        TcpClient client = clients[i];
        if (client.Tag == null) continue;
        PlayerInfo info = (PlayerInfo)client.Tag;
        item = new ListViewItem(new string [] { info.Name, ( info.IsPlayer )?( "Player" ):( "--" ) });
        item.Checked = info.IsPlayer;
        if (info.IsPlayer) cmbFirstMoveNet.Items.Add(info.Name);
        lstPlayersNet.Items.Add(item);
      }
      cmbFirstMoveNet.SelectedIndex = cmbFirstMoveNet.Items.IndexOf(sFirstMove);
      if ((cmbFirstMoveNet.SelectedIndex == -1) && (cmbFirstMoveNet.Items.Count > 0))
        cmbFirstMoveNet.SelectedIndex = 0;
      
      lblImageNet1.Text = GetPlayer1Name();
      lblImageNet2.Text = GetPlayer2Name();
      
      // Write data to clients
      for (int i = 0; i < clients.Count; i++)
      {
        TcpClient client = clients[i];
        if (client.Tag == null) continue;
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
          bw.Write((byte)ClientMessage.RefreshPlayerInfo);
          bw.Write(true);
          bw.Write(txtPlayerNameNet.Text);
          bw.Write(isSelfPlayer);
          for (int n = 0; n < clients.Count; n++)
          {
            if (clients[n].Tag == null) continue;
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
        catch (SocketException ex)
        { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        catch (IOException ex)
        { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        catch (InvalidOperationException ex)
        { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        client.Close(); clients.Remove(client); i--;
      }
      refreshingPlayerInfo = false;
    }
    
    private void RefreshNetImages ()
    {
      if (hostClient != null)
      {
        Image pawn = (( lblImageNet1.Text == txtPlayerNameNet.Text )?( picPawnNet1.Image ):( picPawnNet2.Image ));
        Image king = (( lblImageNet1.Text == txtPlayerNameNet.Text )?( picKingNet1.Image ):( picKingNet2.Image ));
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
          bw.Write((byte)ClientMessage.RefreshNetImages);
          MemoryStream mem;
          mem = new MemoryStream(); pawn.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
          mem = new MemoryStream(); king.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
          bw.Close();
          return;
        }
        catch (SocketException ex)
        { CloseNetGame(); MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        catch (IOException ex)
        { CloseNetGame(); MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        catch (InvalidOperationException ex)
        { CloseNetGame(); MessageBox.Show(this, "Unexpectedly disconnected from host:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        return;
      }
      
      if (clientListener == null) return;
      // Write data to clients
      for (int i = 0; i < clients.Count; i++)
      {
        TcpClient client = clients[i];
        if (client.Tag == null) continue;
        PlayerInfo info = (PlayerInfo)client.Tag;
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(client.Socket, false));
          bw.Write((byte)ClientMessage.RefreshNetImages);
          MemoryStream mem;
          mem = new MemoryStream(); picPawnNet1.Image.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
          mem = new MemoryStream(); picKingNet1.Image.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
          mem = new MemoryStream(); picPawnNet2.Image.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
          mem = new MemoryStream(); picKingNet2.Image.Save(mem, ImageFormat.Png); bw.Write((int)mem.Length); bw.Write(mem.ToArray());
          bw.Close();
          continue;
        }
        catch (SocketException ex)
        { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        catch (IOException ex)
        { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        catch (InvalidOperationException ex)
        { MessageBox.Show(this, "Client disconnected unexpectedly:\n\n" + ex.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        client.Close(); clients.Remove(client); i--;
      }
    }
    
    private void CloseNetSockets ()
    {
      tmrConnection.Stop(); checkingSockets = false;
      if (hostClient != null)
      {
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(hostClient.Socket, false));
          bw.Write((byte)ClientMessage.Closed);
          bw.Close();
        }
        catch (SocketException) {}
        catch (IOException) {}
        catch (InvalidOperationException) {}
        hostClient.Close(); hostClient = null;
      }
      if (clientListener != null) { clientListener.Stop(); clientListener = null; }
      while (clients.Count > 0)
      {
        try
        {
          BinaryWriter bw = new BinaryWriter(new NetworkStream(clients[0].Socket, false));
          bw.Write((byte)ClientMessage.Closed);
          bw.Write((byte)0);
          bw.Close();
        }
        catch (SocketException) {}
        catch (IOException) {}
        catch (InvalidOperationException) {}
        clients[0].Close(); clients.RemoveAt(0);
      }
    }
    private void CloseNetGame ()
    {
      CloseNetSockets();
      lstPlayersNet.Items.Clear();
      panNet.Show();
      panNet.BringToFront();
      panNetSettings.Hide();
      cmbNetGameType.Select();
      txtChat.Text = ""; txtSend.Text = "";
      lblGameNet.Text = "Net Game";
    }
    
    #endregion
    
  }
}
