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

        public static HwndObject Window;

        public static bool Windowed;

        // Relative to screen.
        public static Rectangle WindowPos;

        // Relative to window.
        public static Rectangle SeekArea;

        // Relative to window.
        public static Point Offset;

        // Relative to window.
        public static Point LastKnown;

        public static bool Right;
        public static bool Down;

        public static Bitmap CurrentShot;

        public static Bitmap Screenshot(int x, int y, int w, int l)
        {
            Bitmap res = new Bitmap(w, l);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.CopyFromScreen(x, y, 0, 0, res.Size);
            }
            return res;
        }

        public static void Screenshot()
        {
            CurrentShot = Screenshot(SeekArea.X + WindowPos.X, SeekArea.Y + WindowPos.Y, SeekArea.Width, SeekArea.Height);
        }

        // Normalizes offset from a screenshot.
        public static Point ify(this Point point)
        {
            return point.Add(SeekArea.X, SeekArea.Y).Mod(32);
        }

        // Returns list of integer locations eligible for given offset. Implies 32 as mod.
        public static List<int> EligibleBetween(int a, int b, int off)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off;
            for (int i = start > a ? start : start + 32; i <= b; i += 32)
                result.Add(i);
            return result;
        }

        // Adds a number to each element of array.
        public static List<int> AddInt(this List<int> list, int a)
        {
            List<int> res = new List<int>(list.Count);
            foreach (int x in list)
                res.Add(x + a);
            return res;
        }

        // Dirty form management.

        public static void Log(string s)
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.Text = s;
            });
        }

        public static void ToWorking()
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.button1.Text = "Working";
            });
        }


        public static void Restore()
        {
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.Restore();
            });
        }

    }
}
