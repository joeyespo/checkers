using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Uberware.Gaming.Checkers;
using Uberware.Gaming.Checkers.Agents;

namespace Checkers
{
  public class frmMain : System.Windows.Forms.Form
  {
    private GameType gameType = GameType.None;
    private CheckersAgent agent = null;
    private CheckersSettings settings;
    private DateTime playTime;
    
    #region API Imports
    
    [DllImport("winmm.dll", EntryPoint="PlaySound", SetLastError=true, CallingConvention=CallingConvention.Winapi)]
    static extern bool sndPlaySound( string pszSound, IntPtr hMod, SoundFlags sf );

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
    
    #region Class Variables
    
    private System.Windows.Forms.Panel panGame;
    private System.Windows.Forms.MainMenu menuMain;
    private System.Windows.Forms.Panel panOnline;
    private System.Windows.Forms.MenuItem menuGame;
    private System.Windows.Forms.MenuItem menuGameNew;
    private System.Windows.Forms.MenuItem menuGameLine01;
    private System.Windows.Forms.MenuItem menuGameExit;
    private Uberware.Gaming.Checkers.UI.CheckersUI CheckersUI;
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
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.MenuItem menuGameEnd;
    private System.Windows.Forms.Label lblRemainingP1;
    private System.Windows.Forms.TextBox txtRemainingP1;
    private System.Windows.Forms.TextBox txtRemainingP2;
    private System.Windows.Forms.Label lblRemainingP2;
    private System.Windows.Forms.Timer tmrTimePassed;
    private System.Windows.Forms.MenuItem menuHelpAbout;
    
    #endregion
    
    #region Class Construction
    
    public frmMain()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }
    
