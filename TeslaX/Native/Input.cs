using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLeftExit.TeslaX
{
    public static class Input
    {
        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        public static void SendKey(this IntPtr handle, Keys key, bool down)
        {
            if (handle != IntPtr.Zero)
                SendMessage(handle, down ? WM_KEYDOWN : WM_KEYUP, (uint)key, 0);
        }

        public static async Task HoldKeyAsync(this IntPtr handle, Keys key, int duration)
        {
            handle.SendKey(key, true);
            await Task.Delay(duration);
            handle.SendKey(key, false);
        }
    }
}
