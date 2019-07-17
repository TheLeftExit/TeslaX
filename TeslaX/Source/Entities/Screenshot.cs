using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public class Screenshot : IDisposable
    {
        public Bitmap Picture;
        public Point Location;
        public int X { get { return Location.X; } }
        public int Y { get { return Location.Y; } }
        public Size Size { get { return Picture.Size; } }
        public int Width { get { return Picture.Width; } }
        public int Height { get { return Picture.Height; } }

        public Screenshot(int screenX, int screenY, int width, int height)
        {
            Picture = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(Picture))
                g.CopyFromScreen(screenX, screenY, 0, 0, new Size(width, height));
        }

        public Screenshot() { }

        // Screenshot is a solution-wide replacement for Bitmap, because you can't inherit from it.
        // The rest is "compatibility layer", extensive enough to allow Image to be private.

        public static implicit operator Bitmap(Screenshot s)
        {
            return s.Picture;
        }

        public bool Contains(int x, int y)
        {
            return Picture.Contains(x, y);
        }

        public bool Contains(Point p)
        {
            return Picture.Contains(p);
        }

        public Color GetPixel(int x, int y)
        {
            return Picture.GetPixel(x, y);
        }

        public Color GetPixel(Point p)
        {
            return Picture.GetPixel(p);
        }

        public void Dispose()
        {
            Picture.Dispose();
        }
    }
}