    #region Windows Form Designer generated code
    
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
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
      this.menuViewPreferences = new System.Windows.Forms.MenuItem();
      this.menuHelp = new System.Windows.Forms.MenuItem();
      this.menuHelpAbout = new System.Windows.Forms.MenuItem();
      this.panOnline = new System.Windows.Forms.Panel();
      this.panNet = new System.Windows.Forms.Panel();
      this.splChat = new System.Windows.Forms.Splitter();
      this.panChat = new System.Windows.Forms.Panel();
      this.txtSend = new System.Windows.Forms.TextBox();
      this.btnSend = new System.Windows.Forms.Button();
      this.txtChat = new System.Windows.Forms.RichTextBox();
      this.CheckersUI = new Uberware.Gaming.Checkers.UI.CheckersUI();
      this.imlTurn = new System.Windows.Forms.ImageList(this.components);
      this.tmrTimePassed = new System.Windows.Forms.Timer(this.components);
      this.panGame.SuspendLayout();
      this.panGameInfo.SuspendLayout();
      this.panOnline.SuspendLayout();
      this.panChat.SuspendLayout();
      this.SuspendLayout();
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
      this.panGame.Size = new System.Drawing.Size(120, 272);
      this.panGame.TabIndex = 3;
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
      this.panGameInfo.Size = new System.Drawing.Size(120, 256);
      this.panGameInfo.TabIndex = 5;
      this.panGameInfo.Visible = false;
      // 
      // txtTimePassed
      // 
      this.txtTimePassed.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtTimePassed.Location = new System.Drawing.Point(72, 0);
      this.txtTimePassed.Name = "txtTimePassed";
      this.txtTimePassed.ReadOnly = true;
      this.txtTimePassed.Size = new System.Drawing.Size(48, 13);
      this.txtTimePassed.TabIndex = 2;
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
      this.txtJumpsP1.Size = new System.Drawing.Size(48, 13);
      this.txtJumpsP1.TabIndex = 12;
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
      this.lblNameP1.Size = new System.Drawing.Size(120, 16);
      this.lblNameP1.TabIndex = 7;
      this.lblNameP1.Text = "Player";
      // 
      // lblJumpsP1
      // 
      this.lblJumpsP1.Location = new System.Drawing.Point(0, 100);
      this.lblJumpsP1.Name = "lblJumpsP1";
      this.lblJumpsP1.Size = new System.Drawing.Size(68, 16);
      this.lblJumpsP1.TabIndex = 8;
      this.lblJumpsP1.Text = "Jumps:";
      this.lblJumpsP1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblRemainingP1
      // 
      this.lblRemainingP1.Location = new System.Drawing.Point(0, 84);
      this.lblRemainingP1.Name = "lblRemainingP1";
      this.lblRemainingP1.Size = new System.Drawing.Size(68, 16);
      this.lblRemainingP1.TabIndex = 4;
      this.lblRemainingP1.Text = "Remaining:";
      this.lblRemainingP1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtRemainingP1
      // 
      this.txtRemainingP1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtRemainingP1.Location = new System.Drawing.Point(72, 84);
      this.txtRemainingP1.Name = "txtRemainingP1";
      this.txtRemainingP1.ReadOnly = true;
      this.txtRemainingP1.Size = new System.Drawing.Size(48, 13);
      this.txtRemainingP1.TabIndex = 13;
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
      this.lblNameP2.Size = new System.Drawing.Size(120, 16);
      this.lblNameP2.TabIndex = 6;
      this.lblNameP2.Text = "Opponent";
      // 
      // txtRemainingP2
      // 
      this.txtRemainingP2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtRemainingP2.Location = new System.Drawing.Point(72, 188);
      this.txtRemainingP2.Name = "txtRemainingP2";
      this.txtRemainingP2.ReadOnly = true;
      this.txtRemainingP2.Size = new System.Drawing.Size(48, 13);
      this.txtRemainingP2.TabIndex = 14;
      this.txtRemainingP2.Text = "0";
      // 
      // lblJumpsP2
      // 
      this.lblJumpsP2.Location = new System.Drawing.Point(0, 204);
      this.lblJumpsP2.Name = "lblJumpsP2";
      this.lblJumpsP2.Size = new System.Drawing.Size(68, 16);
      this.lblJumpsP2.TabIndex = 5;
      this.lblJumpsP2.Text = "Jumps:";
      this.lblJumpsP2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblRemainingP2
      // 
      this.lblRemainingP2.Location = new System.Drawing.Point(0, 188);
      this.lblRemainingP2.Name = "lblRemainingP2";
      this.lblRemainingP2.Size = new System.Drawing.Size(68, 16);
      this.lblRemainingP2.TabIndex = 3;
      this.lblRemainingP2.Text = "Remaining:";
      this.lblRemainingP2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtJumpsP2
      // 
      this.txtJumpsP2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtJumpsP2.Location = new System.Drawing.Point(72, 204);
      this.txtJumpsP2.Name = "txtJumpsP2";
      this.txtJumpsP2.ReadOnly = true;
      this.txtJumpsP2.Size = new System.Drawing.Size(48, 13);
      this.txtJumpsP2.TabIndex = 11;
      this.txtJumpsP2.Text = "0";
      // 
      // lblTimePassed
      // 
      this.lblTimePassed.Name = "lblTimePassed";
      this.lblTimePassed.Size = new System.Drawing.Size(76, 16);
      this.lblTimePassed.TabIndex = 3;
      this.lblTimePassed.Text = "Time Passed:";
      // 
      // lblGameType
      // 
      this.lblGameType.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblGameType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblGameType.Name = "lblGameType";
      this.lblGameType.Size = new System.Drawing.Size(120, 16);
      this.lblGameType.TabIndex = 4;
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
      // menuViewPreferences
      // 
      this.menuViewPreferences.Index = 3;
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
      this.panOnline.Size = new System.Drawing.Size(396, 64);
      this.panOnline.TabIndex = 4;
      // 
      // panNet
      // 
      this.panNet.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panNet.Location = new System.Drawing.Point(276, 0);
      this.panNet.Name = "panNet";
      this.panNet.Size = new System.Drawing.Size(120, 64);
      this.panNet.TabIndex = 2;
      // 
      // splChat
      // 
      this.splChat.Location = new System.Drawing.Point(270, 0);
      this.splChat.Name = "splChat";
      this.splChat.Size = new System.Drawing.Size(6, 64);
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
      this.panChat.Size = new System.Drawing.Size(270, 64);
      this.panChat.TabIndex = 3;
      // 
      // txtSend
      // 
      this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.txtSend.Location = new System.Drawing.Point(0, 44);
      this.txtSend.Name = "txtSend";
      this.txtSend.Size = new System.Drawing.Size(226, 20);
      this.txtSend.TabIndex = 4;
      this.txtSend.Text = "";
      // 
      // btnSend
      // 
      this.btnSend.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnSend.Location = new System.Drawing.Point(228, 44);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(42, 20);
      this.btnSend.TabIndex = 3;
      this.btnSend.Text = "Send";
      // 
      // txtChat
      // 
      this.txtChat.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.txtChat.Name = "txtChat";
      this.txtChat.ReadOnly = true;
      this.txtChat.Size = new System.Drawing.Size(270, 42);
      this.txtChat.TabIndex = 1;
      this.txtChat.Text = "";
      // 
      // CheckersUI
      // 
      this.CheckersUI.Location = new System.Drawing.Point(4, 4);
      this.CheckersUI.Name = "CheckersUI";
      this.CheckersUI.Size = new System.Drawing.Size(270, 270);
      this.CheckersUI.TabIndex = 0;
      this.CheckersUI.GameStopped += new System.EventHandler(this.CheckersUI_GameStopped);
      this.CheckersUI.GameStarted += new System.EventHandler(this.CheckersUI_GameStarted);
      this.CheckersUI.WinnerDeclared += new System.EventHandler(this.CheckersUI_WinnerDeclared);
      this.CheckersUI.TurnChanged += new System.EventHandler(this.CheckersUI_TurnChanged);
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
      // frmMain
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(404, 349);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.CheckersUI,
                                                                  this.panOnline,
                                                                  this.panGame});
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Menu = this.menuMain;
      this.MinimumSize = new System.Drawing.Size(286, 324);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Checkers";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.panGame.ResumeLayout(false);
      this.panGameInfo.ResumeLayout(false);
      this.panOnline.ResumeLayout(false);
      this.panChat.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    
    #endregion
    
    #endregion
    
    #region Entry Point of Application
    
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
      Application.Run(new frmMain());
    }
    
    #endregion
    
    
    private void frmMain_Load (object sender, System.EventArgs e)
    {
      Size = new Size(MinimumSize.Width + 130, MinimumSize.Height);
      // !!!!! Load settings
      settings = CheckersSettings.Load();
      UpdateBoard();
    }
    private void frmMain_Closing (object sender, System.ComponentModel.CancelEventArgs e)
    { if (!DoCloseGame()) e.Cancel = true; }
    
    private void menuGameNew_Click(object sender, System.EventArgs e)
    { DoNewGame(); }
    private void menuGameEnd_Click (object sender, System.EventArgs e)
    { DoCloseGame(); }
    private void menuGameExit_Click (object sender, System.EventArgs e)
    { Close(); }
    
    private void menuView_Popup (object sender, System.EventArgs e)
    {
      menuViewGamePanel.Checked = (this.Width != this.MinimumSize.Width);
      menuViewNetPanel.Checked = (this.Height != this.MinimumSize.Height);
    }
    private void menuViewGamePanel_Click (object sender, System.EventArgs e)
    {
      menuViewGamePanel.Checked = !menuViewGamePanel.Checked;
      this.Width = (( menuViewGamePanel.Checked )?( this.MinimumSize.Width + 120 ):( this.MinimumSize.Width ));
    }
    private void menuViewNetPanel_Click (object sender, System.EventArgs e)
    {
      menuViewNetPanel.Checked = !menuViewNetPanel.Checked;
      this.Height = (( menuViewNetPanel.Checked )?( this.MinimumSize.Height + 80 ):( this.MinimumSize.Height ));
    }
    private void menuViewPreferences_Click (object sender, System.EventArgs e)
    {
      frmPreferences form = new frmPreferences();
      form.Settings = settings;
      if (form.ShowDialog(this) == DialogResult.Cancel) return;
      settings = form.Settings;
      UpdateBoard();
    }
    
    private void menuHelpAbout_Click (object sender, System.EventArgs e)
    { (new frmAbout()).ShowDialog(this); }
    
    private void CheckersUI_GameStarted (object sender, System.EventArgs e)
    { DoStarted(); }
    private void CheckersUI_GameStopped (object sender, System.EventArgs e)
    { DoStopped(); }
    private void CheckersUI_TurnChanged(object sender, System.EventArgs e)
    { DoNextTurn(); }
    private void CheckersUI_WinnerDeclared (object sender, System.EventArgs e)
    { DoWinnerDeclared(); }
    
    private void tmrTimePassed_Tick (object sender, System.EventArgs e)
    { DoUpdateTimePassed(); }
    
    
    private void DoNewGame ()
    {
      if ((CheckersUI.IsPlaying) || (CheckersUI.Winner != 0))
      {
        if (!DoCloseGame()) return;
        // Stop current game (with no winner)
        CheckersUI.Stop();
      }
      
      // Get new game type
      frmNewGame newGame = new frmNewGame();
      // Set defaults
      newGame.GameType = gameType;
      newGame.Player1Name = lblNameP1.Text;
      newGame.Player2Name = lblNameP2.Text;
      
      // Show dialog
      if (newGame.ShowDialog(this) == DialogResult.Cancel) return;
      
      // Set new game parameters
      gameType = newGame.GameType;
      
      // Set Game Panel properties
      lblNameP1.Text = newGame.Player1Name; lblNameP2.Text = newGame.Player2Name;
      picPawnP1.Image = newGame.ImageSet[0]; picPawnP2.Image = newGame.ImageSet[2];
      
      // Set UI properties
      switch (gameType)
      {
        case GameType.SinglePlayer:
          CheckersUI.Player1Active = true;
          CheckersUI.Player2Active = false;
        switch (newGame.Difficulty)
        {
          case 0: agent = new CheckersRandomAgent(); break;
          case 1: agent = new CheckersMostJumpsAgent(); break;
          default: return;
        }
          break;
        case GameType.Multiplayer:
          CheckersUI.Player1Active = true;
          CheckersUI.Player2Active = true;
          break;
        case GameType.NetGame:
          CheckersUI.Player1Active = true;
          CheckersUI.Player2Active = false;
          break;
        default: return;
      }
      CheckersUI.CustomPawn1 = newGame.ImageSet[0]; CheckersUI.CustomKing1 = newGame.ImageSet[1];
      CheckersUI.CustomPawn2 = newGame.ImageSet[2]; CheckersUI.CustomKing2 = newGame.ImageSet[3];
      
      // Create the game !!!!! Start with newGame.Game ??
      CheckersGame game = new CheckersGame();
      game.FirstMove = newGame.FirstMove;
      game.OptionalJumping = newGame.OptionalJumping;
      // !!!!! game.OptionalJumping
      
      // Start a new checkers game
      CheckersUI.Play(game);
    }
    
    private void DoStarted ()
    {
      playTime = DateTime.Now;
      tmrTimePassed.Start(); DoUpdateTimePassed();
      panGameInfo.Visible = true;
      menuGameEnd.Enabled = true;
      DoNextTurn();
    }
    private void DoStopped ()
    {
      panGameInfo.Visible = false;
      menuGameEnd.Enabled = false;
      tmrTimePassed.Stop(); txtTimePassed.Text = "0:00";
    }
    private void DoNextTurn ()
    {
      DoShowTurn(CheckersUI.Game.Turn);
      if ((gameType == GameType.SinglePlayer) && (CheckersUI.Game.Turn == 2))
        CheckersUI.MovePiece(agent);        // !!!!! Move after time interval
      UpdatePlayerInfo();
    }
    private void DoWinnerDeclared ()
    {
      UpdatePlayerInfo();
      tmrTimePassed.Stop(); DoUpdateTimePassed();
      DoShowWinner(CheckersUI.Winner);
    }
    
    private bool DoCloseGame ()
    {
      if ((!CheckersUI.IsPlaying) && (CheckersUI.Winner == 0)) return true;
      // !!!!! Ask for 'stalemate' in multiplayer mode
      if (CheckersUI.IsPlaying)
      {
        // Confirm the quit
        if (MessageBox.Show(this, "Quit current game?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return false;
      }
      picTurn.Visible = false;
      CheckersUI.Stop();
      return true;
    }
    
    private void DoUpdateTimePassed ()
    {
      TimeSpan time = DateTime.Now.Subtract(playTime);
      txtTimePassed.Text = ((int)time.TotalMinutes).ToString().PadLeft(2, '0') + ":" + time.Seconds.ToString().PadLeft(2, '0');
    }
    private void DoShowTurn (int player)
    {
      picTurn.Visible = false;
      lblNameP1.BackColor = Color.FromKnownColor(KnownColor.Control); lblNameP1.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
      lblNameP2.BackColor = Color.FromKnownColor(KnownColor.Control); lblNameP2.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
      if (player == 1)
      {
        picTurn.Image = imlTurn.Images[0];
        picTurn.Top = picPawnP1.Top + 1;
        picTurn.Visible = true;
        lblNameP1.BackColor = Color.FromKnownColor(KnownColor.Highlight); lblNameP1.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
      }
      else if (player == 2)
      {
        picTurn.Image = imlTurn.Images[(( gameType == GameType.Multiplayer )?( 0 ):( 1 ))];
        picTurn.Top = picPawnP2.Top + 1;
        picTurn.Visible = true;
        lblNameP2.BackColor = Color.FromKnownColor(KnownColor.Highlight); lblNameP2.ForeColor = Color.FromKnownColor(KnownColor.HighlightText);
      }
    }
    private void DoShowWinner (int player)
    {
      picTurn.Visible = false;
      lblNameP1.BackColor = Color.FromKnownColor(KnownColor.Control); lblNameP1.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
      lblNameP2.BackColor = Color.FromKnownColor(KnownColor.Control); lblNameP2.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
      if (player == 1)
      {
        picTurn.Image = imlTurn.Images[2];
        picTurn.Top = picPawnP1.Top + 1;
        picTurn.Visible = true;
      }
      else if (player == 2)
      {
        picTurn.Image = imlTurn.Images[2];
        picTurn.Top = picPawnP2.Top + 1;
        picTurn.Visible = true;
      }
    }
    
    private void UpdatePlayerInfo ()
    {
      // Update player information
      txtRemainingP1.Text = CheckersUI.Game.GetRemainingCount(1).ToString();
      txtJumpsP1.Text = CheckersUI.Game.GetJumpedCount(2).ToString();
      txtRemainingP2.Text = CheckersUI.Game.GetRemainingCount(2).ToString();
      txtJumpsP2.Text = CheckersUI.Game.GetJumpedCount(1).ToString();
    }
    
    private void UpdateBoard ()
    {
      CheckersUI.BackColor = settings.BackColor;
      CheckersUI.BoardBackColor = settings.BoardBackColor;
      CheckersUI.BoardForeColor = settings.BoardForeColor;
      CheckersUI.BoardGridColor = settings.BoardGridColor;
    }
    
    private void PlaySound (CheckersSounds sound)
    {
      // Play sound
      if (settings.MuteSounds) return;
      sndPlaySound(settings.sounds[(int)sound], IntPtr.Zero, (SoundFlags.SND_FileName | SoundFlags.SND_ASYNC | SoundFlags.SND_NOWAIT));
    }
  }
}
