using System;
using System.Drawing;
using TheLeftExit.TeslaX.Entities;
using TheLeftExit.TeslaX.Properties;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX.Helpers
{
    public class PlayerFinder : IDisposable
    {
        // Player's head.
        public Bitmap Head;

        public PlayerFinder(int skincolor)
        {
            Head = new Bitmap(Resources.head);
            for (int x = 0; x < Head.Width; x++)
                for (int y = 0; y < Head.Height; y++)
                    if (Head.GetPixel(x, y).A == 255)
                        Head.SetPixel(x, y, Game.SkinColors[skincolor].Dim(Head.GetPixel(x, y).R));
        }

        public void Dispose()
        {
            Head.Dispose();
        }

        public bool HasPlayer(Screenshot shot, int x, int y, bool right)
        {
            Color PlayerDark = Head.GetPixel(5, 1);
            Color PlayerLight = Head.GetPixel(6, 1);
            for (int ya = 1; ya <= 14; ya++)
                if (PlayerDark.IsColorAt(x + (right ? 5 : 26), y + ya, shot))
                {
                    for (int yb = 1; yb <= 14; yb++)
                        if (PlayerLight.IsColorAt(x + (right ? 6 : 25), y + yb, shot))
                            return true;
                    break;
                }
            return false;
        }
    }
}
