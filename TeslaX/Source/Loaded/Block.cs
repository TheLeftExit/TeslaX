using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public enum BlockState
    {
        Air = 0,
        Block = 1,
        Uncertain = 2
    }

    public static class Block
    {
        // List of all points on a 32x32 grid that are fully opaque and don't share color with Ignorables.
        // If flippable, one point may have two colors.
        // Used to detect Block.
        public static HashSet<(Point Point, HashSet<Color> Colors)> Sprite;

        // List of all points.
        // If flippable, only points opaque on both rotations are listed.
        // Used to detect Air.
        public static HashSet<Point> Points;

        // Include other images and load them here instead for 100% extensibility!
        // To be run after Ignorable.Load.
        public static void Load()
        {
            Sprite = new HashSet<(Point Point, HashSet<Color> Colors)>();
            Points = new HashSet<Point>();

            Bitmap block = BlockInfo.CurrentBlock.Source;

            // Dealing with invalid spritesheets the hard way. 
            if (block.Width % 32 > 0)
                throw new Exception("Invalid spritesheet supplied: width not a multiple of 32.");

            int frames = block.Width / 32;

            Color color;
            Point point;
            for (int x = 0; x < 32; x++)
                for (int y = 0; y < 32; y++)
                {
                    point = new Point(x, y);
                    bool opaque = true;
                    HashSet<Color> thispixel = new HashSet<Color>();

                    for (int i = 0; i < frames; i++)
                    {
                        color = block.GetPixel(x, y);
                        if (color.A == 255 && !Ignorable.Colors.Contains(color))
                            thispixel.Add(color);

                        if (color.A < 255)
                            opaque = false;
                    }

                    if (thispixel.Count > 0)
                        Sprite.Add((point, thispixel));

                    if (opaque)
                        Points.Add(point);
                }
        }

        public static BlockState HasBlock(this Screenshot shot, int x, int y)
        {
            foreach (var p in Sprite)
                if (shot.Contains(p.Point.Add(x, y)))
                    foreach (var c in p.Colors)
                        if (c.IsColorAt(p.Point.Add(x, y), shot))
                            return BlockState.Block;

            foreach (var p in Points)
                if (shot.Contains(p.Add(x, y)))
                    if (!shot.GetPixel(p.Add(x, y)).IsIgnored())
                        return BlockState.Air;

            return BlockState.Uncertain;
        }
    }
}
