using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace TheLeftExit.TeslaX.Static
{
    internal class ProcessHandle
    {
        public long BaseAddress;
        public IntPtr MemoryHandle;
        public IntPtr WindowHandle;
    }

    internal static class ProcessManipulations
    {
        // Memory manipulations.
        private const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private static byte[] GetBytes(this ProcessHandle handle, long entryPoint, int byteCount, params int[] offsets)
        {
            int dummynum = 0;

            byte[] addressContainer = new byte[8];
            long target = entryPoint + handle.BaseAddress;

            for (int i = 0; i < offsets.Length; i++)
            {
                ReadProcessMemory((int)handle.MemoryHandle, target, addressContainer, 8, ref dummynum);
                target = BitConverter.ToInt64(addressContainer, 0) + offsets[i];
            }

            byte[] res = new byte[byteCount];
            ReadProcessMemory((int)handle.MemoryHandle, target, res, byteCount, ref dummynum);
            return res;
        }

        // Window manipulations.
        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        public static void SendKey(this ProcessHandle handle, Keys key, bool down) =>
            SendMessage(handle.WindowHandle, down ? WM_KEYDOWN : WM_KEYUP, (uint)key, 0);

        public static void HoldKey(this ProcessHandle handle, Keys key, int duration)
        {
            handle.SendKey(key, true);
            Thread.Sleep(duration);
            handle.SendKey(key, false);
        }

        // Constructor.
        public static ProcessHandle GetHandle(this Process process) => new ProcessHandle
        {
            BaseAddress = (long)process.MainModule.BaseAddress,
            MemoryHandle = OpenProcess(PROCESS_WM_READ, false, process.Id),
            WindowHandle = process.MainWindowHandle
        };

        // Values in the following functions have been pointer-sniped with Cheat Engine.
        // Feel free to use them in your app.

        public static Point GetPlayer(this ProcessHandle handle)
        {
            var res = new Point();

            byte[] rawx = handle.GetBytes(0x4EEEF8, 4, 0xCC8, 0x188, 0x8);
            res.X = Convert.ToInt32(BitConverter.ToSingle(rawx, 0));

            byte[] rawy = handle.GetBytes(0x4EEEF8, 4, 0xCC8, 0x188, 0xC);
            res.Y = Convert.ToInt32(BitConverter.ToSingle(rawy, 0));

            return res;
        }

        public static short GetBlock(this ProcessHandle handle, int x, int y)
        {
            int lastoffset = 0x04 + x * 0x80 + y * 0x3200;
            byte[] rawx = handle.GetBytes(0x4EEEF8, 2, 0xCC8, 0x130, 0x28, lastoffset);
            return BitConverter.ToInt16(rawx, 0);
        }

        public static short GetBackground(this ProcessHandle handle, int x, int y)
        {
            int lastoffset = 0x06 + x * 0x80 + y * 0x3200;
            byte[] rawx = handle.GetBytes(0x4EEEF8, 2, 0xCC8, 0x130, 0x28, lastoffset);
            return BitConverter.ToInt16(rawx, 0);
        }

        public static bool GetDirection(this ProcessHandle handle)
        {
            byte[] raw = handle.GetBytes(0x4EEEF8, 1, 0xCC8, 0x188, 0x131);
            return raw[0] == 0;
        }
    }
}
