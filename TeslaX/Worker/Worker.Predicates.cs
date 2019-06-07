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
        public static Point GetOffset(Bitmap bitmap)
        {
            Color PlatformDark = Color.FromArgb(112, 71, 28);
            // Since we're starting from a side edge, we're making sure that we don't run into area without platforms.
            // Assuming that the farm is platformed everywhere except at most several blocks on edges.
            bool startfromleft = (SeekArea.X + SeekArea.Width / 2) > WindowPos.Width;

            Point now = new Point(startfromleft ? 0 : bitmap.Width - 1, bitmap.Height - 1);

            while (!(PlatformDark.IsColorAt(now, bitmap) && PlatformDark.IsColorAt(now.Add(0, -8), bitmap))){
                if (now.Y < 8)
                    return InvalidPoint;
                now.Y -= 1;
            }

            while (!(PlatformDark.IsColorAt(now.Add(0, 1), bitmap) && PlatformDark.IsColorAt(now.Add(startfromleft ? 7 : -7, 1), bitmap)))
            {
                if ((startfromleft && now.X > bitmap.Width - 8) || (!startfromleft && now.X < 7))
                    return InvalidPoint;
                now.X += startfromleft ? 1 : -1;
            }

            return now.Add(startfromleft ? 20 : 13, -8);
        }

        public static Point GetPlayer(Bitmap bitmap)
        {
            // Identifying player's location based on Barky's Mask.
            // Y: 7

            Color NoseGray = Color.FromArgb(27, 27, 27); // 27 27 28
            Color NoseWhite = Color.FromArgb(254, 254, 254); // or 253

            List<int> EligibleY = EligibleBetween(SeekArea.Y, SeekArea.Y + SeekArea.Height, 7 + Offset.Y).AddInt(-SeekArea.Y);
            foreach(int y in EligibleY)
            {
                for(int x = 0 + 1; x < bitmap.Width - 1; x++)
                {
                    if (NoseWhite.IsColorAt(x, y, bitmap) && NoseGray.IsColorAt(x - 1, y, bitmap) && NoseGray.IsColorAt(x + 1, y, bitmap))
                        return new Point(x, y).Add(Right ? -29 : -2, -7).Add(SeekArea.X, SeekArea.Y);
                }
            }

            return InvalidPoint;
        }

    }
}
