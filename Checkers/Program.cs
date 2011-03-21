using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Checkers
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);
            using(MainForm form = new MainForm())
                Application.Run(form);
        }

        /// <summary>
        /// Catches any unhandled exceptions on the main thread and logs them.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Threading.ThreadExceptionEventArgs"/> instance containing the event data.</param>
        private static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                int ext = 1;
                string filename;

                while(true)
                {
                    filename = "ErrorLog [" + DateTime.Now.ToShortDateString() + "]";
                    filename = filename.Replace('/', '-').Replace('\\', '-');
                    filename += ext != 1
                        ? " (" + ext + ")"
                        : string.Empty;
                    if(!File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + filename + ".log"))
                        break;
                    ++ext;
                }

                filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + filename + ".log";
                using(StreamWriter fs = File.CreateText(filename))
                {
                    fs.WriteLine(Application.ProductName + " " + Application.ProductVersion);
                    fs.WriteLine(DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString());
                    fs.WriteLine();
                    fs.WriteLine(e.ToString());
                }
            }
            finally
            {
                // Rethrow the exception, crashing the application
                throw e.Exception;
            }
        }
    }
}
