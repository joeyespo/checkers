using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Uberware.Gaming.Checkers;
using Uberware.Gaming.Checkers.UI;

namespace Checkers
{
  public class frmMain : System.Windows.Forms.Form
  {
    
    #region Class Variables
    
    private System.Windows.Forms.Panel panGame;
    private System.Windows.Forms.MainMenu menuMain;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem menuItem4;
    private System.Windows.Forms.MenuItem menuItem5;
    private System.Windows.Forms.MenuItem menuItem6;
    private System.Windows.Forms.MenuItem menuItem7;
    private System.Windows.Forms.MenuItem menuItem8;
    private System.Windows.Forms.MenuItem menuItem9;
    private System.Windows.Forms.MenuItem menuItem10;
    private System.Windows.Forms.MenuItem menuItem11;
    private System.Windows.Forms.MenuItem menuItem12;
    private System.Windows.Forms.MenuItem menuItem15;
    private System.Windows.Forms.Label lblPlayer;
    private System.Windows.Forms.Panel panOnline;
    private Uberware.Gaming.Checkers.UI.CheckersUI checkersUI;
    
    /// <summary> Required designer variable. </summary>
    private System.ComponentModel.Container components = null;
    
    #endregion
    
    #region Class Construction
    
    public frmMain()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }
    
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null) 
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
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
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.menuItem9 = new System.Windows.Forms.MenuItem();
      this.menuItem10 = new System.Windows.Forms.MenuItem();
      this.menuItem11 = new System.Windows.Forms.MenuItem();
      this.menuItem15 = new System.Windows.Forms.MenuItem();
      this.menuItem12 = new System.Windows.Forms.MenuItem();
      this.menuItem8 = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.menuItem7 = new System.Windows.Forms.MenuItem();
      this.checkersUI = new Uberware.Gaming.Checkers.UI.CheckersUI();
      this.panOnline = new System.Windows.Forms.Panel();
      this.panGame.SuspendLayout();
      this.SuspendLayout();
      // 
      // panGame
      // 
      this.panGame.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.lblPlayer});
      this.panGame.Dock = System.Windows.Forms.DockStyle.Right;
      this.panGame.Location = new System.Drawing.Point(286, 0);
      this.panGame.Name = "panGame";
      this.panGame.Size = new System.Drawing.Size(144, 287);
      this.panGame.TabIndex = 0;
      // 
      // lblPlayer
      // 
      this.lblPlayer.Location = new System.Drawing.Point(8, 8);
      this.lblPlayer.Name = "lblPlayer";
      this.lblPlayer.Size = new System.Drawing.Size(132, 16);
      this.lblPlayer.TabIndex = 0;
      this.lblPlayer.Text = "Player Info:";
      // 
      // menuMain
      // 
      this.menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuItem1,
                                                                             this.menuItem3,
                                                                             this.menuItem6});
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 0;
      this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem2,
                                                                              this.menuItem4,
                                                                              this.menuItem5});
      this.menuItem1.Text = "&Game";
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 0;
      this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.F2;
      this.menuItem2.Text = "&New Game";
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 1;
      this.menuItem4.Text = "-";
      // 
      // menuItem5
      // 
      this.menuItem5.Index = 2;
      this.menuItem5.Text = "E&xit";
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 1;
      this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem9,
                                                                              this.menuItem8});
      this.menuItem3.Text = "&Options";
      // 
      // menuItem9
      // 
      this.menuItem9.Index = 0;
      this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem10,
                                                                              this.menuItem11,
                                                                              this.menuItem15,
                                                                              this.menuItem12});
      this.menuItem9.Text = "&Skill Level";
      // 
      // menuItem10
      // 
      this.menuItem10.Checked = true;
      this.menuItem10.Index = 0;
      this.menuItem10.Text = "&Beginner";
      // 
      // menuItem11
      // 
      this.menuItem11.Index = 1;
      this.menuItem11.Text = "&Intermediate";
      // 
      // menuItem15
      // 
      this.menuItem15.Index = 2;
      this.menuItem15.Text = "&Advanced";
      // 
      // menuItem12
      // 
      this.menuItem12.Enabled = false;
      this.menuItem12.Index = 3;
      this.menuItem12.Text = "&Expert";
      // 
      // menuItem8
      // 
      this.menuItem8.Index = 1;
      this.menuItem8.Text = "&Settings...";
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 2;
      this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem7});
      this.menuItem6.Text = "&Help";
      // 
      // menuItem7
      // 
      this.menuItem7.Index = 0;
      this.menuItem7.Text = "&About...";
      // 
      // checkersUI
      // 
      this.checkersUI.Location = new System.Drawing.Point(8, 8);
      this.checkersUI.Name = "checkersUI";
      this.checkersUI.Size = new System.Drawing.Size(268, 268);
      this.checkersUI.TabIndex = 1;
      // 
      // panOnline
      // 
      this.panOnline.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panOnline.Location = new System.Drawing.Point(0, 287);
      this.panOnline.Name = "panOnline";
      this.panOnline.Size = new System.Drawing.Size(430, 72);
      this.panOnline.TabIndex = 2;
      // 
      // frmMain
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(430, 359);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.panGame,
                                                                  this.panOnline,
                                                                  this.checkersUI});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Menu = this.menuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Checkers";
      this.Load += new System.EventHandler(this.frmMain_Load);
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
    
    private void frmMain_Load(object sender, System.EventArgs e)
    {
      CheckersGame game = new CheckersGame();
    }
    
  }
}
