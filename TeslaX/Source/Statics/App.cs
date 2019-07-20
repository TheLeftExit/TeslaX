using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
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

        private static Bitmap custom = new Bitmap(32, 32);
        public static readonly List<(string Name, Func<Bitmap> Sprite, string AssetName, string DiscordName)> Sprites = new List<(string, Func<Bitmap>, string, string)>()
        {
            ("Laser Grid", () => Resources.lasergrid, "lasergrid", "Laser Grids."),
            ("Pepper Tree", () => Resources.pepper, "peppertree", "Pepper Trees."),
            ("Fish Tank", () => Resources.fishtank, "fishtank", "Fish Tanks."),
            ("Sorcerer Stone", () => Resources.sorcerer, "sorcerer", "Sorcerer Stones."),
            ("Chandelier", () => Resources.chandelier, "chandelier", "Chandeliers."),
            ("Pinball Bumper", () => Resources.pinball, "bumper", "Pinball Bumpers."),
            ("Dirt", () => Resources.dirt, "dirt", "Dirt."),
            ("Custom", () => custom, "mystery", "a mystery!")
        };
        public static Bitmap CustomSprite { set { custom = value; } }

        // Order in which PlayerFinder is used, with respect to last location and direction.
        public static readonly (int x1, int x2, bool SameDirection)[] PlayerFindingOrder = new (int, int, bool)[]
        {
            (0, 0, true), // First guess: same as before (center of the screen).
            (0, 0, false), // Mid-loop swap compatibility. To be commented in/out depending on situation.
            (1, 24, true), // Second guess: ahead (before or after reaching center).
            (-24, -1, true), // MLSC.
            (-24, 24, false), // MLSC.
        };

        private static Label statusLabel;

        public static Label StatusLabel { set { statusLabel = value; } }

        public static string Status
        {
            set
            {
                if (Application.OpenForms["MainForm"] != null)
                    statusLabel.FindForm().Invoke((MethodInvoker)delegate
                    {
                        statusLabel.Text = value;
                    });
            }
        }
    }
}
