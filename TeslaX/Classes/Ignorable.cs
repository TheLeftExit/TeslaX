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
        // List of point on a 32x32 grid to be ignored by Block Predicate.
        public static List<Point> Points; // Count: 236

        // REALLY long list of colors to be considered Uncertain by Block Predicate.
        public static List<Color> Colors; // Count: 863
        // Benchmark: checking 100 different colors against each of those: 5-8 ms. That'll work.

        public static void Load()
        {
            Points = new List<Point>();
            Colors = new List<Color>();

            // crack.png
            using (Bitmap crack = Properties.Resources.crack)
            {
                Point point;
                for (int i = 0; i < 4; i++)
                    for (int x = 0; x < 32; x++)
                        for (int y = 0; y < 32; y++)
                        {
                            point = new Point(x, y);
                            if (crack.GetPixel(x + i * 32, y).A > 0 && !Points.Contains(point))
                                Points.Add(point);
                        }
            }

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

            // dust.png
            using (Bitmap seed = Properties.Resources.seed)
            {
                Color color;
                for (int x = 0; x < seed.Width; x++)
                    for (int y = 0; y < seed.Height; y++)
                    {
                        color = seed.GetPixel(x, y);
                        if (color.A == 255 && !Colors.Contains(color))
                            Colors.Add(color);
                    }
            }

            // gems.png
            using (Bitmap gems = Properties.Resources.gems)
            {
                Color color;
                int k = 3; // Kinds of gems to consider, 0~5.
                for (int x = 0; x < k*32; x++)
                    for (int y = 0; y < gems.Height; y++)
                    {
                        color = gems.GetPixel(x, y);
                        if (color.A == 255 && !Colors.Contains(color))
                            Colors.Add(color);
                    }
            }

            // Fist
            Colors.AddRange(Fist.FistColors(4));
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
