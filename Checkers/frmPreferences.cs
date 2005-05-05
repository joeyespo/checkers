using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Checkers
{
  /// <summary>
  /// Summary description for frmPreferences.
  /// </summary>
  public class frmPreferences : System.Windows.Forms.Form
  {
    private Pabo.MozBar.MozPane mozPreferences;
    
    #region Class Variables
    
    /// <summary> Required designer variable. </summary>
    private System.ComponentModel.Container components = null;
    
    #endregion
    
    #region Class Construction
    
    public frmPreferences()
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
      this.mozPreferences = new Pabo.MozBar.MozPane();
      ((System.ComponentModel.ISupportInitialize)(this.mozPreferences)).BeginInit();
      this.SuspendLayout();
      // 
      // mozPreferences
      // 
      this.mozPreferences.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left);
      this.mozPreferences.BackColor = System.Drawing.Color.White;
      this.mozPreferences.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(127)), ((System.Byte)(157)), ((System.Byte)(185)));
      this.mozPreferences.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozPreferences.ImageList = null;
      this.mozPreferences.ItemBorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozPreferences.ItemBorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.None;
      this.mozPreferences.ItemBorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozPreferences.ItemColors.Background = System.Drawing.Color.White;
      this.mozPreferences.ItemColors.Border = System.Drawing.Color.Black;
      this.mozPreferences.ItemColors.Divider = System.Drawing.Color.FromArgb(((System.Byte)(127)), ((System.Byte)(157)), ((System.Byte)(185)));
      this.mozPreferences.ItemColors.FocusBackground = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(232)), ((System.Byte)(246)));
      this.mozPreferences.ItemColors.FocusBorder = System.Drawing.Color.FromArgb(((System.Byte)(152)), ((System.Byte)(180)), ((System.Byte)(226)));
      this.mozPreferences.ItemColors.SelectedBackground = System.Drawing.Color.FromArgb(((System.Byte)(193)), ((System.Byte)(210)), ((System.Byte)(238)));
      this.mozPreferences.ItemColors.SelectedBorder = System.Drawing.Color.FromArgb(((System.Byte)(49)), ((System.Byte)(106)), ((System.Byte)(197)));
      this.mozPreferences.ItemColors.Text = System.Drawing.Color.Black;
      this.mozPreferences.Location = new System.Drawing.Point(4, 4);
      this.mozPreferences.MaxSelectedItems = 1;
      this.mozPreferences.Name = "mozPreferences";
      this.mozPreferences.Padding.Horizontal = 2;
      this.mozPreferences.Padding.Vertical = 2;
      this.mozPreferences.Size = new System.Drawing.Size(76, 328);
      this.mozPreferences.Style = Pabo.MozBar.paneStyle.Vertical;
      this.mozPreferences.TabIndex = 0;
      this.mozPreferences.Toggle = false;
      // 
      // frmPreferences
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(500, 337);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.mozPreferences});
      this.Name = "frmPreferences";
      this.Text = "frmPreferences";
      ((System.ComponentModel.ISupportInitialize)(this.mozPreferences)).EndInit();
      this.ResumeLayout(false);

    }
    
    #endregion
    
    #endregion
    
  }
}
