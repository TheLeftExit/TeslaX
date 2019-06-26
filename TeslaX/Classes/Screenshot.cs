using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public class Screenshot
    {
        public Bitmap Image;
        public Point Location;
        public int X { get { return Location.X; } }
        public int Y { get { return Location.Y; } }
        public Size Size { get { return Image.Size; } }
        public int Width { get { return Image.Width; } }
        public int Height { get { return Image.Height; } }

        public static Point LocationOffset;

        public Screenshot(int x, int y, int w, int h)
        {
            Location = new Point(x, y);
            Image = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(Image))
            {
                g.CopyFromScreen(x + LocationOffset.X, y + LocationOffset.Y, 0, 0, Image.Size);
            }
        }

        // Screenshot is a solution-wide replacement for Bitmap, because you can't inherit from it.
        // The rest is "compatibility layer".

        public static implicit operator Bitmap(Screenshot s)
        {
            return s.Image;
        }

        public bool Contains(int x, int y)
        {
            return Image.Contains(x, y);
        }

        public bool Contains(Point p)
        {
            return Image.Contains(p);
        }

        public Color GetPixel(int x, int y)
        {
            return Image.GetPixel(x, y);
        }

        public Color GetPixel(Point p)
        {
            return Image.GetPixel(p);
        }
    }
}