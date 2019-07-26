﻿using System.Collections.Generic;
using System.Drawing;
using TheLeftExit.TeslaX.Entities;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX.Helpers
{
    internal class OffsetFinder
    {
        public Point GetOffset(Screenshot shot, bool fullscreen = false)
        {
            Color PlatformDark = Color.FromArgb(112, 71, 28);
            List<int> ValidY;
            if (fullscreen)
            {
                ValidY = new List<int>();
                int step = shot.Width / 4;
                for (int i = 1; i <= 3; i++)
                    for (int y = 0 + 8; y < shot.Height - 12; y++)
                        if (PlatformDark.IsColorAt(i * step, y, shot) && PlatformDark.IsColorAt(i * step, y - 8, shot) && !ValidY.Contains(y))
                            ValidY.Add(y - 8);
            }
            else
                ValidY = new List<int> { 32 };

            foreach (int y in ValidY)
                for (int x = 0; x < shot.Width - 32; x++)
                    if (PlatformDark.IsColorAt(x + 13, y + 12, shot) && PlatformDark.IsColorAt(x + 18, y + 12, shot))
                        return new Point(x, y).Add(shot.Location).Mod(32);

            return App.InvalidPoint;
        }
    }
}