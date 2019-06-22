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

            // Spike handling mechanism.
            Stopwatch SpikeWatch = new Stopwatch();
            bool spike = false;
            // Spike height.
            int sh = 24;
            // Spike length.
            int sl = 150;

            // Input smoothing mechanism.
            Stopwatch InputWatch = new Stopwatch();
            bool KeyDown = false;

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
                    ToCheck = EligibleBetween(LastKnown.X + 32, SeekArea.X + 128, Offset.X).AddInt(-SeekArea.X);
                    for (int x = 0; x < ToCheck.Count; x++)
                        if (CurrentShot.HasBlock(ToCheck[x], 0) != BlockState.Air)
                        {
                            NewDistance = (ToCheck[x] + SeekArea.X) - LastKnown.X - 32;
                            break;
                        }
                }
                else
                {
                    ToCheck = EligibleBetween(SeekArea.X - 32, LastKnown.X, Offset.X).AddInt(-SeekArea.X);
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

                if (spike)
                {
                    if (SpikeWatch.ElapsedMilliseconds > sl || Math.Abs(NewDistance - Distance) <= sh)
                    {
                        Distance = NewDistance;
                        SpikeWatch.Stop();
                        spike = false;
                    }
                }
                else
                if (Math.Abs(NewDistance - Distance) > sh)
                {
                    SpikeWatch.Restart();
                    spike = true;
                }
                else
                    Distance = NewDistance;

                bool NewKeyDown = Distance > 38;//(Right ? 38 : 0);

                if (InputWatch.ElapsedMilliseconds > 150 && NewKeyDown != KeyDown)
                {
                    InputWatch.Restart();
                    KeyDown = NewKeyDown;
                    Window.Send(Right ? KeyCode.Right : KeyCode.Left, KeyDown);
                    
                }

                // First iteration only.
                if (!InputWatch.IsRunning)
                    InputWatch.Start();

                Log((KeyDown ? "+" : "-") + Distance.ToString() + (spike ? "S" : ""));
            }

            Restore();
        }
    }
}