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
            // REQUIREMENT: Bitmap has platforms all the way to the left/right.
            bool startfromleft = (SeekArea.X + SeekArea.Width / 2) > WindowPos.Width;

            Point now = new Point(startfromleft ? 0 : bitmap.Width - 1, bitmap.Height - 2);

            while (!(PlatformDark.IsColorAt(now, bitmap) && PlatformDark.IsColorAt(now.Add(0, -8), bitmap))){
                if (now.Y < 8)
                    return InvalidPoint;
                now.Y -= 1;
            }

            while (!(PlatformDark.IsColorAt(now.Add(0, 1), bitmap) && PlatformDark.IsColorAt(now.Add(startfromleft ? 7 : -7, 1), bitmap)))
            {
                if ((startfromleft && now.X >= bitmap.Width - 8) || (!startfromleft && now.X <= 7))
                    return InvalidPoint;
                now.X += startfromleft ? 1 : -1;
            }

            return now.Add(startfromleft ? 20 : 13, -8);
        }

        /// <summary>
        /// Finds player's topleft pixel on the shot.<br/>
        /// Automatically adds SeekArea position, so result is relative to window.
        /// </summary>
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

        // The rest is the block predicate.

        // Since there are three possible outcomes, we're using this fake enumerable.
        // Uncertain returns when all possible checks return explosion colors.
        public static readonly int Block = 0;
        public static readonly int Air = 1;
        public static readonly int Uncertain = 2;

        /* Laser Grids are a real pain to work with here.
         * Most of their sprite is in grey scale, so I can't use it because that's how I check for explosions.
         * That leaves me with two 3x3 squares of random green pixels, toplefting at (1,28) and (28,28).
         * If that's not enough, Laser Grids can be placed in two directions, so both cases have to be checked.
         * In the end, we're checking 18 pixels, for 2 possibilities in each (except square centers which are the same).
         */

        // Green square 1, if seen on the left side.
        public static readonly Color[,] GS1 = new Color[,]
        {
            {Color.FromArgb(106,185,102), Color.FromArgb(80,160,76),   Color.FromArgb(138,207,106) },
            {Color.FromArgb(69,149,65),   Color.FromArgb(196,255,137), Color.FromArgb(106,185,102) },
            {Color.FromArgb(106,185,102), Color.FromArgb(85,164,81),   Color.FromArgb(85,164,81)   }
        };

        // Green square 2, if seen on the left side.
        public static readonly Color[,] GS2 = new Color[,]
        {
            {Color.FromArgb(151,220,119), Color.FromArgb(69,149,65),   Color.FromArgb(69,149,65)   },
            {Color.FromArgb(69,149,65),   Color.FromArgb(196,255,137), Color.FromArgb(106,185,102) },
            {Color.FromArgb(106,185,102), Color.FromArgb(69,149,65),   Color.FromArgb(69,149,65)   }
        };

        public static int HasBlockAt(this Bitmap bitmap, int off)
        {
            // Crawling both squares.
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    // Take both colors at once, mirroring the right square.
                    // Screenshot height of 32 pixels matching the offset is a requirement.
                    Color lcolor = bitmap.GetPixel((off + 1) + x, (28) + y);
                    Color rcolor = bitmap.GetPixel((off + 30) - x, (28) + y);
                    // If explosion, ignore.
                    if (lcolor.IsGrayScale() || rcolor.IsGrayScale())
                        continue;
                    // If matches, good.
                    if (lcolor.Is(GS1[x, y]) || lcolor.Is(GS2[x, y]) || rcolor.Is(GS1[x, y]) || rcolor.Is(GS2[x, y]))
                        return Block;
                    // If neither, good.
                    return Air;
                }
            // If always explosion, to hell with it.
            return Uncertain;
        }
    }
}
