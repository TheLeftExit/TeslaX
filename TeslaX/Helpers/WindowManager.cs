using System;
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

        public WindowManager()
        {
            HwndObject = HwndObject.GetWindowByTitle("Growtopia");
            if (HwndObject.Hwnd == IntPtr.Zero)
                return;
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
