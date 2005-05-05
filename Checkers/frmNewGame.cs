using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Checkers
{
  public enum GameType
  {
    SinglePlayer = 0,
    Multiplayer = 1,
    Tournament = 2,
    NetGame = 3,
  }
  
  class ImageSetInfo
  {
    public ImageSetInfo (ComboBox owner) : this(owner, Color.Empty, null, null)
    {}
    public ImageSetInfo (ComboBox owner, Color color, Image pawn, Image king)
    { Owner = owner; Color = color; Pawn = pawn; King = king; }
    public ComboBox Owner;
    public Color Color;
    public Image Pawn;
    public Image King;
  }
  
  public class frmNewGame : System.Windows.Forms.Form
  {
    private GameType gameType;
    private bool opponentFirstMove;
    private bool useTournamentImages;
    private int difficulty;
    private PictureBox selectedPicture;
    
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
    private System.Windows.Forms.Button btnNetCreate;
    private System.Windows.Forms.Button btnNewShowGames;
    private System.Windows.Forms.ListBox lstNetGames;
    private System.Windows.Forms.ComboBox cmbNetGameType;
    private System.Windows.Forms.Button btnNetJoin;
    private System.Windows.Forms.Label lblAvailableGames;
    private System.Windows.Forms.GroupBox grpGameSettings1P;
    private System.Windows.Forms.GroupBox grpGameSettings2P;
    private System.Windows.Forms.GroupBox grpPlayerSettingsNet;
    private System.Windows.Forms.Label lblFirstMoveNet;
    private System.Windows.Forms.ComboBox cmbFirstMoveNet;
    private System.Windows.Forms.ListView lstPlayersNet;
    private System.Windows.Forms.Label lblChat;
    private System.Windows.Forms.ComboBox cmbImageSetNet;
    private System.Windows.Forms.Label lblImageSetNet;
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
    private System.Windows.Forms.PictureBox picImage1P1;
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
    private System.Windows.Forms.MenuItem menuImageBuiltin;
    private System.Windows.Forms.MenuItem menuImageBuiltinRed;
    private System.Windows.Forms.MenuItem menuImageBuiltinBlack;
    private System.Windows.Forms.MenuItem menuImageBuiltinWhite;
    private System.Windows.Forms.MenuItem menuImageBuiltinGold;
    private System.Windows.Forms.Panel panImageSet1P;
    private System.Windows.Forms.Label lblImage1P2;
    private System.Windows.Forms.PictureBox picImage1P2;
    private System.Windows.Forms.Panel panImageSet2P;
    private System.Windows.Forms.PictureBox picImageSwap1P;
    private System.Windows.Forms.Label lblImage2P2;
    private System.Windows.Forms.PictureBox picImage2P1;
    private System.Windows.Forms.PictureBox picImage2P2;
    private System.Windows.Forms.Label lblImage2P1;
    private System.Windows.Forms.PictureBox picImageSwap2P;
    private System.Windows.Forms.Panel panImageSetNet;
    private System.Windows.Forms.Label lblImageNet2;
    private System.Windows.Forms.PictureBox picImageNet1;
    private System.Windows.Forms.PictureBox picImageNet2;
    private System.Windows.Forms.Label lblImageNet1;
    private System.Windows.Forms.PictureBox picImageSwapNet;
    private System.Windows.Forms.ImageList imlKing;
    private System.ComponentModel.IContainer components;
    
    #endregion
    
    #region Class Construction
    
    public frmNewGame ()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
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
      this.grpGameSettings1P = new System.Windows.Forms.GroupBox();
      this.panImageSet1P = new System.Windows.Forms.Panel();
      this.lblImage1P2 = new System.Windows.Forms.Label();
      this.picImage1P1 = new System.Windows.Forms.PictureBox();
      this.picImage1P2 = new System.Windows.Forms.PictureBox();
      this.lblImage1P1 = new System.Windows.Forms.Label();
      this.picImageSwap1P = new System.Windows.Forms.PictureBox();
      this.lblFirstMove1P = new System.Windows.Forms.Label();
      this.cmbFirstMove1P = new System.Windows.Forms.ComboBox();
      this.cmbImageSet1P = new System.Windows.Forms.ComboBox();
      this.lblImageSet1P = new System.Windows.Forms.Label();
      this.lblGame1P = new System.Windows.Forms.Label();
      this.lblDifficulty1P = new System.Windows.Forms.Label();
      this.cmbDifficulty1P = new System.Windows.Forms.ComboBox();
      this.tabGame2P = new System.Windows.Forms.TabPage();
      this.grpGameSettings2P = new System.Windows.Forms.GroupBox();
      this.panImageSet2P = new System.Windows.Forms.Panel();
      this.lblImage2P2 = new System.Windows.Forms.Label();
      this.picImage2P1 = new System.Windows.Forms.PictureBox();
      this.picImage2P2 = new System.Windows.Forms.PictureBox();
      this.lblImage2P1 = new System.Windows.Forms.Label();
      this.picImageSwap2P = new System.Windows.Forms.PictureBox();
      this.cmbImageSet2P = new System.Windows.Forms.ComboBox();
      this.lblImageSet2P = new System.Windows.Forms.Label();
      this.lblFirstMove2P = new System.Windows.Forms.Label();
      this.cmbFirstMove2P = new System.Windows.Forms.ComboBox();
      this.lblGame2P = new System.Windows.Forms.Label();
      this.tabGameNet = new System.Windows.Forms.TabPage();
      this.lblGameNet = new System.Windows.Forms.Label();
      this.panNetSettings = new System.Windows.Forms.Panel();
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
      this.lblImageNet2 = new System.Windows.Forms.Label();
      this.picImageNet1 = new System.Windows.Forms.PictureBox();
      this.picImageNet2 = new System.Windows.Forms.PictureBox();
      this.lblImageNet1 = new System.Windows.Forms.Label();
      this.picImageSwapNet = new System.Windows.Forms.PictureBox();
      this.cmbFirstMoveNet = new System.Windows.Forms.ComboBox();
      this.lblFirstMoveNet = new System.Windows.Forms.Label();
      this.cmbImageSetNet = new System.Windows.Forms.ComboBox();
      this.lblImageSetNet = new System.Windows.Forms.Label();
      this.panNet = new System.Windows.Forms.Panel();
      this.grpLocalInfoNet = new System.Windows.Forms.GroupBox();
      this.lblIPAddress = new System.Windows.Forms.Label();
      this.lnkIPAddress = new System.Windows.Forms.LinkLabel();
      this.lblNetGameType = new System.Windows.Forms.Label();
      this.lblAvailableGames = new System.Windows.Forms.Label();
      this.cmbNetGameType = new System.Windows.Forms.ComboBox();
      this.btnNewShowGames = new System.Windows.Forms.Button();
      this.btnNetCreate = new System.Windows.Forms.Button();
      this.btnNetJoin = new System.Windows.Forms.Button();
      this.lstNetGames = new System.Windows.Forms.ListBox();
      this.imlImageSet = new System.Windows.Forms.ImageList(this.components);
      this.menuImage = new System.Windows.Forms.ContextMenu();
      this.menuImageBuiltin = new System.Windows.Forms.MenuItem();
      this.menuImageBuiltinRed = new System.Windows.Forms.MenuItem();
      this.menuImageBuiltinBlack = new System.Windows.Forms.MenuItem();
      this.menuImageBuiltinWhite = new System.Windows.Forms.MenuItem();
      this.menuImageBuiltinGold = new System.Windows.Forms.MenuItem();
      this.menuImageBrowse = new System.Windows.Forms.MenuItem();
      this.menuImageChooseColor = new System.Windows.Forms.MenuItem();
      this.mozGameType = new Pabo.MozBar.MozPane();
      this.mozGameType1P = new Pabo.MozBar.MozItem();
      this.mozGameType2P = new Pabo.MozBar.MozItem();
      this.mozGameTypeNet = new Pabo.MozBar.MozItem();
      this.dlgOpenImage = new System.Windows.Forms.OpenFileDialog();
      this.dlgSelectColor = new System.Windows.Forms.ColorDialog();
      this.imlKing = new System.Windows.Forms.ImageList(this.components);
      this.tabGame.SuspendLayout();
      this.tabGame1P.SuspendLayout();
      this.grpGameSettings1P.SuspendLayout();
      this.panImageSet1P.SuspendLayout();
      this.tabGame2P.SuspendLayout();
      this.grpGameSettings2P.SuspendLayout();
      this.panImageSet2P.SuspendLayout();
      this.tabGameNet.SuspendLayout();
      this.panNetSettings.SuspendLayout();
      this.grpPlayerSettingsNet.SuspendLayout();
      this.panImageSetNet.SuspendLayout();
      this.panNet.SuspendLayout();
      this.grpLocalInfoNet.SuspendLayout();
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
                                                                            this.grpGameSettings1P,
                                                                            this.lblGame1P,
                                                                            this.lblDifficulty1P,
                                                                            this.cmbDifficulty1P});
      this.tabGame1P.ImageIndex = 0;
      this.tabGame1P.Location = new System.Drawing.Point(4, 42);
      this.tabGame1P.Name = "tabGame1P";
      this.tabGame1P.Size = new System.Drawing.Size(592, 278);
      this.tabGame1P.TabIndex = 0;
      this.tabGame1P.ToolTipText = "Single Player Game";
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
      this.grpGameSettings1P.Size = new System.Drawing.Size(256, 200);
      this.grpGameSettings1P.TabIndex = 1;
      this.grpGameSettings1P.TabStop = false;
      this.grpGameSettings1P.Text = "Game Settings";
      // 
      // panImageSet1P
      // 
      this.panImageSet1P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.lblImage1P2,
                                                                                this.picImage1P1,
                                                                                this.picImage1P2,
                                                                                this.lblImage1P1,
                                                                                this.picImageSwap1P});
      this.panImageSet1P.Location = new System.Drawing.Point(12, 72);
      this.panImageSet1P.Name = "panImageSet1P";
      this.panImageSet1P.Size = new System.Drawing.Size(236, 52);
      this.panImageSet1P.TabIndex = 15;
      // 
      // lblImage1P2
      // 
      this.lblImage1P2.Location = new System.Drawing.Point(160, 20);
      this.lblImage1P2.Name = "lblImage1P2";
      this.lblImage1P2.Size = new System.Drawing.Size(72, 20);
      this.lblImage1P2.TabIndex = 5;
      this.lblImage1P2.Text = "Opponent";
      this.lblImage1P2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // picImage1P1
      // 
      this.picImage1P1.BackColor = System.Drawing.Color.White;
      this.picImage1P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picImage1P1.Location = new System.Drawing.Point(80, 4);
      this.picImage1P1.Name = "picImage1P1";
      this.picImage1P1.Size = new System.Drawing.Size(34, 34);
      this.picImage1P1.TabIndex = 11;
      this.picImage1P1.TabStop = false;
      this.picImage1P1.Click += new System.EventHandler(this.picImage_Click);
      this.picImage1P1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
      // 
      // picImage1P2
      // 
      this.picImage1P2.BackColor = System.Drawing.Color.White;
      this.picImage1P2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picImage1P2.Location = new System.Drawing.Point(120, 4);
      this.picImage1P2.Name = "picImage1P2";
      this.picImage1P2.Size = new System.Drawing.Size(34, 34);
      this.picImage1P2.TabIndex = 11;
      this.picImage1P2.TabStop = false;
      this.picImage1P2.Click += new System.EventHandler(this.picImage_Click);
      this.picImage1P2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
      // 
      // lblImage1P1
      // 
      this.lblImage1P1.Location = new System.Drawing.Point(4, 4);
      this.lblImage1P1.Name = "lblImage1P1";
      this.lblImage1P1.Size = new System.Drawing.Size(72, 20);
      this.lblImage1P1.TabIndex = 4;
      this.lblImage1P1.Text = "Player";
      this.lblImage1P1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // picImageSwap1P
      // 
      this.picImageSwap1P.Image = ((System.Drawing.Bitmap)(resources.GetObject("picImageSwap1P.Image")));
      this.picImageSwap1P.Location = new System.Drawing.Point(84, 40);
      this.picImageSwap1P.Name = "picImageSwap1P";
      this.picImageSwap1P.Size = new System.Drawing.Size(65, 8);
      this.picImageSwap1P.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.picImageSwap1P.TabIndex = 14;
      this.picImageSwap1P.TabStop = false;
      this.picImageSwap1P.Click += new System.EventHandler(this.picImageSwap1P_Click);
      // 
      // lblFirstMove1P
      // 
      this.lblFirstMove1P.Location = new System.Drawing.Point(12, 24);
      this.lblFirstMove1P.Name = "lblFirstMove1P";
      this.lblFirstMove1P.Size = new System.Drawing.Size(76, 20);
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
      this.cmbFirstMove1P.Location = new System.Drawing.Point(92, 24);
      this.cmbFirstMove1P.Name = "cmbFirstMove1P";
      this.cmbFirstMove1P.Size = new System.Drawing.Size(156, 21);
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
      this.cmbImageSet1P.Location = new System.Drawing.Point(92, 48);
      this.cmbImageSet1P.Name = "cmbImageSet1P";
      this.cmbImageSet1P.Size = new System.Drawing.Size(156, 21);
      this.cmbImageSet1P.TabIndex = 3;
      this.cmbImageSet1P.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
      // 
      // lblImageSet1P
      // 
      this.lblImageSet1P.Location = new System.Drawing.Point(12, 48);
      this.lblImageSet1P.Name = "lblImageSet1P";
      this.lblImageSet1P.Size = new System.Drawing.Size(76, 20);
      this.lblImageSet1P.TabIndex = 2;
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
      // lblDifficulty1P
      // 
      this.lblDifficulty1P.Location = new System.Drawing.Point(276, 36);
      this.lblDifficulty1P.Name = "lblDifficulty1P";
      this.lblDifficulty1P.Size = new System.Drawing.Size(164, 20);
      this.lblDifficulty1P.TabIndex = 2;
      this.lblDifficulty1P.Text = "Difficulty:";
      this.lblDifficulty1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cmbDifficulty1P
      // 
      this.cmbDifficulty1P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDifficulty1P.Items.AddRange(new object[] {
                                                         "Beginner",
                                                         "Intermediate",
                                                         "Advanced",
                                                         "Expert"});
      this.cmbDifficulty1P.Location = new System.Drawing.Point(276, 56);
      this.cmbDifficulty1P.Name = "cmbDifficulty1P";
      this.cmbDifficulty1P.Size = new System.Drawing.Size(164, 21);
      this.cmbDifficulty1P.TabIndex = 3;
      // 
      // tabGame2P
      // 
      this.tabGame2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.grpGameSettings2P,
                                                                            this.lblGame2P});
      this.tabGame2P.ImageIndex = 1;
      this.tabGame2P.Location = new System.Drawing.Point(4, 42);
      this.tabGame2P.Name = "tabGame2P";
      this.tabGame2P.Size = new System.Drawing.Size(592, 278);
      this.tabGame2P.TabIndex = 2;
      this.tabGame2P.ToolTipText = "Multiplayer Game";
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
      this.grpGameSettings2P.Size = new System.Drawing.Size(256, 200);
      this.grpGameSettings2P.TabIndex = 8;
      this.grpGameSettings2P.TabStop = false;
      this.grpGameSettings2P.Text = "Game Settings";
      // 
      // panImageSet2P
      // 
      this.panImageSet2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                this.lblImage2P2,
                                                                                this.picImage2P1,
                                                                                this.picImage2P2,
                                                                                this.lblImage2P1,
                                                                                this.picImageSwap2P});
      this.panImageSet2P.Location = new System.Drawing.Point(12, 72);
      this.panImageSet2P.Name = "panImageSet2P";
      this.panImageSet2P.Size = new System.Drawing.Size(236, 52);
      this.panImageSet2P.TabIndex = 16;
      // 
      // lblImage2P2
      // 
      this.lblImage2P2.Location = new System.Drawing.Point(160, 20);
      this.lblImage2P2.Name = "lblImage2P2";
      this.lblImage2P2.Size = new System.Drawing.Size(72, 20);
      this.lblImage2P2.TabIndex = 5;
      this.lblImage2P2.Text = "Player 2";
      this.lblImage2P2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // picImage2P1
      // 
      this.picImage2P1.BackColor = System.Drawing.Color.White;
      this.picImage2P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picImage2P1.Location = new System.Drawing.Point(80, 4);
      this.picImage2P1.Name = "picImage2P1";
      this.picImage2P1.Size = new System.Drawing.Size(34, 34);
      this.picImage2P1.TabIndex = 11;
      this.picImage2P1.TabStop = false;
      this.picImage2P1.Click += new System.EventHandler(this.picImage_Click);
      this.picImage2P1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
      // 
      // picImage2P2
      // 
      this.picImage2P2.BackColor = System.Drawing.Color.White;
      this.picImage2P2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picImage2P2.Location = new System.Drawing.Point(120, 4);
      this.picImage2P2.Name = "picImage2P2";
      this.picImage2P2.Size = new System.Drawing.Size(34, 34);
      this.picImage2P2.TabIndex = 11;
      this.picImage2P2.TabStop = false;
      this.picImage2P2.Click += new System.EventHandler(this.picImage_Click);
      this.picImage2P2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
      // 
      // lblImage2P1
      // 
      this.lblImage2P1.Location = new System.Drawing.Point(4, 4);
      this.lblImage2P1.Name = "lblImage2P1";
      this.lblImage2P1.Size = new System.Drawing.Size(72, 20);
      this.lblImage2P1.TabIndex = 4;
      this.lblImage2P1.Text = "Player 1";
      this.lblImage2P1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // picImageSwap2P
      // 
      this.picImageSwap2P.Image = ((System.Drawing.Bitmap)(resources.GetObject("picImageSwap2P.Image")));
      this.picImageSwap2P.Location = new System.Drawing.Point(84, 40);
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
      this.cmbImageSet2P.Location = new System.Drawing.Point(92, 48);
      this.cmbImageSet2P.Name = "cmbImageSet2P";
      this.cmbImageSet2P.Size = new System.Drawing.Size(156, 21);
      this.cmbImageSet2P.TabIndex = 3;
      this.cmbImageSet2P.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
      // 
      // lblImageSet2P
      // 
      this.lblImageSet2P.Location = new System.Drawing.Point(12, 48);
      this.lblImageSet2P.Name = "lblImageSet2P";
      this.lblImageSet2P.Size = new System.Drawing.Size(76, 20);
      this.lblImageSet2P.TabIndex = 2;
      this.lblImageSet2P.Text = "Image Set:";
      this.lblImageSet2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblFirstMove2P
      // 
      this.lblFirstMove2P.Location = new System.Drawing.Point(12, 24);
      this.lblFirstMove2P.Name = "lblFirstMove2P";
      this.lblFirstMove2P.Size = new System.Drawing.Size(76, 20);
      this.lblFirstMove2P.TabIndex = 1;
      this.lblFirstMove2P.Text = "First Move:";
      this.lblFirstMove2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cmbFirstMove2P
      // 
      this.cmbFirstMove2P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFirstMove2P.Items.AddRange(new object[] {
                                                        "Player 1",
                                                        "Player 2"});
      this.cmbFirstMove2P.Location = new System.Drawing.Point(92, 24);
      this.cmbFirstMove2P.Name = "cmbFirstMove2P";
      this.cmbFirstMove2P.Size = new System.Drawing.Size(156, 21);
      this.cmbFirstMove2P.TabIndex = 0;
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
      this.lblGame2P.TabIndex = 1;
      this.lblGame2P.Text = "Multiplayer";
      this.lblGame2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabGameNet
      // 
      this.tabGameNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.lblGameNet,
                                                                             this.panNetSettings,
                                                                             this.panNet});
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
      this.lblGameNet.TabIndex = 3;
      this.lblGameNet.Text = "Net Game";
      this.lblGameNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panNetSettings
      // 
      this.panNetSettings.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.panNetSettings.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.lblPlayersNet,
                                                                                 this.lblChat,
                                                                                 this.lstPlayersNet,
                                                                                 this.btnSend,
                                                                                 this.txtSend,
                                                                                 this.txtChat,
                                                                                 this.grpPlayerSettingsNet});
      this.panNetSettings.Location = new System.Drawing.Point(0, 20);
      this.panNetSettings.Name = "panNetSettings";
      this.panNetSettings.Size = new System.Drawing.Size(592, 260);
      this.panNetSettings.TabIndex = 9;
      // 
      // lblPlayersNet
      // 
      this.lblPlayersNet.Location = new System.Drawing.Point(0, 12);
      this.lblPlayersNet.Name = "lblPlayersNet";
      this.lblPlayersNet.Size = new System.Drawing.Size(256, 16);
      this.lblPlayersNet.TabIndex = 15;
      this.lblPlayersNet.Text = "Connected Players:";
      // 
      // lblChat
      // 
      this.lblChat.Location = new System.Drawing.Point(0, 128);
      this.lblChat.Name = "lblChat";
      this.lblChat.Size = new System.Drawing.Size(256, 16);
      this.lblChat.TabIndex = 14;
      this.lblChat.Text = "Discussion:";
      // 
      // lstPlayersNet
      // 
      this.lstPlayersNet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    this.colName,
                                                                                    this.colPosition});
      this.lstPlayersNet.Location = new System.Drawing.Point(0, 28);
      this.lstPlayersNet.Name = "lstPlayersNet";
      this.lstPlayersNet.Size = new System.Drawing.Size(256, 88);
      this.lstPlayersNet.TabIndex = 13;
      this.lstPlayersNet.View = System.Windows.Forms.View.Details;
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
      this.btnSend.Location = new System.Drawing.Point(212, 224);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(44, 20);
      this.btnSend.TabIndex = 12;
      this.btnSend.Text = "&Send";
      // 
      // txtSend
      // 
      this.txtSend.Location = new System.Drawing.Point(0, 224);
      this.txtSend.Name = "txtSend";
      this.txtSend.Size = new System.Drawing.Size(208, 20);
      this.txtSend.TabIndex = 11;
      this.txtSend.Text = "";
      // 
      // txtChat
      // 
      this.txtChat.Location = new System.Drawing.Point(0, 144);
      this.txtChat.Name = "txtChat";
      this.txtChat.ReadOnly = true;
      this.txtChat.Size = new System.Drawing.Size(256, 76);
      this.txtChat.TabIndex = 10;
      this.txtChat.Text = "";
      // 
      // grpPlayerSettingsNet
      // 
      this.grpPlayerSettingsNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.panImageSetNet,
                                                                                       this.cmbFirstMoveNet,
                                                                                       this.lblFirstMoveNet,
                                                                                       this.cmbImageSetNet,
                                                                                       this.lblImageSetNet});
      this.grpPlayerSettingsNet.Location = new System.Drawing.Point(268, 12);
      this.grpPlayerSettingsNet.Name = "grpPlayerSettingsNet";
      this.grpPlayerSettingsNet.Size = new System.Drawing.Size(256, 220);
      this.grpPlayerSettingsNet.TabIndex = 7;
      this.grpPlayerSettingsNet.TabStop = false;
      this.grpPlayerSettingsNet.Text = "Game Settings";
      // 
      // panImageSetNet
      // 
      this.panImageSetNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.lblImageNet2,
                                                                                 this.picImageNet1,
                                                                                 this.picImageNet2,
                                                                                 this.lblImageNet1,
                                                                                 this.picImageSwapNet});
      this.panImageSetNet.Location = new System.Drawing.Point(12, 72);
      this.panImageSetNet.Name = "panImageSetNet";
      this.panImageSetNet.Size = new System.Drawing.Size(236, 52);
      this.panImageSetNet.TabIndex = 17;
      // 
      // lblImageNet2
      // 
      this.lblImageNet2.Location = new System.Drawing.Point(160, 20);
      this.lblImageNet2.Name = "lblImageNet2";
      this.lblImageNet2.Size = new System.Drawing.Size(72, 20);
      this.lblImageNet2.TabIndex = 5;
      this.lblImageNet2.Text = "Opponent";
      this.lblImageNet2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // picImageNet1
      // 
      this.picImageNet1.BackColor = System.Drawing.Color.White;
      this.picImageNet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picImageNet1.Location = new System.Drawing.Point(80, 4);
      this.picImageNet1.Name = "picImageNet1";
      this.picImageNet1.Size = new System.Drawing.Size(34, 34);
      this.picImageNet1.TabIndex = 11;
      this.picImageNet1.TabStop = false;
      this.picImageNet1.Click += new System.EventHandler(this.picImage_Click);
      // 
      // picImageNet2
      // 
      this.picImageNet2.BackColor = System.Drawing.Color.White;
      this.picImageNet2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picImageNet2.Location = new System.Drawing.Point(120, 4);
      this.picImageNet2.Name = "picImageNet2";
      this.picImageNet2.Size = new System.Drawing.Size(34, 34);
      this.picImageNet2.TabIndex = 11;
      this.picImageNet2.TabStop = false;
      this.picImageNet2.Click += new System.EventHandler(this.picImage_Click);
      // 
      // lblImageNet1
      // 
      this.lblImageNet1.Location = new System.Drawing.Point(4, 4);
      this.lblImageNet1.Name = "lblImageNet1";
      this.lblImageNet1.Size = new System.Drawing.Size(72, 20);
      this.lblImageNet1.TabIndex = 4;
      this.lblImageNet1.Text = "Player";
      this.lblImageNet1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // picImageSwapNet
      // 
      this.picImageSwapNet.Image = ((System.Drawing.Bitmap)(resources.GetObject("picImageSwapNet.Image")));
      this.picImageSwapNet.Location = new System.Drawing.Point(84, 40);
      this.picImageSwapNet.Name = "picImageSwapNet";
      this.picImageSwapNet.Size = new System.Drawing.Size(65, 8);
      this.picImageSwapNet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.picImageSwapNet.TabIndex = 14;
      this.picImageSwapNet.TabStop = false;
      this.picImageSwapNet.Click += new System.EventHandler(this.picImageSwapNet_Click);
      // 
      // cmbFirstMoveNet
      // 
      this.cmbFirstMoveNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFirstMoveNet.Items.AddRange(new object[] {
                                                         "Host",
                                                         "Opponent"});
      this.cmbFirstMoveNet.Location = new System.Drawing.Point(92, 24);
      this.cmbFirstMoveNet.Name = "cmbFirstMoveNet";
      this.cmbFirstMoveNet.Size = new System.Drawing.Size(148, 21);
      this.cmbFirstMoveNet.TabIndex = 1;
      // 
      // lblFirstMoveNet
      // 
      this.lblFirstMoveNet.Location = new System.Drawing.Point(12, 24);
      this.lblFirstMoveNet.Name = "lblFirstMoveNet";
      this.lblFirstMoveNet.Size = new System.Drawing.Size(76, 20);
      this.lblFirstMoveNet.TabIndex = 0;
      this.lblFirstMoveNet.Text = "First Move:";
      this.lblFirstMoveNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cmbImageSetNet
      // 
      this.cmbImageSetNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbImageSetNet.Items.AddRange(new object[] {
                                                        "Standard",
                                                        "Standard (Black)",
                                                        "Tournament",
                                                        "Custom"});
      this.cmbImageSetNet.Location = new System.Drawing.Point(92, 48);
      this.cmbImageSetNet.Name = "cmbImageSetNet";
      this.cmbImageSetNet.Size = new System.Drawing.Size(148, 21);
      this.cmbImageSetNet.TabIndex = 1;
      this.cmbImageSetNet.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
      // 
      // lblImageSetNet
      // 
      this.lblImageSetNet.Location = new System.Drawing.Point(12, 48);
      this.lblImageSetNet.Name = "lblImageSetNet";
      this.lblImageSetNet.Size = new System.Drawing.Size(76, 20);
      this.lblImageSetNet.TabIndex = 0;
      this.lblImageSetNet.Text = "Image Set:";
      this.lblImageSetNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panNet
      // 
      this.panNet.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.panNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                         this.grpLocalInfoNet,
                                                                         this.lblNetGameType,
                                                                         this.lblAvailableGames,
                                                                         this.cmbNetGameType,
                                                                         this.btnNewShowGames,
                                                                         this.btnNetCreate,
                                                                         this.btnNetJoin,
                                                                         this.lstNetGames});
      this.panNet.Location = new System.Drawing.Point(0, 20);
      this.panNet.Name = "panNet";
      this.panNet.Size = new System.Drawing.Size(592, 260);
      this.panNet.TabIndex = 10;
      // 
      // grpLocalInfoNet
      // 
      this.grpLocalInfoNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.lblIPAddress,
                                                                                  this.lnkIPAddress});
      this.grpLocalInfoNet.Location = new System.Drawing.Point(308, 12);
      this.grpLocalInfoNet.Name = "grpLocalInfoNet";
      this.grpLocalInfoNet.Size = new System.Drawing.Size(200, 76);
      this.grpLocalInfoNet.TabIndex = 16;
      this.grpLocalInfoNet.TabStop = false;
      this.grpLocalInfoNet.Text = "Local Information";
      // 
      // lblIPAddress
      // 
      this.lblIPAddress.Location = new System.Drawing.Point(12, 24);
      this.lblIPAddress.Name = "lblIPAddress";
      this.lblIPAddress.Size = new System.Drawing.Size(64, 16);
      this.lblIPAddress.TabIndex = 1;
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
      this.lnkIPAddress.Size = new System.Drawing.Size(108, 16);
      this.lnkIPAddress.TabIndex = 0;
      this.lnkIPAddress.TabStop = true;
      this.lnkIPAddress.Text = "x.x.x.x";
      this.lnkIPAddress.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.lnkIPAddress.VisitedLinkColor = System.Drawing.SystemColors.WindowText;
      // 
      // lblNetGameType
      // 
      this.lblNetGameType.Location = new System.Drawing.Point(0, 12);
      this.lblNetGameType.Name = "lblNetGameType";
      this.lblNetGameType.Size = new System.Drawing.Size(296, 16);
      this.lblNetGameType.TabIndex = 15;
      this.lblNetGameType.Text = "Game Type (Protocol):";
      // 
      // lblAvailableGames
      // 
      this.lblAvailableGames.Location = new System.Drawing.Point(0, 96);
      this.lblAvailableGames.Name = "lblAvailableGames";
      this.lblAvailableGames.Size = new System.Drawing.Size(296, 16);
      this.lblAvailableGames.TabIndex = 14;
      this.lblAvailableGames.Text = "Available Games:";
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
      this.cmbNetGameType.TabIndex = 13;
      // 
      // btnNewShowGames
      // 
      this.btnNewShowGames.Location = new System.Drawing.Point(192, 56);
      this.btnNewShowGames.Name = "btnNewShowGames";
      this.btnNewShowGames.Size = new System.Drawing.Size(104, 32);
      this.btnNewShowGames.TabIndex = 12;
      this.btnNewShowGames.Text = "&Show Games";
      // 
      // btnNetCreate
      // 
      this.btnNetCreate.Location = new System.Drawing.Point(100, 204);
      this.btnNetCreate.Name = "btnNetCreate";
      this.btnNetCreate.Size = new System.Drawing.Size(96, 32);
      this.btnNetCreate.TabIndex = 7;
      this.btnNetCreate.Text = "&Create";
      // 
      // btnNetJoin
      // 
      this.btnNetJoin.Location = new System.Drawing.Point(200, 204);
      this.btnNetJoin.Name = "btnNetJoin";
      this.btnNetJoin.Size = new System.Drawing.Size(96, 32);
      this.btnNetJoin.TabIndex = 7;
      this.btnNetJoin.Text = "&Join";
      // 
      // lstNetGames
      // 
      this.lstNetGames.IntegralHeight = false;
      this.lstNetGames.Location = new System.Drawing.Point(0, 112);
      this.lstNetGames.Name = "lstNetGames";
      this.lstNetGames.Size = new System.Drawing.Size(296, 88);
      this.lstNetGames.TabIndex = 11;
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
                                                                              this.menuImageBuiltin,
                                                                              this.menuImageBrowse,
                                                                              this.menuImageChooseColor});
      // 
      // menuImageBuiltin
      // 
      this.menuImageBuiltin.Index = 0;
      this.menuImageBuiltin.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuImageBuiltinRed,
                                                                                     this.menuImageBuiltinBlack,
                                                                                     this.menuImageBuiltinWhite,
                                                                                     this.menuImageBuiltinGold});
      this.menuImageBuiltin.Text = "&Built-in Image";
      // 
      // menuImageBuiltinRed
      // 
      this.menuImageBuiltinRed.Index = 0;
      this.menuImageBuiltinRed.Text = "&Red Set";
      this.menuImageBuiltinRed.Click += new System.EventHandler(this.menuImageBuiltinRed_Click);
      // 
      // menuImageBuiltinBlack
      // 
      this.menuImageBuiltinBlack.Index = 1;
      this.menuImageBuiltinBlack.Text = "&Black Set";
      this.menuImageBuiltinBlack.Click += new System.EventHandler(this.menuImageBuiltinBlack_Click);
      // 
      // menuImageBuiltinWhite
      // 
      this.menuImageBuiltinWhite.Index = 2;
      this.menuImageBuiltinWhite.Text = "&White Set";
      this.menuImageBuiltinWhite.Click += new System.EventHandler(this.menuImageBuiltinWhite_Click);
      // 
      // menuImageBuiltinGold
      // 
      this.menuImageBuiltinGold.Index = 3;
      this.menuImageBuiltinGold.Text = "&Gold Set";
      this.menuImageBuiltinGold.Click += new System.EventHandler(this.menuImageBuiltinGold_Click);
      // 
      // menuImageBrowse
      // 
      this.menuImageBrowse.Index = 1;
      this.menuImageBrowse.Text = "&Browse for Image...";
      this.menuImageBrowse.Click += new System.EventHandler(this.menuImageBrowse_Click);
      // 
      // menuImageChooseColor
      // 
      this.menuImageChooseColor.Index = 2;
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
      // frmNewGame
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(606, 387);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.btnCancel,
                                                                  this.btnOK,
                                                                  this.tabGame,
                                                                  this.mozGameType});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "frmNewGame";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "New Checkers Game";
      this.Load += new System.EventHandler(this.frmNewGame_Load);
      this.tabGame.ResumeLayout(false);
      this.tabGame1P.ResumeLayout(false);
      this.grpGameSettings1P.ResumeLayout(false);
      this.panImageSet1P.ResumeLayout(false);
      this.tabGame2P.ResumeLayout(false);
      this.grpGameSettings2P.ResumeLayout(false);
      this.panImageSet2P.ResumeLayout(false);
      this.tabGameNet.ResumeLayout(false);
      this.panNetSettings.ResumeLayout(false);
      this.grpPlayerSettingsNet.ResumeLayout(false);
      this.panImageSetNet.ResumeLayout(false);
      this.panNet.ResumeLayout(false);
      this.grpLocalInfoNet.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.mozGameType)).EndInit();
      this.mozGameType.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    
    #endregion
    
    #endregion
    
    public GameType GameType
    { get { return gameType; } }
    public bool OpponentFirstMove
    { get { return opponentFirstMove; } }
    public bool UseTournamentImages
    { get { return useTournamentImages; } }
    public int Difficulty
    { get { return difficulty; } }
    
    public new DialogResult ShowDialog (IWin32Window owner)
    {
      DialogResult result = base.ShowDialog(owner);
      if (result != DialogResult.Cancel)
      {
        switch (tabGame.TabIndex)
        {
          case 0:
            gameType = GameType.SinglePlayer;
            opponentFirstMove = (cmbFirstMove1P.SelectedIndex == 1);
            useTournamentImages = (cmbImageSet1P.SelectedIndex == 1);
            difficulty = (( cmbDifficulty1P.SelectedIndex == -1 )?( 0 ):( cmbDifficulty1P.SelectedIndex ));
            break;
        }
      }
      return result;
    }
    
    private void frmNewGame_Load (object sender, System.EventArgs e)
    {
      // Add tournament images
      imlImageSet.Images.Add(CreatePieceImage(Color.Firebrick, false));
      imlImageSet.Images.Add(CreatePieceImage(Color.Firebrick, true));
      imlImageSet.Images.Add(CreatePieceImage(Color.LightGray, false));
      imlImageSet.Images.Add(CreatePieceImage(Color.LightGray, true));
      
      // Fill picturebox info
      picImage1P1.Tag = new ImageSetInfo(cmbImageSet1P); picImage1P2.Tag = new ImageSetInfo(cmbImageSet1P);
      picImage2P1.Tag = new ImageSetInfo(cmbImageSet2P); picImage2P2.Tag = new ImageSetInfo(cmbImageSet2P);
      picImageNet1.Tag = new ImageSetInfo(cmbImageSetNet); picImageNet2.Tag = new ImageSetInfo(cmbImageSetNet);
      
      // Select initial tab
      mozGameType.Items[0].SelectItem();
      
      // !!!!! Remember last values used
      cmbFirstMove1P.SelectedIndex = 0;
      cmbImageSet1P.SelectedIndex = 0;
      cmbDifficulty1P.SelectedIndex = 0;
      
      cmbFirstMove2P.SelectedIndex = 0;
      cmbImageSet2P.SelectedIndex = 0;
      
      cmbNetGameType.SelectedIndex = 0;
      cmbFirstMoveNet.SelectedIndex = 0;
      cmbImageSetNet.SelectedIndex = 0;
      lnkIPAddress.Text = "0.0.0.0";        // !!!!!
      panNet.BringToFront();
    }
    
    private void menuImageBrowse_Click (object sender, System.EventArgs e)
    {
      if (selectedPicture == null) return;
      // Open pawn image
      dlgOpenImage.Title = "Open Custom Pawn Image";
      if (dlgOpenImage.ShowDialog(this) == DialogResult.Cancel) return;
      Image pawn = Image.FromFile(dlgOpenImage.FileName);
      if ((pawn.Width != 32) || (pawn.Height != 32)) pawn = new Bitmap(pawn, 32, 32);
      // Open king image
      dlgOpenImage.Title = "Open Custom King Image";
      if (dlgOpenImage.ShowDialog(this) == DialogResult.Cancel) return;
      Image king = Image.FromFile(dlgOpenImage.FileName);
      if ((king.Width != 32) || (king.Height != 32)) king = new Bitmap(king, 32, 32);
      // Set custom pawn.king images
      UpdateImageSet(selectedPicture, Color.Gray, pawn, king);
      SetCustomImage();
    }
    private void menuImageChooseColor_Click (object sender, System.EventArgs e)
    {
      if (selectedPicture == null) return;
      dlgSelectColor.Color = (( !((ImageSetInfo)selectedPicture.Tag).Color.IsEmpty )?( (Color)((ImageSetInfo)selectedPicture.Tag).Color ):( Color.Gray ));
      if (dlgSelectColor.ShowDialog(this) == DialogResult.Cancel) return;
      Color color = dlgSelectColor.Color;
      UpdateImageSet(selectedPicture, color, CreatePieceImage(color, false), CreatePieceImage(color, true));
      SetCustomImage();
    }
    private void menuImageBuiltinRed_Click (object sender, System.EventArgs e)
    {
      if (selectedPicture == null) return;
      UpdateImageSet(selectedPicture, Color.Firebrick, imlImageSet.Images[0], imlImageSet.Images[1]);
      SetCustomImage();
    }
    private void menuImageBuiltinBlack_Click (object sender, System.EventArgs e)
    {
      if (selectedPicture == null) return;
      UpdateImageSet(selectedPicture, Color.DimGray, imlImageSet.Images[4], imlImageSet.Images[5]);
      SetCustomImage();
    }
    private void menuImageBuiltinWhite_Click (object sender, System.EventArgs e)
    {
      if (selectedPicture == null) return;
      UpdateImageSet(selectedPicture, Color.LightGray, imlImageSet.Images[2], imlImageSet.Images[3]);
      SetCustomImage();
    }
    private void menuImageBuiltinGold_Click (object sender, System.EventArgs e)
    {
      if (selectedPicture == null) return;
      UpdateImageSet(selectedPicture, Color.Gold, imlImageSet.Images[6], imlImageSet.Images[7]);
      SetCustomImage();
    }
    
    private void tabGame_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if (tabGame.SelectedIndex == -1) return;
      mozGameType.Items[tabGame.SelectedIndex].SelectItem();
      
      switch (tabGame.SelectedIndex)
      {
        case 0: break;  // !!!!!
      }
    }
    
    private void picImageSwap1P_Click (object sender, System.EventArgs e)
    { SwapImageSet(picImage1P1, picImage1P2); }
    private void picImageSwap2P_Click(object sender, System.EventArgs e)
    { SwapImageSet(picImage2P1, picImage2P2); }
    private void picImageSwapNet_Click(object sender, System.EventArgs e)
    { SwapImageSet(picImageNet1, picImageNet2); }
    
    private void cmbImageSet_SelectedIndexChanged (object sender, System.EventArgs e)
    { SetImageSet((ComboBox)sender); }
    
    private void picImage_Click (object sender, System.EventArgs e)
    {
      selectedPicture = (PictureBox)sender;
      menuImage.Show(((Control)sender).Parent, new Point(((Control)sender).Left, ((Control)sender).Top+((Control)sender).Height));
    }
    private void picImage_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
    { picImage_Click(sender, EventArgs.Empty); }
    
    private void mozGameType1P_Click (object sender, System.EventArgs e)
    { tabGame.SelectedIndex = 0; }
    private void mozGameType2P_Click (object sender, System.EventArgs e)
    { tabGame.SelectedIndex = 1; }
    private void mozGameTypeNet_Click (object sender, System.EventArgs e)
    { tabGame.SelectedIndex = 2; }
    
    private void SetCustomImage ()
    {
      if ((selectedPicture == picImage1P1) || (selectedPicture == picImage1P2))
        cmbImageSet1P.SelectedIndex = 3;
      else if ((selectedPicture == picImage2P1) || (selectedPicture == picImage2P2))
        cmbImageSet2P.SelectedIndex = 3;
    }
    
    private void SwapImageSet (PictureBox pic1, PictureBox pic2)
    {
      Image temp = pic1.Image; pic1.Image = pic2.Image; pic2.Image = temp;
      object tag = pic1.Tag; pic1.Tag = pic2.Tag; pic2.Tag = tag;
    }
    private void SetImageSet (ComboBox sender)
    {
      if (sender == cmbImageSet1P) SetImageSet(cmbImageSet1P.SelectedIndex, picImage1P1, picImage1P2);
      else if (sender == cmbImageSet2P) SetImageSet(cmbImageSet2P.SelectedIndex, picImage2P1, picImage2P2);
      else if (sender == cmbImageSetNet) SetImageSet(cmbImageSetNet.SelectedIndex, picImageNet1, picImageNet2);
    }
    private void SetImageSet (int setIndex, PictureBox pic1, PictureBox pic2)
    {
      switch (setIndex)
      {
        case 0:
          UpdateImageSet(pic1, Color.Firebrick, imlImageSet.Images[0], imlImageSet.Images[1]);
          UpdateImageSet(pic2, Color.LightGray, imlImageSet.Images[2], imlImageSet.Images[3]);
          break;
        case 1:
          UpdateImageSet(pic1, Color.DimGray, imlImageSet.Images[4], imlImageSet.Images[5]);
          UpdateImageSet(pic2, Color.LightGray, imlImageSet.Images[2], imlImageSet.Images[3]);
          break;
        case 2:
          UpdateImageSet(pic1, Color.Firebrick, imlImageSet.Images[8], imlImageSet.Images[9]);
          UpdateImageSet(pic2, Color.LightGray, imlImageSet.Images[10], imlImageSet.Images[11]);
          break;
      }
    }
    private void UpdateImageSet (PictureBox pic, Color color, Image pawn, Image king)
    {
      pic.Image = pawn;
      ((ImageSetInfo)pic.Tag).Color = color;
      ((ImageSetInfo)pic.Tag).Pawn = pawn;
      ((ImageSetInfo)pic.Tag).King = king;
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
    private void DrawKingIcon (Graphics g)
    { g.DrawImage(imlKing.Images[0], 0, 0, 32, 32); }
  }
}
