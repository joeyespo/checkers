using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Checkers
{
    /// <summary>
    /// Represents the about dialog.
    /// </summary>
    public sealed partial class frmAbout : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutDialog"/> class.
        /// </summary>
        public frmAbout()
        {
            InitializeComponent();

            lblTitle.Text = AppTitle;
            lblDescription.Text = AppDescription;
        }

        #region Event Handler Methods

        /// <summary>
        /// Handles the Load event of the frmAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmAbout_Load(object sender, System.EventArgs e)
        {
            string m_strProductVersion;
            string m_strVersion;
            string m_strRevision;
            int i;

            m_strProductVersion = System.Windows.Forms.Application.ProductVersion;

            i = m_strProductVersion.IndexOf(".");
            if(i >= 0)
                i = m_strProductVersion.IndexOf(".", (i + 1));
            m_strVersion = ((i >= 0) ? (m_strProductVersion.Substring(0, i)) : ("0"));

            if(i >= 0)
                i = m_strProductVersion.IndexOf(".", (i + 1));
            m_strRevision = ((i >= 0) ? (m_strProductVersion.Substring(i + 1)) : ("0"));

            lblVersion.Text = "Version: " + m_strVersion;
            lblRevision.Text = "[Revision: " + m_strRevision + "]";
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkWebLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lnkWebLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Call the Process.Start method to open the default browser with a URL:
                System.Diagnostics.Process.Start("http://joeyespo.com");
            }
            catch(Win32Exception)
            {
            }
            catch
            {
                // Failsafe
                MessageBox.Show(this, "Could not start browser process.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the lnkWebLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lnkWebLink_MouseEnter(object sender, System.EventArgs e)
        {
            lnkWebLink.LinkColor = lnkWebLink.ActiveLinkColor;
        }

        /// <summary>
        /// Handles the MouseLeave event of the lnkWebLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lnkWebLink_MouseLeave(object sender, System.EventArgs e)
        {
            lnkWebLink.LinkColor = lnkWebLink.ForeColor;
        }

        /// <summary>
        /// Handles the Resize event of the lblVersion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lblVersion_Resize(object sender, System.EventArgs e)
        {
            lblRevision.Left = lblVersion.Left + lblVersion.Width;
        }

        #endregion

        private const string AppTitle = "Checkers";
        private const string AppDescription = "Play checkers against a computer or another player, locally or remotely over a network.";

        // >> Icon is located on the window
    }
}
