using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TheLeftExit.TeslaX.Static
{
    internal struct ProcessHandle
    {
        public long BaseAddress;
        public IntPtr OpenHandle;
    }

    internal static class Memory
    {
        private const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public static ProcessHandle GetHandle(this Process process) => new ProcessHandle
        {
            BaseAddress = (long)process.MainModule.BaseAddress,
            OpenHandle = OpenProcess(PROCESS_WM_READ, false, process.Id)
        };

        private static byte[] GetBytes(this ProcessHandle handle, long entryPoint, int byteCount, params int[] offsets)
        {
            int dummynum = 0;

            byte[] addressContainer = new byte[8];
            long target = entryPoint + handle.BaseAddress;

            for (int i = 0; i < offsets.Length; i++)
            {
                ReadProcessMemory((int)handle.OpenHandle, target, addressContainer, 8, ref dummynum);
                target = BitConverter.ToInt64(addressContainer, 0) + offsets[i];
            }

            byte[] res = new byte[byteCount];
            ReadProcessMemory((int)handle.OpenHandle, target, res, byteCount, ref dummynum);
            return res;
        }

        // Values in the following functions have been pointer-sniped with Cheat Engine.
        // I'm not sure yet if they change with EXE updates.
        // Either way, feel free to use them in your app.

        public static Point GetPlayer(this ProcessHandle handle)
        {
            var res = new Point();

            byte[] rawx = handle.GetBytes(0x40A730, 4, 0xB10, 0x180, 0x8);
            res.X = Convert.ToInt32(BitConverter.ToSingle(rawx, 0));

            byte[] rawy = handle.GetBytes(0x40A730, 4, 0xB10, 0x180, 0xC);
            res.Y = Convert.ToInt32(BitConverter.ToSingle(rawy, 0));

            return res;
        }

        public static short GetBlock(this ProcessHandle handle, int x, int y)
        {
            int lastoffset = 0x04 + x * 0x80 + y * 0x3200;
            byte[] rawx = handle.GetBytes(0x40A730, 2, 0xB10, 0x130, 0x8, 0x28, lastoffset);
            return BitConverter.ToInt16(rawx, 0);
        }

        public static short GetBackground(this ProcessHandle handle, int x, int y)
        {
            int lastoffset = 0x06 + x * 0x80 + y * 0x3200;
            byte[] rawx = handle.GetBytes(0x40A730, 2, 0xB10, 0x130, 0x8, 0x28, lastoffset);
            return BitConverter.ToInt16(rawx, 0);
        }

        public static bool GetDirection(this ProcessHandle handle)
        {
            byte[] raw = handle.GetBytes(0x40A730, 1, 0xB10, 0x180, 0x131);
            return raw[0] == 0;
        }
    }
}
