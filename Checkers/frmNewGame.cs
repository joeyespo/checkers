using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Checkers
{
  public enum GameType
  {
    None = 0,
    SinglePlayer = 1,
    Multiplayer = 2,
    NetGame = 3,
  }
  
  public class frmNewGame : System.Windows.Forms.Form
  {
    private PictureBox selectedPicture;
    private GameType gameType;
    private bool optionalJumping;
    private int firstMove;
    private int difficulty;
    private Image [] imageSet;
    private string player1Name;
    private string player2Name;
    
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
    private System.Windows.Forms.Label lblJumpRule1P;
    private System.Windows.Forms.ComboBox cmbJumpRule1P;
    private System.Windows.Forms.Label lblJumpRule2P;
    private System.Windows.Forms.ComboBox cmbJumpRule2P;
    private System.Windows.Forms.Label lblJumpRuleNet;
    private System.Windows.Forms.ComboBox cmbJumpRuleNet;
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
      this.lblJumpRule1P = new System.Windows.Forms.Label();
      this.cmbJumpRule1P = new System.Windows.Forms.ComboBox();
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
      this.lblJumpRule2P = new System.Windows.Forms.Label();
      this.cmbJumpRule2P = new System.Windows.Forms.ComboBox();
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
      this.lblJumpRuleNet = new System.Windows.Forms.Label();
      this.cmbJumpRuleNet = new System.Windows.Forms.ComboBox();
      this.panImageSetNet = new System.Windows.Forms.Panel();
      this.picKingNet1 = new System.Windows.Forms.PictureBox();
      this.picKingNet2 = new System.Windows.Forms.PictureBox();
      this.lblImageNet2 = new System.Windows.Forms.Label();
      this.picPawnNet1 = new System.Windows.Forms.PictureBox();
      this.picPawnNet2 = new System.Windows.Forms.PictureBox();
      this.lblImageNet1 = new System.Windows.Forms.Label();
      this.picImageSwapNet = new System.Windows.Forms.PictureBox();
      this.cmbFirstMoveNet = new System.Windows.Forms.ComboBox();
      this.lblFirstMoveNet = new System.Windows.Forms.Label();
      this.cmbImageSetNet = new System.Windows.Forms.ComboBox();
      this.lblImageSetNet = new System.Windows.Forms.Label();
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
                                                                                    this.lblImageSet1P,
                                                                                    this.lblJumpRule1P,
                                                                                    this.cmbJumpRule1P});
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
      this.panImageSet1P.Location = new System.Drawing.Point(8, 96);
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
      this.cmbImageSet1P.Location = new System.Drawing.Point(96, 72);
      this.cmbImageSet1P.Name = "cmbImageSet1P";
      this.cmbImageSet1P.Size = new System.Drawing.Size(164, 21);
      this.cmbImageSet1P.TabIndex = 5;
      this.cmbImageSet1P.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
      // 
      // lblImageSet1P
      // 
      this.lblImageSet1P.Location = new System.Drawing.Point(8, 72);
      this.lblImageSet1P.Name = "lblImageSet1P";
      this.lblImageSet1P.Size = new System.Drawing.Size(84, 20);
      this.lblImageSet1P.TabIndex = 4;
      this.lblImageSet1P.Text = "Image Set:";
      this.lblImageSet1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblJumpRule1P
      // 
      this.lblJumpRule1P.Location = new System.Drawing.Point(8, 48);
      this.lblJumpRule1P.Name = "lblJumpRule1P";
      this.lblJumpRule1P.Size = new System.Drawing.Size(84, 20);
      this.lblJumpRule1P.TabIndex = 2;
      this.lblJumpRule1P.Text = "Jump Rule:";
      this.lblJumpRule1P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cmbJumpRule1P
      // 
      this.cmbJumpRule1P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbJumpRule1P.Items.AddRange(new object[] {
                                                       "Forceful Jumping",
                                                       "Optional Jumping"});
      this.cmbJumpRule1P.Location = new System.Drawing.Point(96, 48);
      this.cmbJumpRule1P.Name = "cmbJumpRule1P";
      this.cmbJumpRule1P.Size = new System.Drawing.Size(164, 21);
      this.cmbJumpRule1P.TabIndex = 3;
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
      this.txtPlayerName2P2.Name = "txtPlayerName2P2";
      this.txtPlayerName2P2.Size = new System.Drawing.Size(252, 20);
      this.txtPlayerName2P2.TabIndex = 3;
      this.txtPlayerName2P2.Text = "Player 2";
      // 
      // grpGameSettings2P
      // 
      this.grpGameSettings2P.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.lblJumpRule2P,
                                                                                    this.cmbJumpRule2P,
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
      // lblJumpRule2P
      // 
      this.lblJumpRule2P.Location = new System.Drawing.Point(8, 48);
      this.lblJumpRule2P.Name = "lblJumpRule2P";
      this.lblJumpRule2P.Size = new System.Drawing.Size(84, 20);
      this.lblJumpRule2P.TabIndex = 2;
      this.lblJumpRule2P.Text = "Jump Rule:";
      this.lblJumpRule2P.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cmbJumpRule2P
      // 
      this.cmbJumpRule2P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbJumpRule2P.Items.AddRange(new object[] {
                                                       "Forceful Jumping",
                                                       "Optional Jumping"});
      this.cmbJumpRule2P.Location = new System.Drawing.Point(96, 48);
      this.cmbJumpRule2P.Name = "cmbJumpRule2P";
      this.cmbJumpRule2P.Size = new System.Drawing.Size(164, 21);
      this.cmbJumpRule2P.TabIndex = 3;
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
      this.panImageSet2P.Location = new System.Drawing.Point(8, 96);
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
      this.cmbImageSet2P.Location = new System.Drawing.Point(96, 72);
      this.cmbImageSet2P.Name = "cmbImageSet2P";
      this.cmbImageSet2P.Size = new System.Drawing.Size(164, 21);
      this.cmbImageSet2P.TabIndex = 5;
      this.cmbImageSet2P.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
      // 
      // lblImageSet2P
      // 
      this.lblImageSet2P.Location = new System.Drawing.Point(8, 72);
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
      this.panNet.TabIndex = 1;
      // 
      // grpLocalInfoNet
      // 
      this.grpLocalInfoNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                  this.lblIPAddress,
                                                                                  this.lnkIPAddress});
      this.grpLocalInfoNet.Location = new System.Drawing.Point(308, 12);
      this.grpLocalInfoNet.Name = "grpLocalInfoNet";
      this.grpLocalInfoNet.Size = new System.Drawing.Size(200, 76);
      this.grpLocalInfoNet.TabIndex = 7;
      this.grpLocalInfoNet.TabStop = false;
      this.grpLocalInfoNet.Text = "Local Information";
      // 
      // lblIPAddress
      // 
      this.lblIPAddress.Location = new System.Drawing.Point(12, 24);
      this.lblIPAddress.Name = "lblIPAddress";
      this.lblIPAddress.Size = new System.Drawing.Size(64, 16);
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
      this.lnkIPAddress.Size = new System.Drawing.Size(108, 16);
      this.lnkIPAddress.TabIndex = 1;
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
      this.lblNetGameType.TabIndex = 0;
      this.lblNetGameType.Text = "Game Type (Protocol):";
      // 
      // lblAvailableGames
      // 
      this.lblAvailableGames.Location = new System.Drawing.Point(0, 96);
      this.lblAvailableGames.Name = "lblAvailableGames";
      this.lblAvailableGames.Size = new System.Drawing.Size(296, 16);
      this.lblAvailableGames.TabIndex = 3;
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
      this.cmbNetGameType.TabIndex = 1;
      // 
      // btnNewShowGames
      // 
      this.btnNewShowGames.Location = new System.Drawing.Point(192, 56);
      this.btnNewShowGames.Name = "btnNewShowGames";
      this.btnNewShowGames.Size = new System.Drawing.Size(104, 32);
      this.btnNewShowGames.TabIndex = 2;
      this.btnNewShowGames.Text = "&Show Games";
      // 
      // btnNetCreate
      // 
      this.btnNetCreate.Location = new System.Drawing.Point(100, 204);
      this.btnNetCreate.Name = "btnNetCreate";
      this.btnNetCreate.Size = new System.Drawing.Size(96, 32);
      this.btnNetCreate.TabIndex = 5;
      this.btnNetCreate.Text = "&Create";
      // 
      // btnNetJoin
      // 
      this.btnNetJoin.Location = new System.Drawing.Point(200, 204);
      this.btnNetJoin.Name = "btnNetJoin";
      this.btnNetJoin.Size = new System.Drawing.Size(96, 32);
      this.btnNetJoin.TabIndex = 6;
      this.btnNetJoin.Text = "&Join";
      // 
      // lstNetGames
      // 
      this.lstNetGames.IntegralHeight = false;
      this.lstNetGames.Location = new System.Drawing.Point(0, 112);
      this.lstNetGames.Name = "lstNetGames";
      this.lstNetGames.Size = new System.Drawing.Size(296, 88);
      this.lstNetGames.TabIndex = 4;
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
      this.lblPlayersNet.Size = new System.Drawing.Size(268, 16);
      this.lblPlayersNet.TabIndex = 0;
      this.lblPlayersNet.Text = "Connected Players:";
      // 
      // lblChat
      // 
      this.lblChat.Location = new System.Drawing.Point(0, 128);
      this.lblChat.Name = "lblChat";
      this.lblChat.Size = new System.Drawing.Size(268, 16);
      this.lblChat.TabIndex = 2;
      this.lblChat.Text = "Discussion:";
      // 
      // lstPlayersNet
      // 
      this.lstPlayersNet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    this.colName,
                                                                                    this.colPosition});
      this.lstPlayersNet.Location = new System.Drawing.Point(0, 28);
      this.lstPlayersNet.Name = "lstPlayersNet";
      this.lstPlayersNet.Size = new System.Drawing.Size(268, 88);
      this.lstPlayersNet.TabIndex = 1;
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
      this.btnSend.Location = new System.Drawing.Point(224, 224);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(44, 20);
      this.btnSend.TabIndex = 5;
      this.btnSend.Text = "&Send";
      // 
      // txtSend
      // 
      this.txtSend.Location = new System.Drawing.Point(0, 224);
      this.txtSend.Name = "txtSend";
      this.txtSend.Size = new System.Drawing.Size(220, 20);
      this.txtSend.TabIndex = 4;
      this.txtSend.Text = "";
      // 
      // txtChat
      // 
      this.txtChat.Location = new System.Drawing.Point(0, 144);
      this.txtChat.Name = "txtChat";
      this.txtChat.ReadOnly = true;
      this.txtChat.Size = new System.Drawing.Size(268, 76);
      this.txtChat.TabIndex = 3;
      this.txtChat.Text = "";
      // 
      // grpPlayerSettingsNet
      // 
      this.grpPlayerSettingsNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.lblJumpRuleNet,
                                                                                       this.cmbJumpRuleNet,
                                                                                       this.panImageSetNet,
                                                                                       this.cmbFirstMoveNet,
                                                                                       this.lblFirstMoveNet,
                                                                                       this.cmbImageSetNet,
                                                                                       this.lblImageSetNet});
      this.grpPlayerSettingsNet.Location = new System.Drawing.Point(280, 12);
      this.grpPlayerSettingsNet.Name = "grpPlayerSettingsNet";
      this.grpPlayerSettingsNet.Size = new System.Drawing.Size(268, 200);
      this.grpPlayerSettingsNet.TabIndex = 6;
      this.grpPlayerSettingsNet.TabStop = false;
      this.grpPlayerSettingsNet.Text = "Game Settings";
      // 
      // lblJumpRuleNet
      // 
      this.lblJumpRuleNet.Location = new System.Drawing.Point(8, 48);
      this.lblJumpRuleNet.Name = "lblJumpRuleNet";
      this.lblJumpRuleNet.Size = new System.Drawing.Size(84, 20);
      this.lblJumpRuleNet.TabIndex = 2;
      this.lblJumpRuleNet.Text = "Jump Rule:";
      this.lblJumpRuleNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cmbJumpRuleNet
      // 
      this.cmbJumpRuleNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbJumpRuleNet.Items.AddRange(new object[] {
                                                        "Forceful Jumping",
                                                        "Optional Jumping"});
      this.cmbJumpRuleNet.Location = new System.Drawing.Point(96, 48);
      this.cmbJumpRuleNet.Name = "cmbJumpRuleNet";
      this.cmbJumpRuleNet.Size = new System.Drawing.Size(164, 21);
      this.cmbJumpRuleNet.TabIndex = 3;
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
      this.panImageSetNet.Location = new System.Drawing.Point(8, 96);
      this.panImageSetNet.Name = "panImageSetNet";
      this.panImageSetNet.Size = new System.Drawing.Size(252, 92);
      this.panImageSetNet.TabIndex = 6;
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
      this.picKingNet1.Visible = false;
      this.picKingNet1.Click += new System.EventHandler(this.picImage_Click);
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
      this.picKingNet2.Visible = false;
      this.picKingNet2.Click += new System.EventHandler(this.picImage_Click);
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
      // cmbFirstMoveNet
      // 
      this.cmbFirstMoveNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFirstMoveNet.Items.AddRange(new object[] {
                                                         "Host",
                                                         "Opponent"});
      this.cmbFirstMoveNet.Location = new System.Drawing.Point(96, 24);
      this.cmbFirstMoveNet.Name = "cmbFirstMoveNet";
      this.cmbFirstMoveNet.Size = new System.Drawing.Size(164, 21);
      this.cmbFirstMoveNet.TabIndex = 1;
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
      // cmbImageSetNet
      // 
      this.cmbImageSetNet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbImageSetNet.Items.AddRange(new object[] {
                                                        "Standard",
                                                        "Standard (Black)",
                                                        "Tournament",
                                                        "Custom"});
      this.cmbImageSetNet.Location = new System.Drawing.Point(96, 72);
      this.cmbImageSetNet.Name = "cmbImageSetNet";
      this.cmbImageSetNet.Size = new System.Drawing.Size(164, 21);
      this.cmbImageSetNet.TabIndex = 5;
      this.cmbImageSetNet.SelectedIndexChanged += new System.EventHandler(this.cmbImageSet_SelectedIndexChanged);
      // 
      // lblImageSetNet
      // 
      this.lblImageSetNet.Location = new System.Drawing.Point(8, 72);
      this.lblImageSetNet.Name = "lblImageSetNet";
      this.lblImageSetNet.Size = new System.Drawing.Size(84, 20);
      this.lblImageSetNet.TabIndex = 4;
      this.lblImageSetNet.Text = "Image Set:";
      this.lblImageSetNet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
      // frmNewGame
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(606, 387);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.btnCancel,
                                                                  this.btnOK,
                                                                  this.mozGameType,
                                                                  this.tabGame});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "frmNewGame";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "New Checkers Game";
      this.Load += new System.EventHandler(this.frmNewGame_Load);
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
    
    public GameType GameType
    {
      get { return gameType; }
      set { gameType = value; }
    }
    
    public bool OptionalJumping
    { get { return optionalJumping; } }
    
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
    
    #endregion
    
    public new DialogResult ShowDialog (IWin32Window owner)
    {
      // Set control properties
      if (player1Name != "") txtPlayerName1P.Text = txtPlayerName2P1.Text = player1Name;
      if ((player2Name != "") && (gameType == GameType.Multiplayer)) txtPlayerName2P2.Text = player2Name;
      
      // Show dialog
      DialogResult result = base.ShowDialog(owner);
      
      // Set properties
      if (result != DialogResult.Cancel)
      {
        switch (tabGame.SelectedIndex)
        {
          case 0:
            gameType = GameType.SinglePlayer;
            optionalJumping = (cmbJumpRule1P.SelectedIndex != 0);
            firstMove = (( cmbFirstMove1P.SelectedIndex == 0 )?( 1 ):( 2 ));
            difficulty = (( cmbDifficulty1P.SelectedIndex == -1 )?( 0 ):( cmbDifficulty1P.SelectedIndex ));
            player1Name = (( txtPlayerName1P.Text != "" )?( txtPlayerName1P.Text ):( "Player" ));
            player2Name = "Opponent";
            imageSet[0] = picPawn1P1.Image; imageSet[1] = picKing1P1.Image;
            imageSet[2] = picPawn1P2.Image; imageSet[3] = picKing1P2.Image;
            break;
          case 1:
            gameType = GameType.Multiplayer;
            optionalJumping = (cmbJumpRule2P.SelectedIndex != 0);
            firstMove = (( cmbFirstMove2P.SelectedIndex == 0 )?( 1 ):( 2 ));
            difficulty = 0;
            player1Name = (( txtPlayerName2P1.Text != "" )?( txtPlayerName2P1.Text ):( "Player 1" ));
            player2Name = (( txtPlayerName2P2.Text != "" )?( txtPlayerName2P2.Text ):( "Player 2" ));
            imageSet[0] = picPawn2P1.Image; imageSet[1] = picKing2P1.Image;
            imageSet[2] = picPawn2P2.Image; imageSet[3] = picKing2P2.Image;
            break;
          case 2:
            gameType = GameType.NetGame;
            optionalJumping = (cmbJumpRuleNet.SelectedIndex != 0);
            firstMove = (( cmbFirstMoveNet.SelectedIndex == 0 )?( 1 ):( 2 ));
            difficulty = 0;
            // !!!!! Username
            //player1Name = (( txtPlayerName2P1.Text != "" )?( txtPlayerName2P1.Text ):( "Player 1" ));
            // !!!!! Downloaded name
            player2Name = "Opponent";
            // Downloaded images
            imageSet[0] = picPawnNet1.Image; imageSet[1] = picKingNet1.Image;
            imageSet[2] = picPawnNet2.Image; imageSet[3] = picKingNet2.Image;
            // !!!!! TCP/IP data
            break;
          default:
            MessageBox.Show(this, "New Game Dialog exited in an unrecognized tab; no settings were set.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            gameType = GameType.None;
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
      
      // Load presets
      EnumPresetMenuItems();
      
      // Select initial tab
      mozGameType.Items[0].SelectItem();
      
      // !!!!! Remember last values used
      cmbFirstMove1P.SelectedIndex = 0;
      cmbJumpRule1P.SelectedIndex = 0;
      cmbImageSet1P.SelectedIndex = 0;
      cmbDifficulty1P.SelectedIndex = 0;
      
      cmbFirstMove2P.SelectedIndex = 0;
      cmbJumpRule2P.SelectedIndex = 0;
      cmbImageSet2P.SelectedIndex = 0;
      
      cmbNetGameType.SelectedIndex = 0;
      cmbJumpRuleNet.SelectedIndex = 0;
      cmbFirstMoveNet.SelectedIndex = 0;
      cmbImageSetNet.SelectedIndex = 0;
      lnkIPAddress.Text = "0.0.0.0";        // !!!!!
      panNet.BringToFront();
      
      // Select initial control
      cmbDifficulty1P.Select();
    }
    
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
    
    private void tabGame_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if (tabGame.SelectedIndex == -1) return;
      mozGameType.Items[tabGame.SelectedIndex].SelectItem();
      
      switch (tabGame.SelectedIndex)
      {
        case 0: cmbDifficulty1P.Select(); break;
        case 1: txtPlayerName2P1.Select(); break;
        case 2: cmbNetGameType.Select(); break;
      }
    }
    private void mozGameType1P_Click (object sender, System.EventArgs e)
    { tabGame.SelectedTab = tabGame1P; }
    private void mozGameType2P_Click (object sender, System.EventArgs e)
    { tabGame.SelectedTab = tabGame2P; }
    private void mozGameTypeNet_Click (object sender, System.EventArgs e)
    { tabGame.SelectedTab = tabGameNet; }
    
    private void picImageSwap1P_Click (object sender, System.EventArgs e)
    { SwapImageSet(picPawn1P1, picPawn1P2); SwapImageSet(picKing1P1, picKing1P2); }
    private void picImageSwap2P_Click(object sender, System.EventArgs e)
    { SwapImageSet(picPawn2P1, picPawn2P2); SwapImageSet(picKing2P1, picKing2P2); }
    private void picImageSwapNet_Click(object sender, System.EventArgs e)
    { SwapImageSet(picPawnNet1, picPawnNet2); SwapImageSet(picKingNet1, picKingNet2); }
    
    private void cmbImageSet_SelectedIndexChanged (object sender, System.EventArgs e)
    { SetImageSet((ComboBox)sender); }
    
    private void picImage_Click (object sender, System.EventArgs e)
    {
      PictureBox pictureBox = sender as PictureBox;
      selectedPicture = pictureBox;
      menuImage.Show(pictureBox.Parent, new Point(pictureBox.Left, pictureBox.Top+pictureBox.Height));
    }
    private void picImage_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
    { picImage_Click(sender, EventArgs.Empty); }
    private void menuImage_Popup (object sender, System.EventArgs e)
    {
      menuImageDefault.Visible = ((selectedPicture == picKing1P1) || (selectedPicture == picKing1P2) || (selectedPicture == picKing2P1) || (selectedPicture == picKing2P2) || (selectedPicture == picKingNet1) || (selectedPicture == picKingNet2));
      menuImageLine.Visible = menuImageDefault.Visible;
    }
    
    
    private void OnCustomImageSet ()
    {
      if ((selectedPicture == picPawn1P1) || (selectedPicture == picKing1P1) || (selectedPicture == picPawn1P2) || (selectedPicture == picKing1P2)) cmbImageSet1P.SelectedIndex = 3;
      else if ((selectedPicture == picPawn2P1) || (selectedPicture == picKing2P1) || (selectedPicture == picPawn2P2) || (selectedPicture == picKing2P2)) cmbImageSet2P.SelectedIndex = 3;
      else if ((selectedPicture == picPawnNet1) || (selectedPicture == picKingNet1) || (selectedPicture == picPawnNet2) || (selectedPicture == picKingNet2)) cmbImageSet2P.SelectedIndex = 3;
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
      else if (sender == cmbImageSetNet) SetImageSet(cmbImageSetNet.SelectedIndex, picPawnNet1, picKingNet1, picPawnNet2, picKingNet2);
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
      else if (selectedPicture == picKingNet1) picKingNet1.Image = (Image)picKingNet1.Tag;
      else if (selectedPicture == picKingNet2) picKingNet2.Image = (Image)picKingNet2.Tag;
      OnCustomImageSet();
    }
    private void BrowseCustomImage ()
    {
      if (selectedPicture == picPawn1P1) BrowseCustomPawn(picPawn1P1, picKing1P1); else if (selectedPicture == picKing1P1) BrowseCustomKing(picKing1P1);
      else if (selectedPicture == picPawn1P2) BrowseCustomPawn(picPawn1P2, picKing1P2); else if (selectedPicture == picKing1P2) BrowseCustomKing(picKing1P2);
      else if (selectedPicture == picPawn2P1) BrowseCustomPawn(picPawn2P1, picKing2P1); else if (selectedPicture == picKing2P1) BrowseCustomKing(picKing2P1);
      else if (selectedPicture == picPawn2P2) BrowseCustomPawn(picPawn2P2, picKing2P2); else if (selectedPicture == picKing2P2) BrowseCustomKing(picKing2P2);
      else if (selectedPicture == picPawnNet1) BrowseCustomPawn(picPawnNet1, picKingNet1); else if (selectedPicture == picKingNet1) BrowseCustomKing(picKingNet1);
      else if (selectedPicture == picPawnNet2) BrowseCustomPawn(picPawnNet2, picKingNet2); else if (selectedPicture == picKingNet2) BrowseCustomKing(picKingNet2);
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
      if (selectedPicture == picPawn1P1) ChooseCustomPawnColor(picPawn1P1, picKing1P1); else if (selectedPicture == picKing1P1) ChooseCustomKingColor(picPawn1P1, picKing1P1);
      else if (selectedPicture == picPawn1P2) ChooseCustomPawnColor(picPawn1P2, picKing1P2); else if (selectedPicture == picKing1P2) ChooseCustomKingColor(picPawn1P1, picKing1P2);
      else if (selectedPicture == picPawn2P1) ChooseCustomPawnColor(picPawn2P1, picKing2P1); else if (selectedPicture == picKing2P1) ChooseCustomKingColor(picPawn2P1, picKing2P1);
      else if (selectedPicture == picPawn2P2) ChooseCustomPawnColor(picPawn2P2, picKing2P2); else if (selectedPicture == picKing2P2) ChooseCustomKingColor(picPawn2P1, picKing2P2);
      else if (selectedPicture == picPawnNet1) ChooseCustomPawnColor(picPawnNet1, picKingNet1); else if (selectedPicture == picKingNet1) ChooseCustomKingColor(picPawnNet1, picKingNet1);
      else if (selectedPicture == picPawnNet2) ChooseCustomPawnColor(picPawnNet2, picKingNet2); else if (selectedPicture == picKingNet2) ChooseCustomKingColor(picPawnNet1, picKingNet2);
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
      if (selectedPicture == picPawn1P1) SetPresetPawn(picPawn1P1, picKing1P1, pawn, king, color); else if (selectedPicture == picKing1P1) SetPresetKing(picKing1P1, king);
      else if (selectedPicture == picPawn1P2) SetPresetPawn(picPawn1P2, picKing1P2, pawn, king, color); else if (selectedPicture == picKing1P2) SetPresetKing(picKing1P2, king);
      else if (selectedPicture == picPawn2P1) SetPresetPawn(picPawn2P1, picKing2P1, pawn, king, color); else if (selectedPicture == picKing2P1) SetPresetKing(picKing2P1, king);
      else if (selectedPicture == picPawn2P2) SetPresetPawn(picPawn2P2, picKing2P2, pawn, king, color); else if (selectedPicture == picKing2P2) SetPresetKing(picKing2P2, king);
      else if (selectedPicture == picPawnNet1) SetPresetPawn(picPawnNet1, picKingNet1, pawn, king, color); else if (selectedPicture == picKingNet1) SetPresetKing(picKingNet1, king);
      else if (selectedPicture == picPawnNet2) SetPresetPawn(picPawnNet2, picKingNet2, pawn, king, color); else if (selectedPicture == picKingNet2) SetPresetKing(picKingNet2, king);
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
  }
}
