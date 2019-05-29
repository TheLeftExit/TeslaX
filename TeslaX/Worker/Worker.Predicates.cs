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
            // Identifying Sheet Music: Repeat Begin behind Wooden Platform, assuming it's ALWAYS and ONLY behind platforms.
            // Also assuming the bottom gray line isn't blocked by hat items or nickname.

            Color NoteBlack = Color.FromArgb(54, 54, 54);
            Color NoteGray = Color.FromArgb(187, 187, 187); // also 186

            Point now = new Point(3, 0); // (0,0) makes it difficult to handle offsets where X is close to 0.

            while (!NoteGray.IsColorAt(now, bitmap))
            {
                now.Y += 1;
                if (now.Y >= bitmap.Size.Height)
                    return InvalidPoint;
                while (NoteBlack.IsColorAt(now, bitmap) || NoteBlack.IsColorAt(now.Add(-3, 0), bitmap))
                {
                    now.X += 1;
                    if (now.X >= bitmap.Size.Width)
                        return InvalidPoint;
                }
            }

            while (NoteGray.IsColorAt(now, bitmap))
            {
                now.X += 1;
                if (now.X >= bitmap.Size.Height)
                    return InvalidPoint;
            }

            return now.Add(0,-26);
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
