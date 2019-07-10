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
        // REALLY long list of colors to be considered Uncertain by Block Predicate.
        public static List<Color> Colors; // Expect >500 colors.
        // Benchmark: checking 100 different colors against each of those: a couple milliseconds. That'll work.

        public static void Load()
        {
            Colors = new List<Color>();

            // dust.png
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
                int k = TechSettings.BiggestGem;
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
            foreach (Color c in Colors)
                if (color.Is(c))
                    return true;
            return false;
        }
    }
}
