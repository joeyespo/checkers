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
    /// <summary>
    /// Represents the "Preferences" dialog.
    /// </summary>
    public sealed partial class PreferencesDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreferencesDialog"/> class.
        /// </summary>
        public PreferencesDialog()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the settings to show and modify.
        /// </summary>
        public CheckersSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
            }
        }

        #endregion

        #region Event Handler Methods

        /// <summary>
        /// Handles the SelectedIndexChanged event of the tabPreferences control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tabPreferences_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(tabPreferences.SelectedIndex == -1)
                return;
            lblTitle.Text = tabPreferences.SelectedTab.Text;
        }

        /// <summary>
        /// Handles the Click event of the btnDefault control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnDefault_Click(object sender, System.EventArgs e)
        {
            if(MessageBox.Show(this, "All settings will be lost. Reset to default settings?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            settings = new CheckersSettings();
            ShowSettings();
        }

        /// <summary>
        /// Handles the MouseDown event of the picColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void picColor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                return;
            dlgColorDialog.Color = ((PictureBox)sender).BackColor;
            if(dlgColorDialog.ShowDialog(this) == DialogResult.Cancel)
                return;
            ((PictureBox)sender).BackColor = dlgColorDialog.Color;
        }

        /// <summary>
        /// Handles the Click event of the menuChangeColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuChangeColor_Click(object sender, System.EventArgs e)
        {
            PictureBox pic = (PictureBox)((MenuItem)sender).GetContextMenu().SourceControl;
            dlgColorDialog.Color = pic.BackColor;
            if(dlgColorDialog.ShowDialog(this) == DialogResult.Cancel)
                return;
            pic.BackColor = dlgColorDialog.Color;
        }

        /// <summary>
        /// Handles the Click event of the menuSetDefault control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuSetDefault_Click(object sender, System.EventArgs e)
        {
            PictureBox pic = (PictureBox)((MenuItem)sender).GetContextMenu().SourceControl;
            if(pic == picBackColor)
                pic.BackColor = (new CheckersSettings().BackColor);
            else if(pic == picBoardBackColor)
                pic.BackColor = (new CheckersSettings().BoardBackColor);
            else if(pic == picBoardForeColor)
                pic.BackColor = (new CheckersSettings().BoardForeColor);
            else if(pic == picBoardGridColor)
                pic.BackColor = (new CheckersSettings().BoardGridColor);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstSounds control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lstSounds_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(lstSounds.SelectedItem == null)
                return;
            txtSoundFile.Text = sounds[(int)typeof(CheckersSounds).GetField((string)lstSounds.SelectedItem).GetValue(null)];
            //txtSoundFile.Text = (string)sounds.GetType().GetField((string)lstSounds.SelectedItem).GetValue(sounds);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtSoundFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtSoundFile_TextChanged(object sender, System.EventArgs e)
        {
            if(lstSounds.SelectedIndex == -1)
                return;
            sounds[(int)typeof(CheckersSounds).GetField((string)lstSounds.SelectedItem).GetValue(null)] = txtSoundFile.Text;
            //sounds.GetType().GetField((string)lstSounds.SelectedItem).SetValue(sounds, txtSoundFile.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnSoundPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSoundPreview_Click(object sender, System.EventArgs e)
        {
            if(lstSounds.SelectedIndex == -1)
                return;
            PlaySound(txtSoundFile.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnSoundFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSoundFile_Click(object sender, System.EventArgs e)
        {
            if(lstSounds.SelectedIndex == -1)
                return;
            string soundsPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds";
            string fileName = ((Path.IsPathRooted(txtSoundFile.Text)) ? (txtSoundFile.Text) : (soundsPath + "\\" + txtSoundFile.Text));

            if(File.Exists(fileName))
                dlgOpenSound.FileName = fileName;
            else
                dlgOpenSound.InitialDirectory = Path.GetDirectoryName(fileName);
            // Show the dialog
            if(dlgOpenSound.ShowDialog(this) == DialogResult.Cancel)
                return;
            // Get the sound file
            string newFileName = dlgOpenSound.FileName;
            string common = Path.GetDirectoryName(newFileName);
            if(common.ToLower() == soundsPath.ToLower())
                newFileName = newFileName.Substring(common.Length + 1);
            txtSoundFile.Text = newFileName;
        }

        #endregion

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="IWin32Window"/> that represents the top-level window that will own the modal dialog box.</param>
        /// <returns>One of the <see cref="DialogResult"/> values.</returns>
        /// <exception cref="ArgumentException">The form specified in the <paramref name="owner"/> parameter is the same as the form being shown.</exception>
        /// <exception cref="InvalidOperationException">The form being shown is already visible.-or-The form being shown is disabled.-or-The form being shown is not a top-level window.-or-The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive"/>).</exception>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            return OnShowDialog(owner);
        }

        #region Helper Methods

        /// <summary>
        /// Called when the dialog is shown.
        /// </summary>
        public new DialogResult OnShowDialog(IWin32Window owner)
        {
            if(settings == null)
                settings = new CheckersSettings();

            // Show settings
            ShowSettings();

            // Show dialog
            DialogResult result = base.ShowDialog(owner);

            // Set properties
            if(result != DialogResult.Cancel)
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

        /// <summary>
        /// Shows the settings.
        /// </summary>
        private void ShowSettings()
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
            sounds = (string[])settings.sounds.Clone();
            lstSounds.Items.Clear();
            foreach(FieldInfo field in typeof(CheckersSounds).GetFields())
            {
                if((!field.IsPublic) || (field.IsSpecialName))
                    continue;
                lstSounds.Items.Add(field.Name);
            }
            chkMuteSounds.Checked = settings.MuteSounds;
            if(lstSounds.Items.Count > 0)
                lstSounds.SelectedIndex = 0;
        }

        /// <summary>
        /// Plays a sound from the specified file.
        /// </summary>
        private void PlaySound(string soundFileName)
        {
            string fileName = ((Path.IsPathRooted(soundFileName)) ? (soundFileName) : (Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds\\" + soundFileName));
            // Play sound
            sndPlaySound(fileName, IntPtr.Zero, (SoundFlags.SND_FileName | SoundFlags.SND_ASYNC | SoundFlags.SND_NOWAIT));
        }

        #endregion

        #region Win32 Members

        [DllImport("winmm.dll", EntryPoint = "PlaySound", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        static extern bool sndPlaySound(string pszSound, IntPtr hMod, SoundFlags sf);

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

        private CheckersSettings settings;
        private string[] sounds;
    }
}
