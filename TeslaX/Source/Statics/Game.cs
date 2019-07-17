using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TeslaX.Properties;

namespace TeslaX
{
    static class Game
    {
        public static readonly Color[] SkinColors = new Color[] {
                Color.FromArgb(119, 91, 80),
                Color.FromArgb(149, 113, 99),
                Color.FromArgb(179, 137, 119),
                Color.FromArgb(194, 148, 129),
                Color.FromArgb(223, 171, 149),
                Color.FromArgb(253, 194, 169),
                Color.FromArgb(253, 205, 179),
                Color.FromArgb(253, 227, 199),
                Color.FromArgb(176, 219, 162),
                Color.FromArgb(65, 193, 196),
                Color.FromArgb(213, 75, 43),
                Color.FromArgb(65, 137, 42),
                Color.FromArgb(168, 81, 212),
                Color.FromArgb(238, 238, 238),
            };

        public static Bitmap GetFistBitmap(int color)
        {
            Bitmap res = new Bitmap(Resources.puncharm);
            for (int x = 0; x < res.Width; x++)
                for (int y = 0; y < res.Width; y++)
                {
                    Color c = res.GetPixel(x, y);
                    c = SkinColors[color].Dim(c.R);
                    res.SetPixel(x, y, c);
                }
            return res;
        }
    }
}
