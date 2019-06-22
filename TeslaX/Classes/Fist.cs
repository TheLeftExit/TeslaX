using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public static class Fist
    {
        public static Color[] SkinColors = new Color[] {
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

        public static int[] Shades = new int[] { 234, 218, 200, 181, 141 };

        public static List<Color> FistColors(int n)
        {
            List<Color> res = new List<Color>(Shades.Length);
            for (int i = 0; i < Shades.Length; i++)
                res.Add(SkinColors[n].Dim(Shades[i]));
            return res;
        }
    }
}
