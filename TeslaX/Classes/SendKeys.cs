using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwndObject = WindowScrape.Types.HwndObject;
using WM = WindowScrape.Constants.WM;
using System.Windows.Forms;

namespace TeslaX
{
    public enum KeyCode
    {
        Left = 0x41, // A
        Right = 0x44, // D
        Punch = 0x20 // Spacebar
    }

    public static class Key
    {
        public static void Send(KeyCode k, bool down)
        {
            Window.HwndObject.SendMessage(down ? WM.KEYDOWN : WM.KEYUP, (uint)k, 0);
        }

        public static bool Down(KeyCode k)
        {
            // To be implemented
            return false;
        }
    }
}
