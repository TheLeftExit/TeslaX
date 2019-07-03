using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwndObject = WindowScrape.Types.HwndObject;
using WM = WindowScrape.Constants.WM;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TeslaX
{
    public static class Key
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys k);

        public static void Down(this Keys k)
        {
            Window.HwndObject.SendMessage(WM.KEYDOWN, (uint)k, 0);
        }

        public static void Up(this Keys k)
        {
            Window.HwndObject.SendMessage(WM.KEYUP, (uint)k, 0);
        }

        public static bool IsDown(this Keys k)
        {
            return GetAsyncKeyState(k) != 0;
        }
    }
}
