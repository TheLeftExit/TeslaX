using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public static partial class Worker
    {
        public static MainForm mainForm;

        public static bool Busy;

        public static readonly Point InvalidPoint = new Point(-1, -1);

        // Relative to window.
        public static Point Offset;

        // Relative to window.
        public static Point LastKnown;

        public static bool Right;
        public static bool Down;

        // Dirty form management.

        public static void Log(string s)
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.Log(s);
            });
        }

        public static void ToWorking()
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.Restore(1);
            });
        }

        public static void Restore()
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.Restore(0);
            });
        }

    }
}
