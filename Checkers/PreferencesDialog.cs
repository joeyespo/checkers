using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Checkers
{
  public class PreferencesDialog : System.Windows.Forms.Form
  {
    
    private CheckersSettings settings;
    private System.Windows.Forms.CheckBox chkMuteSounds;
    private System.Windows.Forms.Button btnDefault;
    private System.Windows.Forms.GroupBox grpGeneral;
    private System.Windows.Forms.CheckBox chkShowJumpMessage;
    private System.Windows.Forms.CheckBox chkHighlightSelection;
    private System.Windows.Forms.CheckBox chkHighlightPossibleMoves;
    private System.Windows.Forms.CheckBox chkShowTextFeedback;
    private System.Windows.Forms.MenuItem menuSetDefault;
    private System.Windows.Forms.ContextMenu menuColor;
    private System.Windows.Forms.MenuItem menuChangeColor;
    private System.Windows.Forms.MenuItem menuColorLine01;
    private System.Windows.Forms.CheckBox chkFlashWindowOnGameEvents;
    private System.Windows.Forms.Label lblColorCaption;
    private System.Windows.Forms.Panel panTitle;
    private System.Windows.Forms.Label lblTitle;
    private string [] sounds;
    
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
    
    #region Class Controls
    
    private System.Windows.Forms.OpenFileDialog dlgOpenSound;
    private System.Windows.Forms.Label lblBoardBackColor;
    private System.Windows.Forms.PictureBox picBoardBackColor;
    private System.Windows.Forms.Label lblBackColor;
    private System.Windows.Forms.PictureBox picBackColor;
    private System.Windows.Forms.Label lblBoardForeColor;
    private System.Windows.Forms.PictureBox picBoardForeColor;
    private System.Windows.Forms.PictureBox picBoardGridColor;
    private System.Windows.Forms.Label lblBoardGridColor;
    private System.Windows.Forms.TabControl tabPreferences;
    private System.Windows.Forms.TabPage tabGeneral;
    private System.Windows.Forms.TabPage tabBoard;
    private System.Windows.Forms.TabPage tabSounds;
    private System.Windows.Forms.ImageList imlTabs;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.GroupBox grpNet;
    private System.Windows.Forms.CheckBox chkShowNetPanelOnMessage;
    private System.Windows.Forms.CheckBox chkFlashWindowOnTurn;
    private System.Windows.Forms.CheckBox chkFlashWindowOnMessage;
    private System.Windows.Forms.ColorDialog dlgColorDialog;
    private System.Windows.Forms.ListBox lstSounds;
    private System.Windows.Forms.Label lblSounds;
    private System.Windows.Forms.Button btnSoundFile;
    private System.Windows.Forms.TextBox txtSoundFile;
    private System.Windows.Forms.Button btnSoundPreview;
    private System.Windows.Forms.Label lblSoundFile;
    private System.ComponentModel.IContainer components;
    
    #endregion
    
    #region Class Construction
    
    public PreferencesDialog()
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PreferencesDialog));
      this.imlTabs = new System.Windows.Forms.ImageList(this.components);
      this.tabPreferences = new System.Windows.Forms.TabControl();
      this.tabGeneral = new System.Windows.Forms.TabPage();
      this.grpGeneral = new System.Windows.Forms.GroupBox();
      this.chkShowJumpMessage = new System.Windows.Forms.CheckBox();
      this.chkHighlightSelection = new System.Windows.Forms.CheckBox();
      this.chkHighlightPossibleMoves = new System.Windows.Forms.CheckBox();
      this.chkShowTextFeedback = new System.Windows.Forms.CheckBox();
      this.grpNet = new System.Windows.Forms.GroupBox();
      this.chkFlashWindowOnTurn = new System.Windows.Forms.CheckBox();
      this.chkFlashWindowOnMessage = new System.Windows.Forms.CheckBox();
      this.chkShowNetPanelOnMessage = new System.Windows.Forms.CheckBox();
      this.chkFlashWindowOnGameEvents = new System.Windows.Forms.CheckBox();
      this.tabBoard = new System.Windows.Forms.TabPage();
      this.lblColorCaption = new System.Windows.Forms.Label();
      this.lblBoardBackColor = new System.Windows.Forms.Label();
      this.picBoardBackColor = new System.Windows.Forms.PictureBox();
      this.menuColor = new System.Windows.Forms.ContextMenu();
      this.menuChangeColor = new System.Windows.Forms.MenuItem();
      this.menuColorLine01 = new System.Windows.Forms.MenuItem();
      this.menuSetDefault = new System.Windows.Forms.MenuItem();
      this.lblBackColor = new System.Windows.Forms.Label();
      this.picBackColor = new System.Windows.Forms.PictureBox();
      this.lblBoardForeColor = new System.Windows.Forms.Label();
      this.picBoardForeColor = new System.Windows.Forms.PictureBox();
      this.picBoardGridColor = new System.Windows.Forms.PictureBox();
      this.lblBoardGridColor = new System.Windows.Forms.Label();
      this.tabSounds = new System.Windows.Forms.TabPage();
      this.chkMuteSounds = new System.Windows.Forms.CheckBox();
      this.lstSounds = new System.Windows.Forms.ListBox();
      this.lblSounds = new System.Windows.Forms.Label();
      this.txtSoundFile = new System.Windows.Forms.TextBox();
      this.btnSoundFile = new System.Windows.Forms.Button();
      this.btnSoundPreview = new System.Windows.Forms.Button();
      this.lblSoundFile = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.dlgColorDialog = new System.Windows.Forms.ColorDialog();
      this.dlgOpenSound = new System.Windows.Forms.OpenFileDialog();
      this.btnDefault = new System.Windows.Forms.Button();
      this.panTitle = new System.Windows.Forms.Panel();
      this.lblTitle = new System.Windows.Forms.Label();
      this.tabPreferences.SuspendLayout();
      this.tabGeneral.SuspendLayout();
      this.grpGeneral.SuspendLayout();
      this.grpNet.SuspendLayout();
      this.tabBoard.SuspendLayout();
      this.tabSounds.SuspendLayout();
      this.panTitle.SuspendLayout();
      this.SuspendLayout();
      // 
      // imlTabs
      // 
      this.imlTabs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imlTabs.ImageSize = new System.Drawing.Size(32, 32);
      this.imlTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTabs.ImageStream")));
      this.imlTabs.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // tabPreferences
      // 
      this.tabPreferences.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.tabPreferences.Appearance = System.Windows.Forms.TabAppearance.Buttons;
      this.tabPreferences.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.tabGeneral,
                                                                                 this.tabBoard,
                                                                                 this.tabSounds});
      this.tabPreferences.ImageList = this.imlTabs;
      this.tabPreferences.Location = new System.Drawing.Point(8, 8);
      this.tabPreferences.Multiline = true;
      this.tabPreferences.Name = "tabPreferences";
      this.tabPreferences.SelectedIndex = 0;
      this.tabPreferences.Size = new System.Drawing.Size(396, 324);
      this.tabPreferences.TabIndex = 1;
      this.tabPreferences.SelectedIndexChanged += new System.EventHandler(this.tabPreferences_SelectedIndexChanged);
      // 
      // tabGeneral
      // 
      this.tabGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.grpGeneral,
                                                                             this.grpNet});
      this.tabGeneral.ImageIndex = 0;
      this.tabGeneral.Location = new System.Drawing.Point(4, 42);
      this.tabGeneral.Name = "tabGeneral";
      this.tabGeneral.Size = new System.Drawing.Size(388, 278);
      this.tabGeneral.TabIndex = 0;
      this.tabGeneral.Text = "General";
      // 
      // grpGeneral
      // 
      this.grpGeneral.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.grpGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.chkShowJumpMessage,
                                                                             this.chkHighlightSelection,
                                                                             this.chkHighlightPossibleMoves,
                                                                             this.chkShowTextFeedback});
      this.grpGeneral.Location = new System.Drawing.Point(0, 36);
      this.grpGeneral.Name = "grpGeneral";
      this.grpGeneral.Size = new System.Drawing.Size(386, 112);
      this.grpGeneral.TabIndex = 0;
      this.grpGeneral.TabStop = false;
      this.grpGeneral.Text = "General";
      // 
      // chkShowJumpMessage
      // 
      this.chkShowJumpMessage.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkShowJumpMessage.Location = new System.Drawing.Point(8, 84);
      this.chkShowJumpMessage.Name = "chkShowJumpMessage";
      this.chkShowJumpMessage.Size = new System.Drawing.Size(370, 20);
      this.chkShowJumpMessage.TabIndex = 3;
      this.chkShowJumpMessage.Text = "Show detailed message when a jump must be completed";
      // 
      // chkHighlightSelection
      // 
      this.chkHighlightSelection.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkHighlightSelection.Location = new System.Drawing.Point(8, 24);
      this.chkHighlightSelection.Name = "chkHighlightSelection";
      this.chkHighlightSelection.Size = new System.Drawing.Size(370, 20);
      this.chkHighlightSelection.TabIndex = 0;
      this.chkHighlightSelection.Text = "Highlight selected squares";
      // 
      // chkHighlightPossibleMoves
      // 
      this.chkHighlightPossibleMoves.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkHighlightPossibleMoves.Location = new System.Drawing.Point(8, 44);
      this.chkHighlightPossibleMoves.Name = "chkHighlightPossibleMoves";
      this.chkHighlightPossibleMoves.Size = new System.Drawing.Size(370, 20);
      this.chkHighlightPossibleMoves.TabIndex = 1;
      this.chkHighlightPossibleMoves.Text = "Highlight possible moves";
      // 
      // chkShowTextFeedback
      // 
      this.chkShowTextFeedback.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkShowTextFeedback.Location = new System.Drawing.Point(8, 64);
      this.chkShowTextFeedback.Name = "chkShowTextFeedback";
      this.chkShowTextFeedback.Size = new System.Drawing.Size(370, 20);
      this.chkShowTextFeedback.TabIndex = 2;
      this.chkShowTextFeedback.Text = "Show text feedback";
      // 
      // grpNet
      // 
      this.grpNet.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.grpNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                         this.chkFlashWindowOnTurn,
                                                                         this.chkFlashWindowOnMessage,
                                                                         this.chkShowNetPanelOnMessage,
                                                                         this.chkFlashWindowOnGameEvents});
      this.grpNet.Location = new System.Drawing.Point(0, 160);
      this.grpNet.Name = "grpNet";
      this.grpNet.Size = new System.Drawing.Size(386, 112);
      this.grpNet.TabIndex = 1;
      this.grpNet.TabStop = false;
      this.grpNet.Text = "Net Settings";
      // 
      // chkFlashWindowOnTurn
      // 
      this.chkFlashWindowOnTurn.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkFlashWindowOnTurn.Location = new System.Drawing.Point(8, 44);
      this.chkFlashWindowOnTurn.Name = "chkFlashWindowOnTurn";
      this.chkFlashWindowOnTurn.Size = new System.Drawing.Size(370, 20);
      this.chkFlashWindowOnTurn.TabIndex = 1;
      this.chkFlashWindowOnTurn.Text = "Flash window when your turn";
      // 
      // chkFlashWindowOnMessage
      // 
      this.chkFlashWindowOnMessage.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkFlashWindowOnMessage.Location = new System.Drawing.Point(8, 64);
      this.chkFlashWindowOnMessage.Name = "chkFlashWindowOnMessage";
      this.chkFlashWindowOnMessage.Size = new System.Drawing.Size(370, 20);
      this.chkFlashWindowOnMessage.TabIndex = 2;
      this.chkFlashWindowOnMessage.Text = "Flash window when a message is received";
      // 
      // chkShowNetPanelOnMessage
      // 
      this.chkShowNetPanelOnMessage.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkShowNetPanelOnMessage.Location = new System.Drawing.Point(8, 84);
      this.chkShowNetPanelOnMessage.Name = "chkShowNetPanelOnMessage";
      this.chkShowNetPanelOnMessage.Size = new System.Drawing.Size(370, 20);
      this.chkShowNetPanelOnMessage.TabIndex = 3;
      this.chkShowNetPanelOnMessage.Text = "Show Net Panel when message is received";
      // 
      // chkFlashWindowOnGameEvents
      // 
      this.chkFlashWindowOnGameEvents.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkFlashWindowOnGameEvents.Location = new System.Drawing.Point(8, 24);
      this.chkFlashWindowOnGameEvents.Name = "chkFlashWindowOnGameEvents";
      this.chkFlashWindowOnGameEvents.Size = new System.Drawing.Size(370, 20);
      this.chkFlashWindowOnGameEvents.TabIndex = 0;
      this.chkFlashWindowOnGameEvents.Text = "Flash window on game events";
      // 
      // tabBoard
      // 
      this.tabBoard.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                           this.lblColorCaption,
                                                                           this.lblBoardBackColor,
                                                                           this.picBoardBackColor,
                                                                           this.lblBackColor,
                                                                           this.picBackColor,
                                                                           this.lblBoardForeColor,
                                                                           this.picBoardForeColor,
                                                                           this.picBoardGridColor,
                                                                           this.lblBoardGridColor});
      this.tabBoard.ImageIndex = 1;
      this.tabBoard.Location = new System.Drawing.Point(4, 42);
      this.tabBoard.Name = "tabBoard";
      this.tabBoard.Size = new System.Drawing.Size(388, 278);
      this.tabBoard.TabIndex = 1;
      this.tabBoard.Text = "Appearance";
      // 
      // lblColorCaption
      // 
      this.lblColorCaption.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblColorCaption.Location = new System.Drawing.Point(12, 188);
      this.lblColorCaption.Name = "lblColorCaption";
      this.lblColorCaption.Size = new System.Drawing.Size(370, 32);
      this.lblColorCaption.TabIndex = 4;
      this.lblColorCaption.Text = "Note: Right-click to reset to default";
      // 
      // lblBoardBackColor
      // 
      this.lblBoardBackColor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblBoardBackColor.Location = new System.Drawing.Point(45, 80);
      this.lblBoardBackColor.Name = "lblBoardBackColor";
      this.lblBoardBackColor.Size = new System.Drawing.Size(334, 16);
      this.lblBoardBackColor.TabIndex = 1;
      this.lblBoardBackColor.Text = "Board Background Color";
      // 
      // picBoardBackColor
      // 
      this.picBoardBackColor.BackColor = System.Drawing.Color.White;
      this.picBoardBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBoardBackColor.ContextMenu = this.menuColor;
      this.picBoardBackColor.Location = new System.Drawing.Point(9, 72);
      this.picBoardBackColor.Name = "picBoardBackColor";
      this.picBoardBackColor.Size = new System.Drawing.Size(32, 32);
      this.picBoardBackColor.TabIndex = 7;
      this.picBoardBackColor.TabStop = false;
      this.picBoardBackColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picColor_MouseDown);
      // 
      // menuColor
      // 
      this.menuColor.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuChangeColor,
                                                                              this.menuColorLine01,
                                                                              this.menuSetDefault});
      // 
      // menuChangeColor
      // 
      this.menuChangeColor.Index = 0;
      this.menuChangeColor.Text = "&Change Color...";
      this.menuChangeColor.Click += new System.EventHandler(this.menuChangeColor_Click);
      // 
      // menuColorLine01
      // 
      this.menuColorLine01.Index = 1;
      this.menuColorLine01.Text = "-";
      // 
      // menuSetDefault
      // 
      this.menuSetDefault.Index = 2;
      this.menuSetDefault.Text = "Set to &Default";
      this.menuSetDefault.Click += new System.EventHandler(this.menuSetDefault_Click);
      // 
      // lblBackColor
      // 
      this.lblBackColor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblBackColor.Location = new System.Drawing.Point(45, 44);
      this.lblBackColor.Name = "lblBackColor";
      this.lblBackColor.Size = new System.Drawing.Size(334, 16);
      this.lblBackColor.TabIndex = 0;
      this.lblBackColor.Text = "Background Color";
      // 
      // picBackColor
      // 
      this.picBackColor.BackColor = System.Drawing.Color.White;
      this.picBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBackColor.ContextMenu = this.menuColor;
      this.picBackColor.Location = new System.Drawing.Point(9, 36);
      this.picBackColor.Name = "picBackColor";
      this.picBackColor.Size = new System.Drawing.Size(32, 32);
      this.picBackColor.TabIndex = 4;
      this.picBackColor.TabStop = false;
      this.picBackColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picColor_MouseDown);
      // 
      // lblBoardForeColor
      // 
      this.lblBoardForeColor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblBoardForeColor.Location = new System.Drawing.Point(45, 116);
      this.lblBoardForeColor.Name = "lblBoardForeColor";
      this.lblBoardForeColor.Size = new System.Drawing.Size(334, 16);
      this.lblBoardForeColor.TabIndex = 2;
      this.lblBoardForeColor.Text = "Board Foreground Color";
      // 
      // picBoardForeColor
      // 
      this.picBoardForeColor.BackColor = System.Drawing.Color.White;
      this.picBoardForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBoardForeColor.ContextMenu = this.menuColor;
      this.picBoardForeColor.Location = new System.Drawing.Point(9, 108);
      this.picBoardForeColor.Name = "picBoardForeColor";
      this.picBoardForeColor.Size = new System.Drawing.Size(32, 32);
      this.picBoardForeColor.TabIndex = 5;
      this.picBoardForeColor.TabStop = false;
      this.picBoardForeColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picColor_MouseDown);
      // 
      // picBoardGridColor
      // 
      this.picBoardGridColor.BackColor = System.Drawing.Color.White;
      this.picBoardGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBoardGridColor.ContextMenu = this.menuColor;
      this.picBoardGridColor.Location = new System.Drawing.Point(9, 144);
      this.picBoardGridColor.Name = "picBoardGridColor";
      this.picBoardGridColor.Size = new System.Drawing.Size(32, 32);
      this.picBoardGridColor.TabIndex = 6;
      this.picBoardGridColor.TabStop = false;
      this.picBoardGridColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picColor_MouseDown);
      // 
      // lblBoardGridColor
      // 
      this.lblBoardGridColor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblBoardGridColor.Location = new System.Drawing.Point(45, 152);
      this.lblBoardGridColor.Name = "lblBoardGridColor";
      this.lblBoardGridColor.Size = new System.Drawing.Size(334, 16);
      this.lblBoardGridColor.TabIndex = 3;
      this.lblBoardGridColor.Text = "Board Foreground Color";
      // 
      // tabSounds
      // 
      this.tabSounds.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.chkMuteSounds,
                                                                            this.lstSounds,
                                                                            this.lblSounds,
                                                                            this.txtSoundFile,
                                                                            this.btnSoundFile,
                                                                            this.btnSoundPreview,
                                                                            this.lblSoundFile});
      this.tabSounds.ImageIndex = 2;
      this.tabSounds.Location = new System.Drawing.Point(4, 42);
      this.tabSounds.Name = "tabSounds";
      this.tabSounds.Size = new System.Drawing.Size(388, 278);
      this.tabSounds.TabIndex = 2;
      this.tabSounds.Text = "Sounds";
      // 
      // chkMuteSounds
      // 
      this.chkMuteSounds.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.chkMuteSounds.Location = new System.Drawing.Point(0, 256);
      this.chkMuteSounds.Name = "chkMuteSounds";
      this.chkMuteSounds.Size = new System.Drawing.Size(386, 20);
      this.chkMuteSounds.TabIndex = 6;
      this.chkMuteSounds.Text = "Mute all Sounds";
      // 
      // lstSounds
      // 
      this.lstSounds.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lstSounds.IntegralHeight = false;
      this.lstSounds.Location = new System.Drawing.Point(0, 56);
      this.lstSounds.Name = "lstSounds";
      this.lstSounds.Size = new System.Drawing.Size(386, 148);
      this.lstSounds.TabIndex = 1;
      this.lstSounds.SelectedIndexChanged += new System.EventHandler(this.lstSounds_SelectedIndexChanged);
      // 
      // lblSounds
      // 
      this.lblSounds.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblSounds.Location = new System.Drawing.Point(0, 36);
      this.lblSounds.Name = "lblSounds";
      this.lblSounds.Size = new System.Drawing.Size(390, 20);
      this.lblSounds.TabIndex = 0;
      this.lblSounds.Text = "Game Sounds:";
      this.lblSounds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtSoundFile
      // 
      this.txtSoundFile.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.txtSoundFile.Location = new System.Drawing.Point(0, 228);
      this.txtSoundFile.Name = "txtSoundFile";
      this.txtSoundFile.Size = new System.Drawing.Size(338, 20);
      this.txtSoundFile.TabIndex = 3;
      this.txtSoundFile.Text = "";
      this.txtSoundFile.TextChanged += new System.EventHandler(this.txtSoundFile_TextChanged);
      // 
      // btnSoundFile
      // 
      this.btnSoundFile.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnSoundFile.Location = new System.Drawing.Point(366, 228);
      this.btnSoundFile.Name = "btnSoundFile";
      this.btnSoundFile.Size = new System.Drawing.Size(20, 20);
      this.btnSoundFile.TabIndex = 5;
      this.btnSoundFile.Text = "..";
      this.btnSoundFile.Click += new System.EventHandler(this.btnSoundFile_Click);
      // 
      // btnSoundPreview
      // 
      this.btnSoundPreview.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnSoundPreview.Location = new System.Drawing.Point(342, 228);
      this.btnSoundPreview.Name = "btnSoundPreview";
      this.btnSoundPreview.Size = new System.Drawing.Size(20, 20);
      this.btnSoundPreview.TabIndex = 4;
      this.btnSoundPreview.Text = "!!";
      this.btnSoundPreview.Click += new System.EventHandler(this.btnSoundPreview_Click);
      // 
      // lblSoundFile
      // 
      this.lblSoundFile.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblSoundFile.Location = new System.Drawing.Point(0, 208);
      this.lblSoundFile.Name = "lblSoundFile";
      this.lblSoundFile.Size = new System.Drawing.Size(390, 20);
      this.lblSoundFile.TabIndex = 2;
      this.lblSoundFile.Text = "Sound File:";
      this.lblSoundFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(314, 337);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(88, 36);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "&Cancel";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(218, 337);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(88, 36);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      // 
      // dlgColorDialog
      // 
      this.dlgColorDialog.AnyColor = true;
      this.dlgColorDialog.FullOpen = true;
      // 
      // dlgOpenSound
      // 
      this.dlgOpenSound.DefaultExt = "wav";
      this.dlgOpenSound.Filter = "Wave Files (*.wav)|*.wav|All Files (*.*)|*.*";
      this.dlgOpenSound.ReadOnlyChecked = true;
      this.dlgOpenSound.Title = "Browse for Sound";
      // 
      // btnDefault
      // 
      this.btnDefault.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnDefault.Location = new System.Drawing.Point(112, 337);
      this.btnDefault.Name = "btnDefault";
      this.btnDefault.Size = new System.Drawing.Size(88, 36);
      this.btnDefault.TabIndex = 2;
      this.btnDefault.Text = "&Default";
      this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
      // 
      // panTitle
      // 
      this.panTitle.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.panTitle.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                           this.lblTitle});
      this.panTitle.Location = new System.Drawing.Point(7, 48);
      this.panTitle.Name = "panTitle";
      this.panTitle.Size = new System.Drawing.Size(396, 28);
      this.panTitle.TabIndex = 5;
      // 
      // lblTitle
      // 
      this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblTitle.BackColor = System.Drawing.SystemColors.Highlight;
      this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.lblTitle.Location = new System.Drawing.Point(0, 4);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(394, 24);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "General";
      // 
      // frmPreferences
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(410, 380);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.panTitle,
                                                                  this.btnDefault,
                                                                  this.btnCancel,
                                                                  this.btnOK,
                                                                  this.tabPreferences});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmPreferences";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Checkers Preferences";
      this.Load += new System.EventHandler(this.frmPreferences_Load);
      this.tabPreferences.ResumeLayout(false);
      this.tabGeneral.ResumeLayout(false);
      this.grpGeneral.ResumeLayout(false);
      this.grpNet.ResumeLayout(false);
      this.tabBoard.ResumeLayout(false);
      this.tabSounds.ResumeLayout(false);
      this.panTitle.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    
    #endregion

    private void tabPreferences_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if (tabPreferences.SelectedIndex == -1) return;
      lblTitle.Text = tabPreferences.SelectedTab.Text;
    }
    
    #endregion
    
    #region Public Properties
    
    public CheckersSettings Settings
    {
      get { return settings; }
      set { settings = value; }
    }
    
    #endregion
    
    public new DialogResult ShowDialog (IWin32Window owner)
    {
      if (settings == null) settings = new CheckersSettings();
      
      // Show settings
      ShowSettings();
      
      // Show dialog
      DialogResult result = base.ShowDialog(owner);
      
      // Set properties
      if (result != DialogResult.Cancel)
      {
        settings = new CheckersSettings();
        // General settings
        settings.HighlightSelection = chkHighlightSelection.Checked;
        settings.HighlightPossibleMoves = chkHighlightPossibleMoves.Checked;
        settings.ShowJumpMessage = chkShowJumpMessage.Checked;
        settings.ShowTextFeedback = chkShowTextFeedback.Checked;
        // Net settings
        settings.FlashWindowOnGameEvents = chkFlashWindowOnGameEvents.Checked;
        settings.FlashWindowOnTurn = chkFlashWindowOnTurn.Checked;
        settings.FlashWindowOnMessage = chkFlashWindowOnMessage.Checked;
        settings.ShowNetPanelOnMessage = chkShowNetPanelOnMessage.Checked;
        // Board appearance
        settings.BackColor = picBackColor.BackColor;
        settings.BoardBackColor = picBoardBackColor.BackColor;
        settings.BoardForeColor = picBoardForeColor.BackColor;
        settings.BoardGridColor = picBoardGridColor.BackColor;
        // Sounds
        settings.sounds = sounds;
        settings.MuteSounds = chkMuteSounds.Checked;
        // Save the settings
        settings.Save();
      }
      return result;
    }
    
    private void ShowSettings ()
    {
      // General settings
      chkHighlightSelection.Checked = settings.HighlightSelection;
      chkHighlightPossibleMoves.Checked = settings.HighlightPossibleMoves;
      chkShowJumpMessage.Checked = settings.ShowJumpMessage;
      chkShowTextFeedback.Checked = settings.ShowTextFeedback;
      // Net settings
      chkFlashWindowOnGameEvents.Checked = settings.FlashWindowOnGameEvents;
      chkFlashWindowOnTurn.Checked = settings.FlashWindowOnTurn;
      chkFlashWindowOnMessage.Checked = settings.FlashWindowOnMessage;
      chkShowNetPanelOnMessage.Checked = settings.ShowNetPanelOnMessage;
      // Board appearance
      picBackColor.BackColor = settings.BackColor;
      picBoardBackColor.BackColor = settings.BoardBackColor;
      picBoardForeColor.BackColor = settings.BoardForeColor;
      picBoardGridColor.BackColor = settings.BoardGridColor;
      // Sounds
      sounds = (string [])settings.sounds.Clone();
      lstSounds.Items.Clear();
      foreach (FieldInfo field in typeof(CheckersSounds).GetFields())
      {
        if ((!field.IsPublic) || (field.IsSpecialName)) continue;
        lstSounds.Items.Add(field.Name);
      }
      chkMuteSounds.Checked = settings.MuteSounds;
      if (lstSounds.Items.Count > 0) lstSounds.SelectedIndex = 0;
    }
    
    private void PlaySound (string soundFileName)
    {
      string fileName = (( Path.IsPathRooted(soundFileName) )?( soundFileName ):( Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds\\" + soundFileName ));
      // Play sound
      sndPlaySound(fileName, IntPtr.Zero, (SoundFlags.SND_FileName | SoundFlags.SND_ASYNC | SoundFlags.SND_NOWAIT));
    }
    
    private void frmPreferences_Load (object sender, System.EventArgs e)
    {}
    
    private void btnDefault_Click (object sender, System.EventArgs e)
    {
      if (MessageBox.Show(this, "All settings will be lost. Reset to default settings?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
      settings = new CheckersSettings();
      ShowSettings();
    }
    
    private void picColor_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left) return;
      dlgColorDialog.Color = ((PictureBox)sender).BackColor;
      if (dlgColorDialog.ShowDialog(this) == DialogResult.Cancel) return;
      ((PictureBox)sender).BackColor = dlgColorDialog.Color;
    }
    
    private void menuChangeColor_Click (object sender, System.EventArgs e)
    {
      PictureBox pic = (PictureBox)((MenuItem)sender).GetContextMenu().SourceControl;
      dlgColorDialog.Color = pic.BackColor;
      if (dlgColorDialog.ShowDialog(this) == DialogResult.Cancel) return;
      pic.BackColor = dlgColorDialog.Color;
    }
    private void menuSetDefault_Click (object sender, System.EventArgs e)
    {
      PictureBox pic = (PictureBox)((MenuItem)sender).GetContextMenu().SourceControl;
      if (pic == picBackColor) pic.BackColor = (new CheckersSettings().BackColor);
      else if (pic == picBoardBackColor) pic.BackColor = (new CheckersSettings().BoardBackColor);
      else if (pic == picBoardForeColor) pic.BackColor = (new CheckersSettings().BoardForeColor);
      else if (pic == picBoardGridColor) pic.BackColor = (new CheckersSettings().BoardGridColor);
    }
    
    private void lstSounds_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedItem == null) return;
      txtSoundFile.Text = sounds[(int)typeof(CheckersSounds).GetField((string)lstSounds.SelectedItem).GetValue(null)];
      //txtSoundFile.Text = (string)sounds.GetType().GetField((string)lstSounds.SelectedItem).GetValue(sounds);
    }
    private void txtSoundFile_TextChanged (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedIndex == -1) return;
      sounds[(int)typeof(CheckersSounds).GetField((string)lstSounds.SelectedItem).GetValue(null)] = txtSoundFile.Text;
      //sounds.GetType().GetField((string)lstSounds.SelectedItem).SetValue(sounds, txtSoundFile.Text);
    }
    private void btnSoundPreview_Click (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedIndex == -1) return;
      PlaySound(txtSoundFile.Text);
    }
    
    private void btnSoundFile_Click (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedIndex == -1) return;
      string soundsPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds";
      string fileName = (( Path.IsPathRooted(txtSoundFile.Text) )?( txtSoundFile.Text ):( soundsPath + "\\" + txtSoundFile.Text ));
      
      if (File.Exists(fileName)) dlgOpenSound.FileName = fileName;
      else dlgOpenSound.InitialDirectory = Path.GetDirectoryName(fileName);
      // Show the dialog
      if (dlgOpenSound.ShowDialog(this) == DialogResult.Cancel) return;
      // Get the sound file
      string newFileName = dlgOpenSound.FileName;
      string common = Path.GetDirectoryName(newFileName);
      if (common.ToLower() == soundsPath.ToLower())
        newFileName = newFileName.Substring(common.Length+1);
      txtSoundFile.Text = newFileName;
    }
  }
}
