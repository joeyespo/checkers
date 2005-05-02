using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Checkers
{
  public class frmMain : System.Windows.Forms.Form
  {
    
    #region Class Variables
    
    private System.Windows.Forms.Panel panGame;
    private System.Windows.Forms.Label lblPlayer;
    private System.Windows.Forms.MainMenu menuMain;
    private System.Windows.Forms.Panel panOnline;
    private System.Windows.Forms.MenuItem menuGame;
    private System.Windows.Forms.MenuItem menuGameNew;
    private System.Windows.Forms.MenuItem menuGameLine01;
    private System.Windows.Forms.MenuItem menuGameExit;
    private System.Windows.Forms.MenuItem menuOptions;
    private System.Windows.Forms.MenuItem menuOptionsSkill;
    private System.Windows.Forms.MenuItem menuOptionsSkillBeginner;
    private System.Windows.Forms.MenuItem menuOptionsSkillIntermediate;
    private System.Windows.Forms.MenuItem menuOptionsSkillAdvanced;
    private System.Windows.Forms.MenuItem menuOptionsSkillExpert;
    private Uberware.Gaming.Checkers.UI.CheckersUI CheckersUI;
    private System.Windows.Forms.MenuItem menuOptionsSettings;
    private System.Windows.Forms.MenuItem menuHelp;
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
      this.panGame = new System.Windows.Forms.Panel();
      this.lblPlayer = new System.Windows.Forms.Label();
      this.menuMain = new System.Windows.Forms.MainMenu();
      this.menuGame = new System.Windows.Forms.MenuItem();
      this.menuGameNew = new System.Windows.Forms.MenuItem();
      this.menuGameLine01 = new System.Windows.Forms.MenuItem();
      this.menuGameExit = new System.Windows.Forms.MenuItem();
      this.menuOptions = new System.Windows.Forms.MenuItem();
      this.menuOptionsSkill = new System.Windows.Forms.MenuItem();
      this.menuOptionsSkillBeginner = new System.Windows.Forms.MenuItem();
      this.menuOptionsSkillIntermediate = new System.Windows.Forms.MenuItem();
      this.menuOptionsSkillAdvanced = new System.Windows.Forms.MenuItem();
      this.menuOptionsSkillExpert = new System.Windows.Forms.MenuItem();
      this.menuOptionsSettings = new System.Windows.Forms.MenuItem();
      this.menuHelp = new System.Windows.Forms.MenuItem();
      this.menuHelpAbout = new System.Windows.Forms.MenuItem();
      this.panOnline = new System.Windows.Forms.Panel();
      this.CheckersUI = new Uberware.Gaming.Checkers.UI.CheckersUI();
      this.panGame.SuspendLayout();
      this.SuspendLayout();
      // 
      // panGame
      // 
      this.panGame.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.panGame.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.lblPlayer});
      this.panGame.Location = new System.Drawing.Point(280, 4);
      this.panGame.Name = "panGame";
      this.panGame.Size = new System.Drawing.Size(108, 272);
      this.panGame.TabIndex = 3;
      // 
      // lblPlayer
      // 
      this.lblPlayer.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblPlayer.Name = "lblPlayer";
      this.lblPlayer.Size = new System.Drawing.Size(108, 16);
      this.lblPlayer.TabIndex = 0;
      this.lblPlayer.Text = "Player Info:";
      // 
      // menuMain
      // 
      this.menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuGame,
                                                                             this.menuOptions,
                                                                             this.menuHelp});
      // 
      // menuGame
      // 
      this.menuGame.Index = 0;
      this.menuGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuGameNew,
                                                                             this.menuGameLine01,
                                                                             this.menuGameExit});
      this.menuGame.Text = "&Game";
      // 
      // menuGameNew
      // 
      this.menuGameNew.Index = 0;
      this.menuGameNew.Shortcut = System.Windows.Forms.Shortcut.F2;
      this.menuGameNew.Text = "&New Game";
      this.menuGameNew.Click += new System.EventHandler(this.menuGameNew_Click);
      // 
      // menuGameLine01
      // 
      this.menuGameLine01.Index = 1;
      this.menuGameLine01.Text = "-";
      // 
      // menuGameExit
      // 
      this.menuGameExit.Index = 2;
      this.menuGameExit.Text = "E&xit";
      this.menuGameExit.Click += new System.EventHandler(this.menuGameExit_Click);
      // 
      // menuOptions
      // 
      this.menuOptions.Index = 1;
      this.menuOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                this.menuOptionsSkill,
                                                                                this.menuOptionsSettings});
      this.menuOptions.Text = "&Options";
      // 
      // menuOptionsSkill
      // 
      this.menuOptionsSkill.Index = 0;
      this.menuOptionsSkill.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuOptionsSkillBeginner,
                                                                                     this.menuOptionsSkillIntermediate,
                                                                                     this.menuOptionsSkillAdvanced,
                                                                                     this.menuOptionsSkillExpert});
      this.menuOptionsSkill.Text = "&Skill Level";
      // 
      // menuOptionsSkillBeginner
      // 
      this.menuOptionsSkillBeginner.Checked = true;
      this.menuOptionsSkillBeginner.Index = 0;
      this.menuOptionsSkillBeginner.Text = "&Beginner";
      // 
      // menuOptionsSkillIntermediate
      // 
      this.menuOptionsSkillIntermediate.Index = 1;
      this.menuOptionsSkillIntermediate.Text = "&Intermediate";
      // 
      // menuOptionsSkillAdvanced
      // 
      this.menuOptionsSkillAdvanced.Index = 2;
      this.menuOptionsSkillAdvanced.Text = "&Advanced";
      // 
      // menuOptionsSkillExpert
      // 
      this.menuOptionsSkillExpert.Enabled = false;
      this.menuOptionsSkillExpert.Index = 3;
      this.menuOptionsSkillExpert.Text = "&Expert";
      // 
      // menuOptionsSettings
      // 
      this.menuOptionsSettings.Index = 1;
      this.menuOptionsSettings.Text = "&Settings...";
      this.menuOptionsSettings.Click += new System.EventHandler(this.menuOptionsSettings_Click);
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
      this.panOnline.Location = new System.Drawing.Point(4, 280);
      this.panOnline.Name = "panOnline";
      this.panOnline.Size = new System.Drawing.Size(384, 68);
      this.panOnline.TabIndex = 4;
      // 
      // CheckersUI
      // 
      this.CheckersUI.Location = new System.Drawing.Point(4, 4);
      this.CheckersUI.Name = "CheckersUI";
      this.CheckersUI.Size = new System.Drawing.Size(270, 270);
      this.CheckersUI.TabIndex = 5;
      this.CheckersUI.UsePieceImages = false;
      // 
      // frmMain
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(392, 353);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.CheckersUI,
                                                                  this.panOnline,
                                                                  this.panGame});
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Menu = this.menuMain;
      this.MinimumSize = new System.Drawing.Size(288, 324);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Checkers";
      this.panGame.ResumeLayout(false);
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
    
    
    private void menuGameNew_Click(object sender, System.EventArgs e)
    { DoNewGame(); }
    private void menuGameExit_Click (object sender, System.EventArgs e)
    { Close(); }
    
    private void menuOptionsSettings_Click (object sender, System.EventArgs e)
    { MessageBox.Show(this, "!!!!! Settings !!!!!"); }
    
    private void menuHelpAbout_Click (object sender, System.EventArgs e)
    { (new frmAbout()).ShowDialog(this); }
    
    
    private void DoNewGame ()
    {
      if (CheckersUI.IsPlaying)
      {
        // !!!!! Ask for 'stalemate' in multiplayer mode
        if (MessageBox.Show(this, "Quit current game?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        // Stop current game (with no winner)
        CheckersUI.Stop();
      }
      // Start a new checkers game
      CheckersUI.Play();
      // !!!!!
    }
    
  }
}
