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
        private static readonly int BiggestGem = 5;

        // List of colors to be considered Uncertain by Block Predicate.
        private static List<Color> Colors;

        public static void Load()
        {
            Colors = new List<Color>();

            // dust.png
            // In case custom textures aren't used.
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
        }

        public static bool IsIgnored(this Color color)
        {
            // Ignoring shades of green, to take advantage of custom textures. Distortion is considered.
            if (color.R < 2 && color.B < 2)
                return true;
            foreach (Color c in Colors)
                if (color.Is(c))
                    return true;
            return false;
        }
    }
}
