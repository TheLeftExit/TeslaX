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
        /// Area to screenshot, relative to WINDOW.
        /// </summary>
        public static Rectangle SeekArea;

        /// <summary>
        /// Location of the fully rendered block closest to top left, relative to WINDOW.
        /// </summary>
        public static Point Offset;

        /// <summary>
        /// Player's last known location, relative to WINDOW.
        /// </summary>
        public static Point LastKnown;

        /// <summary>
        /// Last taken Screenshot.
        /// </summary>
        public static Bitmap CurrentShot;

        // Farming direction.
        public static bool Right;
        public static bool Down;

        // Returns a Bitmap of part of the screen. Resource costly.
        public static Bitmap Screenshot(int x, int y, int w, int l)
        {
            Bitmap res = new Bitmap(w, l);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.CopyFromScreen(x, y, 0, 0, res.Size);
            }
            return res;
        }

        // Takes a screenshot of currently selected SeekArea and stores it in CurrentShot.
        public static void Screenshot()
        {
            CurrentShot = Screenshot(SeekArea.X + WindowPos.X, SeekArea.Y + WindowPos.Y, SeekArea.Width, SeekArea.Height);
        }

        // Checks if a point of Bitmap matches a color.
        // Accounts for possible color distortion.
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

        // Above, for standalone color.
        public static bool Is(this Color source, Color color)
        {
            if (Math.Abs(color.R - source.R) > 2)
                return false;
            if (Math.Abs(color.G - source.G) > 2)
                return false;
            if (Math.Abs(color.B - source.B) > 2)
                return false;
            return true;
        }

        // Checks if the color is a shade of gray, which explosions are drawn with.
        // Accounts for possible color distortion.
        public static bool IsGrayScale(this Color source)
        {
            if (Math.Abs(source.R - source.G) > 2)
                return false;
            if (Math.Abs(source.G - source.B) > 2)
                return false;
            if (Math.Abs(source.B - source.R) > 2)
                return false;
            return true;
        }

        public static bool IsColorAt(this Color color, int x, int y, Bitmap bitmap)
        {
            return color.IsColorAt(new Point(x, y), bitmap);
        }

        /// <summary>
        /// Add (x, y) to a point. Handy!
        /// </summary>
        public static Point Add(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }

        /// <summary>
        /// Find modulo of a point. Handy!
        /// </summary>
        public static Point Mod(this Point point, int mod)
        {
            return new Point(point.X % mod, point.Y % mod);
        }

        // Offset-ify the offset. Get it?
        // Found myself using *.Add().Mod() in multiple places and decided to generalize it.
        public static Point ify(this Point point)
        {
            return point.Add(SeekArea.X, SeekArea.Y).Mod(32);
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
