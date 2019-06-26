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
            if (!Window.Load())
            {
                MessageBox.Show("Failed to find window.");
                Restore();
                return;
            }

            Screenshot firstshot = new Screenshot(0, 0, Window.Width, Window.Height);

            Offset = firstshot.GetOffset(true);
            if (Offset == InvalidPoint)
            {
                MessageBox.Show("Failed to find offset. Make sure you're in a fully platformed world.");
                Restore();
                return;
            }

            LastKnown = firstshot.GetPlayer(Right, Offset.Y);
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
