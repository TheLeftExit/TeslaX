using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;
using System.Diagnostics;

namespace TeslaX
{
    public static partial class Worker
    {
        public static void RowLoop()
        {
            // Apparently, this eats up tons of RAM.
            // Capturing the entire screen makes it into a saw pattern, peaking at almost 1 GB.
            // This is much less, but it may still reach half of that.
            while (true)
            {
                // Step 1: Get offset. Decided to make this separate, instead of working with a single 128x64 shot.
                // Temporary dirty solution: take middle 64x64 of the window.
                SeekArea = new Rectangle(WindowPos.Width / 2 - 32, WindowPos.Height / 2 - 32, 64, 64);
                Screenshot();
                Offset = GetOffset(CurrentShot).ify();
                // Step 2: Get distance to the next block (between edges of character's 32x32 and block's 32x32).
                // I. Take a 128x32 picture.
                SeekArea = new Rectangle(LastKnown.X + (Right ? 0 : 64), LastKnown.Y, 92,64);
                Screenshot();
                // II. Find player.
                var NewLastKnown = GetPlayer(CurrentShot);
                // III. Cycle through blocks.
                // Later.
            }
        }
    }
}