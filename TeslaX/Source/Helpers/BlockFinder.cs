using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    enum BlockState
    {
        Air = 0,
        Block = 1,
        Uncertain = 2
    }

    class BlockFinder
    {
        // All potential points.
        private HashSet<(Point Point, HashSet<Color> Colors)> sprite;

        // Guaranteed points.
        private HashSet<Point> points;

        // Colors of collateral sprites.
        private HashSet<Color> ignoredcolors;

        private bool ignored(Color color)
        {
            foreach (var c in ignoredcolors)
                if (color.Is(c))
                    return true;
            return false;
        }

        // Initializes all three HashSets, enabling HasBlock.
        public BlockFinder(Bitmap spritesheet, params Bitmap[] ignorable)
        {
            Color color;
            Point point;

            // Initializing and filling ignoredcolors.
            ignoredcolors = new HashSet<Color>();

            foreach (Bitmap b in ignorable)
                for (int x = 0; x < b.Width; x++)
                    for (int y = 0; y < b.Height; y++)
                    {
                        color = b.GetPixel(x, y);
                        if (color.A == 255 && !ignoredcolors.Contains(color))
                            ignoredcolors.Add(color);
                    }

            // Initializing and filling sprite and points.
            sprite = new HashSet<(Point Point, HashSet<Color> Colors)>();
            points = new HashSet<Point>();

            int frames = spritesheet.Width / 32;

            for (int x = 0; x < 32; x++)
                for (int y = 0; y < 32; y++)
                {
                    point = new Point(x, y);
                    bool opaque = true;
                    HashSet<Color> thispixel = new HashSet<Color>();

                    for (int i = 0; i < frames; i++)
                    {
                        color = spritesheet.GetPixel(x + i * 32, y);
                        if (color.A == 255 && !ignored(color))
                            thispixel.Add(color);

                        if (color.A < 255)
                            opaque = false;
                    }

                    if (thispixel.Count > 0)
                        sprite.Add((point, thispixel));

                    if (opaque)
                        points.Add(point);
                }

            // Appending sprite with cracks.
            Bitmap crack = Properties.Resources.crack;

            for (int i = 0; i < 4; i++)
            {
                for (int x = 0; x < 32; x++)
                    for (int y = 0; y < 32; y++)
                    {
                        Color now = crack.GetPixel(i * 32 + x, y);

                        if (now.A == 0)
                            continue;

                        bool unique = true;
                        for (int j = 0; j < i; j++)
                            if (now == crack.GetPixel(j * 32 + x, y))
                            {
                                unique = false;
                                break;
                            }

                        if (unique)
                        {
                            foreach (var p in sprite.Where(p => p.Point == new Point(x, y)))
                            {
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

        public BlockState HasBlock(Screenshot shot, int x, int y)
        {
            foreach (var p in sprite)
                if (shot.Contains(p.Point.Add(x, y)))
                    foreach (var c in p.Colors)
                        if (c.IsColorAt(p.Point.Add(x, y), shot))
                            return BlockState.Block;

            foreach (var p in points)
                if (shot.Contains(p.Add(x, y)))
                    if (!ignored(shot.GetPixel(p.Add(x, y))))
                        return BlockState.Air;

            return BlockState.Uncertain;
        }
    }
}
