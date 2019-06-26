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
    public static class Window
    {
        public static HwndObject HwndObject;

        public static Point Location { get { return new Point(X,Y); } }
        public static int X { get { return HwndObject.Location.X + (Windowed ? 8 : 0); } }
        public static int Y { get { return HwndObject.Location.Y + (Windowed ? 31 : 0); } }
        public static Size Size { get { return HwndObject.Size; } }
        public static int Width { get { return Size.Width; } }
        public static int Height { get { return Size.Height; } }
        public static Rectangle Rectangle { get { return new Rectangle(Location, Size); } }

        public static bool Windowed;

        public static bool Load()
        {
            HwndObject = HwndObject.GetWindowByTitle("Growtopia");
            if (HwndObject.Hwnd == IntPtr.Zero)
                return false;
            Screenshot.LocationOffset = Location;
            return true;
        }
    }
}
