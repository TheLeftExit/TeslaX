using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public static class Cracks
    {
        public static Bitmap[] Clean;

        public static void Load()
        {
            Clean = new Bitmap[4];

            var NewSprite = new HashSet<(Point Point, Color Color)>();

            using (Bitmap crack = Properties.Resources.crack)
                // For each of 4 crack sprites
                for(int i = 0; i < 4; i++)
                {
                    // initialize it
                    Clean[i] = new Bitmap(32, 32);
                    // for each pixel
                    for (int x = 0; x < 32; x++)
                        for (int y = 0; y < 32; y++)
                        {
                            // get the color
                            Color now = crack.GetPixel(i * 32 + x, y);
                            // if it's empty, nevermind
                            if (now.A == 0)
                            {
                                Clean[i].SetPixel(x, y, Color.Transparent);
                                continue;
                            }
                            // check if any sprites "before" it have the same color (if it's unique)
                            bool unique = true;
                            for (int j = 0; j < i; j++)
                                if (now == crack.GetPixel(j * 32 + x, y))
                                {
                                    unique = false;
                                    break;
                                }
                            // if unique, add it, otherwise nevermind
                            Clean[i].SetPixel(x, y, unique ? now : Color.Transparent);
                            // also if unique, add it to block predicate
                            if (unique)
                            {
                                foreach (var p in Block.Sprite)
                                    if (p.Point == new Point(x, y))
                                    {
                                        NewSprite.Add((new Point(x, y), p.Color.UnderCrack(now.R)));
                                    }
                            }
                        }
                }
            Block.Sprite.UnionWith(NewSprite);
        }

        /// <summary>
        /// Checks a location (presumably with a block) for cracks.<br/>
        /// Returns 0..3 for appropriate Crack sprite, or -1 if there are none.
        /// </summary>
        public static int HasCracks(this Screenshot shot, int x, int y)
        {
            // Stil doesn't work... Will get back to it.
            for (int xa = 0; xa < 32; xa++)
                for (int ya = 0; ya < 32; ya++)
                    for (int i = 0; i < 4; i++)
                    {
                        Point here = new Point(x, y);
                        if (!shot.Contains(here.Add(xa, ya)) || !Block.Points.Contains(here))
                            continue;
                        Color clean = Clean[i].GetPixel(here);
                        if (clean.A == 0)
                            continue;
                        foreach (var p in Block.Sprite)
                            if (p.Point == here)
                                if (p.Color.UnderCrack(clean.R).IsColorAt(here.Add(xa, ya), shot))
                                    return i;
                    }
            return -1;
        }
    }
}
