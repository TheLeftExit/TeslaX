using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TheLeftExit.TeslaX.Properties;

namespace TheLeftExit.TeslaX.Static
{
    internal static class App
    {
        // Find all integers between /a/ and /b/ where (x => x % 32 == off). Subtract /d/ from each.
        public static List<int> EligibleBetween(int a, int b, int off, int d = 0)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off + (a % 32 < off ? 0 : 32);
            for (int i = start; i <= b; i += 32)
                result.Add(i - d);
            return result;
        }

        public static int Limit(this int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

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
