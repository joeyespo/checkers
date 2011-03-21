namespace Checkers
{
    partial class frmAbout
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
                    components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.lblVersion = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panAbout = new System.Windows.Forms.Panel();
            this.lnkWebLink = new System.Windows.Forms.LinkLabel();
            this.lblWebTitle = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.picDescription = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRevision = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panAbout.SuspendLayout();
            this.picDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(12, 220);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "Version:";
            this.lblVersion.Resize += new System.EventHandler(this.lblVersion_Resize);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(12, 8);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(32, 32);
            this.picLogo.TabIndex = 10;
            this.picLogo.TabStop = false;
            // 
            // panAbout
            // 
            this.panAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(89)))), ((int)(((byte)(117)))));
            this.panAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panAbout.BackgroundImage")));
            this.panAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panAbout.Controls.Add(this.lnkWebLink);
            this.panAbout.Controls.Add(this.lblWebTitle);
            this.panAbout.Controls.Add(this.lblAuthor);
            this.panAbout.Controls.Add(this.picDescription);
            this.panAbout.Location = new System.Drawing.Point(12, 48);
            this.panAbout.Name = "panAbout";
            this.panAbout.Size = new System.Drawing.Size(284, 160);
            this.panAbout.TabIndex = 2;
            // 
            // lnkWebLink
            // 
            this.lnkWebLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(228)))), ((int)(((byte)(164)))));
            this.lnkWebLink.AutoSize = true;
            this.lnkWebLink.BackColor = System.Drawing.Color.Transparent;
            this.lnkWebLink.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.lnkWebLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.484F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lnkWebLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(175)))), ((int)(((byte)(162)))));
            this.lnkWebLink.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lnkWebLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(175)))), ((int)(((byte)(162)))));
            this.lnkWebLink.Location = new System.Drawing.Point(184, 136);
            this.lnkWebLink.Name = "lnkWebLink";
            this.lnkWebLink.Size = new System.Drawing.Size(95, 15);
            this.lnkWebLink.TabIndex = 3;
            this.lnkWebLink.TabStop = true;
            this.lnkWebLink.Text = "joeyespo.com";
            this.lnkWebLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(175)))), ((int)(((byte)(162)))));
            this.lnkWebLink.MouseLeave += new System.EventHandler(this.lnkWebLink_MouseLeave);
            this.lnkWebLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebLink_LinkClicked);
            this.lnkWebLink.MouseEnter += new System.EventHandler(this.lnkWebLink_MouseEnter);
            // 
            // lblWebTitle
            // 
            this.lblWebTitle.AutoSize = true;
            this.lblWebTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblWebTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.484F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblWebTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(175)))), ((int)(((byte)(162)))));
            this.lblWebTitle.Location = new System.Drawing.Point(136, 118);
            this.lblWebTitle.Name = "lblWebTitle";
            this.lblWebTitle.Size = new System.Drawing.Size(145, 15);
            this.lblWebTitle.TabIndex = 2;
            this.lblWebTitle.Text = "Visit my site for more:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(175)))), ((int)(((byte)(162)))));
            this.lblAuthor.Location = new System.Drawing.Point(12, 80);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(264, 15);
            this.lblAuthor.TabIndex = 1;
            this.lblAuthor.Text = "Designed and Coded by Joe Esposito";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picDescription
            // 
            this.picDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(44)))), ((int)(((byte)(58)))));
            this.picDescription.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picDescription.BackgroundImage")));
            this.picDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDescription.Controls.Add(this.lblDescription);
            this.picDescription.Location = new System.Drawing.Point(8, 8);
            this.picDescription.Name = "picDescription";
            this.picDescription.Size = new System.Drawing.Size(268, 64);
            this.picDescription.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblDescription.Location = new System.Drawing.Point(4, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(256, 48);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "[ Description ]";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(48, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "[ Title ]";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(208, 220);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            // 
            // lblRevision
            // 
            this.lblRevision.AutoSize = true;
            this.lblRevision.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblRevision.Location = new System.Drawing.Point(68, 220);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(51, 13);
            this.lblRevision.TabIndex = 4;
            this.lblRevision.Text = "Revision:";
            // 
            // frmAbout
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(306, 264);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblRevision);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.panAbout);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panAbout.ResumeLayout(false);
            this.panAbout.PerformLayout();
            this.picDescription.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panAbout;
        private System.Windows.Forms.LinkLabel lnkWebLink;
        private System.Windows.Forms.Label lblWebTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Panel picDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRevision;
    }
}
