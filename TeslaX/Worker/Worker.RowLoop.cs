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
            Point tmpoint;
            int Distance = -1;
            int NewDistance;
            // Apparently, this eats up tons of RAM.
            // Capturing the entire screen makes it into a saw pattern, peaking at almost 1 GB.
            // This is much less, but it may still reach half of that.
            while (Busy)
            {
                // Get distance to the next block (between edges of character's 32x32 and block's 32x32).
                // I. Take a 128x128 picture.
                SeekArea = new Rectangle(LastKnown.X + (Right ? 0 : -96), LastKnown.Y, 128, 128);
                Screenshot();
                // II. Find offset.
                tmpoint = GetOffset(CurrentShot).ify();
                if (tmpoint != InvalidPoint)
                    Offset = tmpoint;
                else
                    continue;
                // III. Find player.
                tmpoint = GetPlayer(CurrentShot);
                if (tmpoint != InvalidPoint)
                    LastKnown = tmpoint;
                else
                    continue;
                // IV. Cycle through blocks.
                List<int> ToCheck;
                NewDistance = -1;
                if (Right)
                {
                    ToCheck = EligibleBetween(LastKnown.X, SeekArea.X + 128, Offset.X).AddInt(-SeekArea.X);
                    for (int x = 0; x < ToCheck.Count; x++)
                        if (CurrentShot.HasBlockAt(ToCheck[x]) != Air)
                        {
                            NewDistance = (ToCheck[x] + SeekArea.X) - LastKnown.X - 32;
                            break;
                        }
                }
                else
                {
                    ToCheck = EligibleBetween(SeekArea.X - 32, LastKnown.X + 32, Offset.X).AddInt(-SeekArea.X);
                    for(int x = ToCheck.Count - 1; x>=0; x--)
                        if(CurrentShot.HasBlockAt(ToCheck[x]) != Air)
                        {
                            NewDistance = LastKnown.X - (ToCheck[x] + SeekArea.X) - 32;
                            break;
                        }
                }

                // If there are no blocks in sight, something's not right. Ignore the iteration.
                if (NewDistance == -1)
                    continue;

                // Explosions can make "Air" turn into "Uncertain", decreasing Distance by ~32.
                // This is redundant, so we catch any rapid Distance decreases and ignore them.
                // Otherwise, Distance successfully calculated.
                if (Distance - NewDistance > 24)
                {
                    NewDistance = Distance;
                    continue;
                }
                else
                    Distance = NewDistance;

                // Debug: show calculated distance in window title.
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.Text = Distance.ToString();
                });
            }

            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.Restore();
            });
        }
    }
}