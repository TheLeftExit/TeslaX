using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public static class Extensions
    {
        public static Color GetPixel(this Bitmap bitmap, Point point)
        {
            return bitmap.GetPixel(point.X, point.Y);
        }

        public static Color Dim(this Color color, int d)
        {
            int Dim(int a, int b)
            {
                return (int)Math.Round((double)a * b / 254);
                // When predicting fist colors, MSD with 254 is a bit lower than with 255.
            }
            return Color.FromArgb(Dim(color.R, d), Dim(color.G, d), Dim(color.B, d));
        }

        public static bool Is(this Color source, Color color)
        {
            int d = 3; // Distortion value.
            if (Math.Abs(color.R - source.R) > d)
                return false;
            if (Math.Abs(color.G - source.G) > d)
                return false;
            if (Math.Abs(color.B - source.B) > d)
                return false;
            return true;
        }

        public static bool IsColorAt(this Color color, Point point, Bitmap bitmap)
        {
            
            return bitmap.GetPixel(point).Is(color);
        }

        public static bool IsColorAt(this Color color, int x, int y, Bitmap bitmap)
        {
            return bitmap.GetPixel(x, y).Is(color);
        }

        public static bool Contains(this Bitmap bitmap, int x, int y)
        {
            GraphicsUnit g = GraphicsUnit.Pixel;
            return bitmap.GetBounds(ref g).Contains(x, y);
            // Who ever takes enum as ref?
        }

        public static bool Contains(this Bitmap bitmap, Point point)
        {
            return bitmap.Contains(point.X, point.Y);
        }

        public static Point Add(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }

        public static Point Add(this Point point, Point dpoint)
        {
            return point.Add(dpoint.X, dpoint.Y);
        }

        public static Point Mod(this Point point, int mod)
        {
            return new Point(point.X % mod, point.Y % mod);
        }

        public static Point Flip(this Point point)
        {
            return new Point(31 - point.X, point.Y);
        }

        public static Screenshot Screenshot(this Rectangle source)
        {
            return new Screenshot(source.X, source.Y, source.Width, source.Height);
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
