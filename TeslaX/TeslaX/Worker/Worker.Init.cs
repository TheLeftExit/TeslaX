﻿using System;
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
    public partial class Worker
    {
        public static void Init()
        {
            // Getting window handle and window position
            Window = HwndObject.GetWindowByTitle("Growtopia");
            if(Window.Hwnd == IntPtr.Zero)
            {
                MessageBox.Show("Window handle is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            WindowPos = new Rectangle(Window.Location, Window.Size);

            // Getting SeekArea
            SlaveForm slaveForm = new SlaveForm();
            slaveForm.ShowDialog();

            // Getting offset.
            Bitmap tmpshot = Screenshot(SeekArea.X + WindowPos.X, SeekArea.Y + WindowPos.Y, SeekArea.Width, SeekArea.Height);
            Offset = GetOffset(tmpshot);
            if (Offset.Equals(InvalidPoint))
            {
                MessageBox.Show("InvalidPoint");
                return;
            }
            Offset = Offset.Add(SeekArea.X, SeekArea.Y).Mod(32);
            MessageBox.Show(Offset.ToString());
            return;

        }
    }
}