using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace TeslaX
{
    public static class Ignorable
    {
        // Largest gem to be considered (1-5, but really 1-3).
        private static readonly int BiggestGem = 3;

        // List of colors to be considered Uncertain by Block Predicate.
        private static List<Color> Colors;

        public static void Load()
        {
            Colors = new List<Color>();

            // Most Ignorable sources are deprecated in favor of texture injection.

            // dust.png
            /*
            using(Bitmap dust = Properties.Resources.dust)
            {
                Color color;
                for(int x = 0; x < dust.Width; x++)
                    for(int y = 0; y < dust.Height; y++)
                    {
                        color = dust.GetPixel(x, y);
                        if (color.A == 255 && !Colors.Contains(color))
                            Colors.Add(color);
                    }
            }
            */

            // gems.png
            using (Bitmap gems = Properties.Resources.gems)
            {
                Color color;
                int k = BiggestGem;
                for (int x = 0; x < k*32; x++)
                    for (int y = 0; y < gems.Height; y++)
                    {
                        color = gems.GetPixel(x, y);
                        if (color.A == 255 && !Colors.Contains(color))
                            Colors.Add(color);
                    }
            }

            // Fist
            Colors.AddRange(Player.FistColors());

            // (0,255,0) from injected pickup_box.rttex
            Colors.Add(Color.FromArgb(0, 255, 0));
        }

        public static bool IsIgnored(this Color color)
        {
            foreach (Color c in Colors)
                if (color.Is(c))
                    return true;
            return false;
        }
    }
}
