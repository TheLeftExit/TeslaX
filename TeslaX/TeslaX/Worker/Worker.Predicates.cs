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

            return now.Add(-6,-26);
        }
    }
}
