using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TheLeftExit.TeslaX.Static
{
    internal static class App
    {
        private static ToolStripStatusLabel statusLabel;

        public static ToolStripStatusLabel StatusLabel { set { statusLabel = value; } }

        public static string Status
        {
            set
            {
                if (Application.OpenForms["NewMainForm"] != null)
                    statusLabel.GetCurrentParent().Invoke((MethodInvoker)delegate
                    {
                        statusLabel.Text = value;
                    });
            }
        }
    }
}
