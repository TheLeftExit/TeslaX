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
                return false;
            }

            using (Screenshot firstshot = new Screenshot())
            {
                if (!firstshot.SetNewOffset())
                {
                    MessageBox.Show("Failed to find offset. Make sure you're in a fully platformed world.");
                    return false;
                }

                if (!firstshot.SetNewPlayer())
                {
                    MessageBox.Show("Failed to find player. Make sure your character has no hair/hat items, and skin color is set in Settings.cs.");
                    return false;
                }
            }

            new Thread(RowLoop).Start();
            return true;
        }
    }
}
