using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;
using WM = WindowScrape.Constants.WM;
using System.Runtime.InteropServices;

namespace TeslaX
{
    class WindowManager
    {
        public HwndObject HwndObject;

        public bool Windowed;

        public Point Location { get { return new Point(X,Y); } }
        public int X { get { return HwndObject.Location.X + (Windowed ? 8 : 0); } }
        public int Y { get { return HwndObject.Location.Y + (Windowed ? 31 : 0); } }
        public Size Size { get { return HwndObject.Size; } }
        public int Width { get { return Size.Width; } }
        public int Height { get { return Size.Height; } }
        public Rectangle Rectangle { get { return new Rectangle(Location, Size); } }

        public WindowManager(bool windowed)
        {
            HwndObject = HwndObject.GetWindowByTitle("Growtopia");
            if (HwndObject.Hwnd == IntPtr.Zero)
                return;
            Windowed = windowed;
        }

        public Screenshot Shoot(int x, int y, int w, int h) =>
            new Screenshot(x + X, y + Y, w, h) { Location = new Point(x, y) };

        public Screenshot Shoot() =>
            Shoot(0, 0, Width, Height);

        public void SendKey(Keys k, bool down) =>
            HwndObject.SendMessage(down ? WM.KEYDOWN : WM.KEYUP, (uint)k, 0);

        public async void HoldKey(Keys k, ushort ms)
        {
            SendKey(k, true);
            await Task.Delay(ms);
            SendKey(k, false);
        }
    }
}
