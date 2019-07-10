using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TeslaX.Properties;

namespace TeslaX
{
    public static class BlockInfo
    {
        // List of all available blocks.
        public static (string SingleName, string PluralName, string Code, Bitmap Source)[] Blocks = new (string, string, string, Bitmap)[]
        {
            ("Laser Grid", "Laser Grids", "lasergrid", Resources.lasergrid),
            ("Pepper Tree", "Pepper Trees", "peppertree", Resources.pepper),
            ("Pinball Bumper", "Pinball Bumpers", "bumper", Resources.pinball),
            ("Fish Tank", "Fish Tanks", "fishtank", Resources.fishtank),
            ("Sorcerer Stone", "Sorcerer Stones", "sorcerer", Resources.sorcerer),
            ("Dirt", "Dirt", "dirt", Resources.dirt),
        };

        public static (string SingleName, string PluralName, string Code, Bitmap Source) CustomBlock =
            ("Mysterious Block", "a mystery", "mystery", null);

        // Currently selected block.
        public static int BlockID;

        public static (string SingleName, string PluralName, string Code, Bitmap Source) CurrentBlock
        {
            get { return BlockID != -1 ? Blocks[BlockID] : CustomBlock; }
        }
    }
}
