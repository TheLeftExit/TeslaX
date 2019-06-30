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
        public static bool SetNewOffset(this Screenshot shot)
        {
            Point res = shot.GetOffset(true).Mod(32);
            if (res == Global.InvalidPoint)
                return false;
            Offset = res;
            return true;
        }

        public static bool SetNewPlayer(this Screenshot shot)
        {
            List<int> EligibleY = Global.EligibleBetween(0, shot.Height - 32, Offset.Y).AddInt(-shot.Y);
            foreach(int y in EligibleY)
            {
                var res = shot.SeekPlayer(0, shot.Width - 32 - 1, y);
                if (res.x != -1)
                {
                    // Assuming that when LastKnown is (Window.Width / 2 - 16), it's in the center.
                    // Tested in Windowed on two different resolutions. Might be wrong in other cases.
                    LastKnown = new Smooth<Point>(250, (ov, nv) => Math.Abs(ov.X - nv.X) > Settings.MaxSpeed || (ov.X == Window.Width / 2 - 16 && ov != nv)); ;
                    LastKnown.Value = new Point(res.x, y);
                    Right = res.Right;
                    return true;
                }
            }
            return false;
        }

        public static bool Init()
        {
            // Loading window.
            if (!Window.Load())
            {
                MessageBox.Show("Failed to find window. Make sure you've launched Growtopia.");
                return false;
            }

            // Taking initial screenshot.
            using (Screenshot firstshot = new Screenshot())
            {

                // Finding offset.
                if (!firstshot.SetNewOffset())
                {
                    MessageBox.Show("Failed to find offset. Make sure you're in a fully platformed world.");
                    return false;
                }

                // Finding player.
                if (!firstshot.SetNewPlayer())
                {
                    MessageBox.Show("Failed to find player. Make sure your character has no hair/hat items, and skin color is set in Settings.cs.");
                    return false;
                }
            }

            // Going at it.
            new Thread(RowLoop).Start();
            return true;
        }
    }
}
