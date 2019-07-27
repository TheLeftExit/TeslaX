﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TheLeftExit.TeslaX.Entities;
using HwndObject = WindowScrape.Types.HwndObject;
using WM = WindowScrape.Constants.WM;

namespace TheLeftExit.TeslaX.Helpers
{
    internal class WindowManager
    {
        public HwndObject HwndObject;

        public bool Windowed;

        public Point Location { get { return new Point(X, Y); } }
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

        public void SendKey(Keys k, bool down) =>
            HwndObject.SendMessage(down ? WM.KEYDOWN : WM.KEYUP, (uint)k, 0);

        public void HoldKey(Keys k, ushort ms)
        {
            SendKey(k, true);
            Thread.Sleep(ms);
            SendKey(k, false);
        }
    }
}
