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
            while (Busy)
            {
                SeekArea = new Rectangle(LastKnown.X + (Right ? 0 : -96), LastKnown.Y, 96, 128);
                Screenshot();
                // II. Find offset.
                tmpoint = GetOffset(CurrentShot).ify();
                if (tmpoint != InvalidPoint)
                    Offset = tmpoint;
                else
                {
                    Log("Offset?");
                    continue;
                }
                //MessageBox.Show(Offset.ToString());

                tmpoint = GetPlayer(CurrentShot);
                if (tmpoint != InvalidPoint)
                    LastKnown = tmpoint;
                else
                {
                    Log("Player?");
                    continue;
                }

                List<int> ToCheck;
                NewDistance = -1;
                if (Right)
                {
                    ToCheck = EligibleBetween(LastKnown.X, SeekArea.X + 128, Offset.X).AddInt(-SeekArea.X);
                    for (int x = 0; x < ToCheck.Count; x++)
                        if (CurrentShot.HasBlock(ToCheck[x], 0) != BlockState.Air)
                        {
                            NewDistance = (ToCheck[x] + SeekArea.X) - LastKnown.X - 32;
                            break;
                        }
                }
                else
                {
                    ToCheck = EligibleBetween(SeekArea.X - 32, LastKnown.X + 32, Offset.X).AddInt(-SeekArea.X);
                    for(int x = ToCheck.Count - 1; x>=0; x--)
                        if(CurrentShot.HasBlock(ToCheck[x], 0) != BlockState.Air)
                        {
                            NewDistance = LastKnown.X - (ToCheck[x] + SeekArea.X) - 32;
                            break;
                        }
                }

                if (NewDistance == -1) {
                    Log("It's -1");
                    continue;
                }

                if (Distance - NewDistance > 24) {
                    Log("Fast drop");
                    continue;
                }
                else
                    Distance = NewDistance;

                Log(Distance.ToString());
            }

            Restore();
        }
    }
}