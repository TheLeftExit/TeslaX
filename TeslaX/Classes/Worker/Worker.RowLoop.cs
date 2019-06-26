using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;
using System.Diagnostics;

namespace TeslaX
{
    public static partial class Worker
    {
        public static List<int> EligibleBetween(int a, int b, int off)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off + (a % 32 < off ? 0 : 32);
            for (int i = start; i <= b; i += 32)
                result.Add(i);
            return result;
        }

        public static void RowLoop()
        {
            Point tmpoint;
            Screenshot shot;

            int Distance = -1;
            int NewDistance;

            // Blocks in front of character to check.
            int range = Settings.BlocksAhead;

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

            // Debug info.
            DebugForm debugForm = new DebugForm();
            StringBuilder debugInfo = new StringBuilder();
            if (Settings.Debug)
            {
                new Thread(() =>
                {
                    debugForm.ShowDialog();
                }).Start();
            }

            Busy = true;
            while (Busy)
            {
                shot = new Screenshot(LastKnown.X + (Right ? 0 : -range * 32), LastKnown.Y, (range + 1) * 32, 64);

                if (Settings.Debug)
                {
                    debugInfo.Clear();
                }

                tmpoint = shot.GetOffset();
                if (tmpoint != InvalidPoint)
                    Offset = tmpoint;
                else
                {
                    if (Settings.Debug)
                    {
                        debugInfo.AppendLine("Offset:   N/A");
                        debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                    }
                    continue;
                }
                if (Settings.Debug)
                    debugInfo.AppendLine("Offset: " + Offset.ToString());

                tmpoint = shot.GetPlayer(Right);
                if (tmpoint != InvalidPoint)
                    LastKnown = tmpoint;
                else
                {
                    if (Settings.Debug)
                    {
                        debugInfo.AppendLine("Player: N/A");
                        debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                    }
                    continue;
                }
                if (Settings.Debug)
                    debugInfo.AppendLine("Player: " + LastKnown.ToString());

                List<int> ToCheck;
                NewDistance = -1;
                if (Right)
                {
                    ToCheck = EligibleBetween(LastKnown.X + 32, LastKnown.X + 32 + range * 32, Offset.X).AddInt(-shot.X);
                    for (int x = 0; x < ToCheck.Count; x++)
                    {
                        BlockState next = shot.HasBlock(ToCheck[x], 0);
                        if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.UncertainIsBlock))
                        {
                            NewDistance = (ToCheck[x] + shot.X) - LastKnown.X - 32;
                            break;
                        }
                    }
                }
                else
                {
                    ToCheck = EligibleBetween(LastKnown.X - 32 - range * 32, LastKnown.X - 32, Offset.X).AddInt(-shot.X);
                    for (int x = ToCheck.Count - 1; x >= 0; x--)
                    {
                        BlockState next = shot.HasBlock(ToCheck[x], 0);
                        if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.UncertainIsBlock))
                        {
                            NewDistance = LastKnown.X - (ToCheck[x] + shot.X) - 32;
                            break;
                        }
                    }
                }

                if (NewDistance == -1) {
                    if (Settings.Debug)
                    {
                        debugInfo.AppendLine("NewDistance: N/A");
                        debugInfo.AppendLine("Distance: " + Distance.ToString());
                        debugInfo.AppendLine("KeyDown: " + KeyDown.ToString());
                        debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                    }
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

                bool NewKeyDown = Distance > 26; //(Right ? 38 : 0);

                if (Settings.Debug)
                {
                    debugInfo.AppendLine("NewDistance: " + NewDistance.ToString());
                    debugInfo.AppendLine("Distance: " + Distance.ToString());
                    debugInfo.AppendLine("KeyDown: " + KeyDown.ToString());
                }

                if (InputWatch.ElapsedMilliseconds > 150 && NewKeyDown != KeyDown)
                {
                    InputWatch.Restart();
                    KeyDown = NewKeyDown;
                    if(Settings.SimulateInput)
                        Key.Send(Right ? KeyCode.Right : KeyCode.Left, KeyDown);
                }

                // First iteration only.
                if (!InputWatch.IsRunning)
                    InputWatch.Start();

                if (Settings.Debug)
                {
                    debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                }
            }
        }
    }
}