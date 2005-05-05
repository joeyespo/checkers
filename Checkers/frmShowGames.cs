using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Checkers
{
  /// <summary>
  /// Summary description for frmConnectTo.
  /// </summary>
  public class frmShowGames : System.Windows.Forms.Form
  {
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ListBox lstRecentGames;
    private System.Windows.Forms.Label lblRecentGames;
    private System.Windows.Forms.TextBox txtRemoteHost;
    private System.Windows.Forms.Label lblRemoteHost;
    
    #region Class Variables
    
    /// <summary> Required designer variable. </summary>
    private System.ComponentModel.Container components = null;
    
    #endregion
    
    #region Class Construction
    
    public frmShowGames()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }

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

    #region Windows Form Designer generated code
    
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lstRecentGames = new System.Windows.Forms.ListBox();
      this.lblRecentGames = new System.Windows.Forms.Label();
      this.txtRemoteHost = new System.Windows.Forms.TextBox();
      this.lblRemoteHost = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnOK.Location = new System.Drawing.Point(272, 216);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(76, 32);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "&OK";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(356, 216);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(76, 32);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "&Cancel";
      // 
      // lstRecentGames
      // 
      this.lstRecentGames.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lstRecentGames.IntegralHeight = false;
      this.lstRecentGames.Location = new System.Drawing.Point(8, 24);
      this.lstRecentGames.Name = "lstRecentGames";
      this.lstRecentGames.Size = new System.Drawing.Size(424, 140);
      this.lstRecentGames.TabIndex = 1;
      // 
      // lblRecentGames
      // 
      this.lblRecentGames.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblRecentGames.Location = new System.Drawing.Point(8, 8);
      this.lblRecentGames.Name = "lblRecentGames";
      this.lblRecentGames.Size = new System.Drawing.Size(424, 16);
      this.lblRecentGames.TabIndex = 2;
      this.lblRecentGames.Text = "Recent Games:";
      // 
      // txtRemoteHost
      // 
      this.txtRemoteHost.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.txtRemoteHost.Location = new System.Drawing.Point(8, 188);
      this.txtRemoteHost.Name = "txtRemoteHost";
      this.txtRemoteHost.Size = new System.Drawing.Size(424, 20);
      this.txtRemoteHost.TabIndex = 3;
      this.txtRemoteHost.Text = "";
      // 
      // lblRemoteHost
      // 
      this.lblRemoteHost.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblRemoteHost.Location = new System.Drawing.Point(8, 172);
      this.lblRemoteHost.Name = "lblRemoteHost";
      this.lblRemoteHost.Size = new System.Drawing.Size(424, 16);
      this.lblRemoteHost.TabIndex = 4;
      this.lblRemoteHost.Text = "Remote Host:";
      // 
      // frmShowGames
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(438, 255);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.lblRemoteHost,
                                                                  this.txtRemoteHost,
                                                                  this.lblRecentGames,
                                                                  this.lstRecentGames,
                                                                  this.btnOK,
                                                                  this.btnCancel});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmShowGames";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Show Games";
      this.ResumeLayout(false);

    }
    
    #endregion
    
    #endregion
    
  }
}
