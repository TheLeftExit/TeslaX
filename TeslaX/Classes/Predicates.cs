using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public static class Predicates
    {
        private static readonly Point InvalidPoint = new Point(-1, -1);

        public static List<int> EligibleBetween(int a, int b, int off)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off + (a % 32 < off ? 0 : 32);
            for (int i = start; i <= b; i += 32)
                result.Add(i);
            return result;
        }

        public static Point GetOffset(this Screenshot shot, params bool[] fullscreen)
        {
            Color PlatformDark = Color.FromArgb(112, 71, 28);
            List<int> ValidY;
            if (fullscreen.Length>0)
            {
                ValidY = new List<int>();
                int step = shot.Width / 4;
                for (int i = 1; i <= 3; i++)
                    for (int y = 32; y < shot.Height - 32; y++)
                        if (PlatformDark.IsColorAt(i * step, y, shot) && PlatformDark.IsColorAt(i * step, y - 8, shot) && !ValidY.Contains(y))
                            ValidY.Add(y - 8);
            }
            else
                ValidY = new List<int> { 32 };

            foreach (int y in ValidY)
                for (int x = 0; x < shot.Width - 32; x++)
                    if (PlatformDark.IsColorAt(x + 13, y + 12, shot) && PlatformDark.IsColorAt(x + 18, y + 12, shot))
                        return new Point(x, y).Add(shot.Location).Mod(32);

            return InvalidPoint;
        }

        public static (Point Point, bool Right) GetPlayer(this Screenshot shot, params int[] o)
        {
            List<int> EligibleY;
            if (o.Length == 1)
            {
                int LocalOffsetY = o[0];
                EligibleY = EligibleBetween(0, shot.Height - 32, LocalOffsetY).AddInt(-shot.Y);
            }
            else
                EligibleY = new List<int> { 0 };

            foreach (int y in EligibleY)
                for (int x = 0 - 5; x < shot.Width - 26; x++) {
                    int res = shot.HasPlayer(x, y);
                    if (res != 0)
                        return (new Point(x, y).Add(shot.Location), res == 2);
                }

            return (InvalidPoint, false);
        }
    }
}
