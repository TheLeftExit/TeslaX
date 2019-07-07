using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    // Unused.
    public static class Cracks
    {
        public static Bitmap[] Clean;

        public static void Load()
        {
            using (Bitmap crack = Properties.Resources.crack)
                // For each of 4 crack sprites
                for(int i = 0; i < 4; i++)
                {
                    // for each pixel
                    for (int x = 0; x < 32; x++)
                        for (int y = 0; y < 32; y++)
                        {
                            // get the color
                            Color now = crack.GetPixel(i * 32 + x, y);
                            // if it's empty, nevermind
                            if (now.A == 0)
                                continue;
                            // check if any sprites "before" it have the same color (if it's unique)
                            bool unique = true;
                            for (int j = 0; j < i; j++)
                                if (now == crack.GetPixel(j * 32 + x, y))
                                {
                                    unique = false;
                                    break;
                                }
                            // also if unique, inject it into sprite
                            if (unique)
                            {
                                foreach (var p in Block.Sprite.Where(p => p.Point == new Point(x, y))) {
                                    HashSet<Color> colors = new HashSet<Color>();
                                    foreach (var c in p.Colors)
                                        colors.Add(c.UnderCrack(now.R));
                                    foreach (var c in colors)
                                        p.Colors.Add(c);
                                }
                            }
                        }
                }
        }
    }
}
