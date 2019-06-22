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
    public partial class Worker
    {
        public static void Init()
        {
            Busy = true;

            Window = HwndObject.GetWindowByTitle("Growtopia");
            if(Window.Hwnd == IntPtr.Zero)
            {
                MessageBox.Show("Window handle is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (Windowed)
                WindowPos = new Rectangle(Window.Location.X + 8, Window.Location.Y + 31, Window.Size.Width, Window.Size.Height);
            else
                WindowPos = new Rectangle(Window.Location, Window.Size);

            SlaveForm slaveForm = new SlaveForm();
            slaveForm.ShowDialog(); // Sets SeekArea.

            Bitmap firstshot = Screenshot(SeekArea.X + WindowPos.X, SeekArea.Y + WindowPos.Y, SeekArea.Width, SeekArea.Height);

            Offset = GetOffset(firstshot);
            if (Offset == InvalidPoint)
            {
                MessageBox.Show("Failed to find offset in selected area.");
                Restore();
                return;
            }
            Offset = Offset.ify();

            LastKnown = GetPlayer(firstshot);
            if(LastKnown == InvalidPoint)
            {
                MessageBox.Show("Failed to find player in selected area.");
                Restore();
                return;
            }

            // To be appended.
            Ignorable.Load();
            Block.Load();
            ToWorking();
            RowLoop();
        }
    }
}
