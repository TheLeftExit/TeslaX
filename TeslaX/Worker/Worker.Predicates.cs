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
            bool startfromleft = (SeekArea.X + SeekArea.Width / 2) > WindowPos.Width;

            Point now = new Point(startfromleft ? 0 : bitmap.Width - 1, bitmap.Height - 2);

            while (!(PlatformDark.IsColorAt(now, bitmap) && PlatformDark.IsColorAt(now.Add(0, -8), bitmap))){
                if (now.Y <= 8)
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

        // Automatically adds SeekArea position, so result is relative to window.
        public static Point GetPlayer(Bitmap bitmap)
        {
            // Using Barky's Mask.
            Color EarBrown1 = Color.FromArgb(144, 121, 88);
            Color EarBrown2 = Color.FromArgb(155, 130, 96);
            Color EarBrown3 = Color.FromArgb(166, 138, 99);

            List<int> EligibleY = EligibleBetween(SeekArea.Y, SeekArea.Y + SeekArea.Height - 1, 10 + Offset.Y).AddInt(-SeekArea.Y);
            foreach(int y in EligibleY)
            {
                for(int x = 0 + 1; x < bitmap.Width - 1; x++)
                {
                    if (EarBrown1.IsColorAt(x + (Right ? -1 : 1), y, bitmap) && EarBrown2.IsColorAt(x , y, bitmap) && EarBrown3.IsColorAt(x + (Right ? 1: -1), y, bitmap))
                        return new Point(x, y).Add(Right ? -4 : -27, -10).Add(SeekArea.X, SeekArea.Y);
                }
            }

            return InvalidPoint;
        }

        // The rest is the block predicate. DEPRECATED, to be removed once alternative is working.

        /*

        // Since there are three possible outcomes, we're using this fake enumerable.
        // Uncertain returns when all possible checks return explosion colors.
        public static readonly int Block = 0;
        public static readonly int Air = 1;
        public static readonly int Uncertain = 2;


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
                    // First, check for out-of-bounds cases and ignore them [swap in gray scale]. | (29,28) and (29,29) are failible
                    Color lcolor;
                    if (((off + 1) + x >= 0) && ((off + 1) + x < bitmap.Width))
                        lcolor = bitmap.GetPixel((off + 1) + x, (28) + y);
                    else
                        lcolor = Color.FromArgb(0, 0, 0);
                    Color rcolor;
                    if (((off + 30) - x >= 0) && ((off + 30) - x < bitmap.Width) && (x, y) != (2, 1) && (x, y) != (2, 2))
                        rcolor = bitmap.GetPixel((off + 30) - x, (28) + y);
                    else
                        rcolor = Color.FromArgb(0, 0, 0);
                    // If matches, good.
                    if (lcolor.Is(GS1[x, y]) || lcolor.Is(GS2[x, y]) || rcolor.Is(GS1[x, y]) || rcolor.Is(GS2[x, y]))
                        return Block;
                    // If explosion, ignore.
                    if (lcolor.IsGrayScale() && rcolor.IsGrayScale())
                        continue;
                    // If neither, good.
                    return Air;
                }
            // If always explosion, to hell with it.
            // Will probably treat this as block anyway, but oh well.
            return Uncertain;
        }

        */
    }
}
