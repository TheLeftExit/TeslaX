using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public partial class Worker
    {
        public static bool Init()
        {
            if (!Window.Load())
            {
                MessageBox.Show("Failed to find window. Make sure you've launched Growtopia.");
                //Restore();
                return false;
            }

            Screenshot firstshot = new Screenshot(0, 0, Window.Width, Window.Height);

            Offset = firstshot.GetOffset(true).Mod(32);
            if (Offset == InvalidPoint)
            {
                MessageBox.Show("Failed to find offset. Make sure you're in a fully platformed world.");
                return false;
            }

            LastKnown = firstshot.GetPlayer(Right, Offset.Y);
            if(LastKnown == InvalidPoint)
            {
                MessageBox.Show("Failed to find player. Make sure you're wearing an unobstructed Barky's Mask.");
                return false;
            }

            new Thread(RowLoop).Start();
            return true;
        }
    }
}
