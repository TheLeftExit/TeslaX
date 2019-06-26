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
        // Used to detect Block,
        public static List<(Point Point, Color Color)> Sprite;

        // List of all points.
        // If flippable, only points opaque on both rotations are listed.
        // Used to detect Air.
        public static List<Point> Points;

        public static bool Flippable = true;

        // Include other images and load them here instead for 100% extensibility!
        // To be run after Ignorable.Load.
        public static void Load()
        {
            Sprite = new List<(Point Point, Color Color)>();
            Points = new List<Point>();

            using (Bitmap block = Properties.Resources.lasergrid)
            {
                Color color;
                Point point;
                for(int x = 0; x<32; x++)
                    for(int y = 0; y <32; y++)
                    {
                        color = block.GetPixel(x, y);
                        point = new Point(x, y);
                        if (color.A == 255 && !Ignorable.Colors.Contains(color) && !Ignorable.Points.Contains(point))
                        {
                            if (!Flippable)
                            {
                                Sprite.Add((point, color));
                                Points.Add(point);
                            }
                            else
                            {
                                if (!Sprite.Contains((point, color))){
                                    Sprite.Add((point, color));
                                    Sprite.Add((point.Flip(), color));
                                }
                                if (block.GetPixel(point.Flip()).A == 255)
                                    Points.Add(point);
                            }
                        }
                    }
            }
            using(Bitmap cracks = Properties.Resources.crack)
            {

            }
        }

        // Bruteforcing the result. Expect drops in performance with more opaque blocks (like Sorcerer Stone).
        public static BlockState HasBlock(this Screenshot shot, int x, int y)
        {
            foreach(var p in Sprite)
                if (shot.Contains(p.Point.Add(x,y)))
                    if (p.Color.IsColorAt(p.Point.Add(x,y), shot))
                        return BlockState.Block;
            foreach (var p in Points)
                if (shot.Contains(p.Add(x,y)))
                    if (!shot.GetPixel(p.Add(x,y)).IsIgnored())
                        return BlockState.Air;
            return BlockState.Uncertain;
        }
    }
}
