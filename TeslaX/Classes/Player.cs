using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public static class Player
    {
        private static readonly Color[] SkinColors = new Color[] {
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

        public static int[] FistShades = new int[] { 234, 218, 200, 181, 141 };

        public static List<Color> FistColors()
        {
            List<Color> res = new List<Color>(FistShades.Length);
            for (int i = 0; i < FistShades.Length; i++)
                res.Add(SkinColors[Settings.SkinColor].Dim(FistShades[i]));
            return res;
        }

        public static Bitmap Head;

        public static void Load()
        {
            Head = Properties.Resources.head;
            for (int x = 0; x < Head.Width; x++)
                for (int y = 0; y < Head.Height; y++)
                    if (Head.GetPixel(x, y).A == 255)
                    {
                        Head.SetPixel(x, y, SkinColors[Settings.SkinColor].Dim(Head.GetPixel(x, y).R));
                    }
        }

        /// <summary>0: None, 1: Left, 2: Right</summary>
        public static int HasPlayer(this Screenshot shot, int x, int y)
        {
            Color PlayerDark = Head.GetPixel(5, 1);
            Color PlayerLight = Head.GetPixel(6, 1);
            for (int ya = 1; ya <= 14; ya++) {
                if (PlayerDark.IsColorAt(x + 5, y + ya, shot))
                {
                    for (int yb = 1; yb <= 14; yb++)
                        if (PlayerLight.IsColorAt(x + 6, y + yb, shot))
                            return 2;
                    break;
                }
                if (PlayerDark.IsColorAt(x + 26, y + ya, shot))
                {
                    for (int yb = 1; yb <= 14; yb++)
                        if (PlayerLight.IsColorAt(x + 25, y + yb, shot))
                            return 1;
                    break;
                }
            }
            return 0;
        }
    }
}
