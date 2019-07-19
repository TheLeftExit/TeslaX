using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TeslaX.Properties;

namespace TeslaX
{
    static class App
    {
        // OffsetFinder's default output.
        public static readonly Point InvalidPoint = new Point(-1, -1);

        // Find all integers between /a/ and /b/ where (x => x % 32 == off). Subtract /d/ from each.
        public static List<int> EligibleBetween(int a, int b, int off, int d = 0)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off + (a % 32 < off ? 0 : 32);
            for (int i = start; i <= b; i += 32)
                result.Add(i - d);
            return result;
        }

        // Managing preset/custom blocks.
        private static Bitmap custom = new Bitmap(32, 32);
        public static readonly List<(string Name, Bitmap Sprite, string AssetName)> Sprites = new List<(string, Bitmap, string)>()
        {
            ("Laser Grid", Resources.lasergrid, "lasergrid"),
            ("Pepper Tree", Resources.pepper, "peppertree"),
            ("Fish Tank", Resources.fishtank, "fishtank"),
            ("Sorcerer Stone", Resources.sorcerer, "sorcerer"),
            ("Chandelier", Resources.chandelier, "chandelier"),
            ("Pinball Bumper", Resources.pinball, "bumper"),
            ("Dirt", Resources.dirt, "dirt"),
            ("Custom", null, "mystery")
        };
        public static Bitmap CustomSprite {
            set { custom = value; }
            get { return custom; }
       }

        // Order in which PlayerFinder is used, with respect to last location and direction.
        public static readonly (int x1, int x2, bool SameDirection)[] PlayerFindingOrder = new (int, int, bool)[]
        {
            (0, 0, true), // First guess: same as before (center of the screen).
            (0, 0, false), // Mid-loop swap compatibility. To be commented in/out depending on situation.
            (1, 24, true), // Second guess: ahead (before or after reaching center).
            (-24, -1, true), // MLSC.
            (-24, 24, false), // MLSC.
        };
    }
}
