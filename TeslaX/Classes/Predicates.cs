using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public static class Predicates
    {
        public static Point GetOffset(Bitmap bitmap)
        {
            Color PlatformDark = Color.FromArgb(112, 71, 28);
            bool startfromleft = (SeekArea.X + SeekArea.Width / 2) > WindowPos.Width;

            Point now = new Point(startfromleft ? 0 : bitmap.Width - 1, bitmap.Height - 2);

            while (!(PlatformDark.IsColorAt(now, bitmap) && PlatformDark.IsColorAt(now.Add(0, -8), bitmap)))
            {
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
            foreach (int y in EligibleY)
            {
                for (int x = 0 + 1; x < bitmap.Width - 1; x++)
                {
                    if (EarBrown1.IsColorAt(x + (Right ? -1 : 1), y, bitmap) && EarBrown2.IsColorAt(x, y, bitmap) && EarBrown3.IsColorAt(x + (Right ? 1 : -1), y, bitmap))
                        return new Point(x, y).Add(Right ? -4 : -27, -10).Add(SeekArea.X, SeekArea.Y);
                }
            }

            return InvalidPoint;
        }
    }
}
