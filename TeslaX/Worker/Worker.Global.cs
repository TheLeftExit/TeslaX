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
        /// <summary>
        /// Predicate output in case of error.
        /// </summary>
        public static readonly Point InvalidPoint = new Point(-1, -1);

        /// <summary>
        /// Stores Growtopia window handle.
        /// </summary>
        public static HwndObject Window;

        /// <summary>
        /// Determines whether Growtopia is windowed. Affects window offset.
        /// </summary>
        public static bool Windowed;

        /// <summary>
        /// Window location and size, relative to SCREEN.
        /// </summary>
        public static Rectangle WindowPos;

        /// <summary>
        /// Starting seeking area, relative to WINDOW.
        /// </summary>
        public static Rectangle SeekArea;

        /// <summary>
        /// Blocks' topleft pixel offset, relative to WINDOW.
        /// </summary>
        public static Point Offset;

        /// <summary>
        /// Player's last known location, relative to WINDOW.
        /// </summary>
        public static Point LastKnown;

        /// <summary>
        /// Farming direction.
        /// </summary>
        public static bool Right;
        public static bool Down;

        /// <summary>
        /// Returns a Bitmap of part of the screen. Resource costly.
        /// </summary>
        public static Bitmap Screenshot(int x, int y, int w, int l)
        {
            Bitmap res = new Bitmap(w, l);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.CopyFromScreen(x, y, 0, 0, res.Size);
            }
            return res;
        }

        /// <summary>
        /// Checks if a point of Bitmap matches a color.
        /// Accounts for possible color distortion.
        /// </summary>
        public static bool IsColorAt(this Color color, Point point, Bitmap bitmap)
        {
            Color source = bitmap.GetPixel(point.X, point.Y);
            if (Math.Abs(color.R - source.R) > 2)
                return false;
            if (Math.Abs(color.G - source.G) > 2)
                return false;
            if (Math.Abs(color.B - source.B) > 2)
                return false;
            return true;
        }

        /// <summary>
        /// Checks if a point of Bitmap matches a color.
        /// </summary>
        public static bool IsColorAt(this Color color, int x, int y, Bitmap bitmap)
        {
            return color.IsColorAt(new Point(x, y), bitmap);
        }

        public static Point Add(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }

        public static Point Mod(this Point point, int mod)
        {
            return new Point(point.X % mod, point.Y % mod);
        }

        /// <summary>
        /// Returns list of integer locations eligible for given offset. Implies 32 as mod.
        /// </summary>
        public static List<int> EligibleBetween(int a, int b, int off)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off;
            for (int i = start > a ? start : start + 32; i <= b; i += 32)
                result.Add(i);
            return result;
        }

        public static List<int> AddInt(this List<int> list, int a)
        {
            List<int> res = new List<int>(list.Count);
            foreach (int x in list)
                res.Add(x + a);
            return res;
        }
    }
}
