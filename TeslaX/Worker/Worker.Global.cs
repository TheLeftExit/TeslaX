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
        /// As a bonus, handles "out of bounds" exceptions.
        /// </summary>
        public static bool IsColorAt(this Color color, Point point, Bitmap bitmap)
        {
            if (point.X < 0 || point.Y < 0 || point.X >= bitmap.Size.Width || point.Y >= bitmap.Size.Height)
                return false;
            return bitmap.GetPixel(point.X, point.Y).Equals(color);
        }

        public static Point Add(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }

        public static Point Mod(this Point point, int mod)
        {
            return new Point(point.X % mod, point.Y % mod);
        }
    }
}
