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
        private static void GainLoop()
        {
            Distance = new Smooth<int>(TechSettings.DistanceSpikeLength, TechSettings.DistanceSpikeCondition);
            Distance.Value = -1;

            DebugForm debugForm = new DebugForm();
            
            StringBuilder debugInfo = new StringBuilder();
            new Thread(() => { debugForm.ShowDialog(); }).Start();

            var stamps = new List<(long Time, int Change)>();

            var allstamps = new List<(long Time, int Change)>();

            bool LastWasUp = true;

            Stopwatch sw = Stopwatch.StartNew();

            Busy = true;
            while (Busy)
            {
                using (shot = new Screenshot(
                    LastKnown.Value.X + (Right ? -TechSettings.BlocksBehind * 32 : -TechSettings.BlocksAhead * 32),
                    LastKnown.Value.Y,
                    (TechSettings.BlocksAhead + TechSettings.BlocksBehind + 1) * 32,
                    64))
                {
                    // Clearing.
                    debugInfo.Clear();

                    // Getting offset difference.
                    int oldx = Offset.X;
                    shot.SetOffset();
                    int newx = Offset.X;

                    // Getting player, just to know where to draw the form.
                    shot.SetPlayer();

                    // If we're not moving right, clear the buffer and don't bother.
                    if (Keys.D.IsDown())
                    {
                        /*if(!LastWasUp)
                        {
                            int sofara = 0;
                            // For each checked value "i", get distance traveled before "i" ms.
                            for (int i = 0; i < stamps.Count; i++)
                            {
                                sofara += stamps[i].Change;
                                allstamps.Add((stamps[i].Time, sofara));
                            }
                            ;
                        }*/
                        allstamps = stamps;
                        stamps.Clear();
                        LastWasUp = false;
                        continue;
                    }

                    if (!LastWasUp)
                    {
                        sw.Restart();
                        LastWasUp = true;
                    }

                    // Calculating distance traveled.
                    if (sw.ElapsedMilliseconds < 400)
                    {
                        (long Time, int Change) entry;
                        entry.Time = sw.ElapsedMilliseconds;
                        entry.Change = (oldx + 32 - newx) % 32;
                        if (entry.Change != 0)
                            stamps.Add(entry);
                    }

                    int sofar = 0;
                    // For each checked value "i", get distance traveled before "i" ms.
                    for(int i = 0; i<stamps.Count && stamps[i].Time < 400; i++)
                    {
                        sofar += stamps[i].Change;
                        debugInfo.AppendLine(stamps[i].Time.ToString() + ": " + sofar.ToString());
                    }

                    debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                }
            }
            debugForm.BusyCheck();

            StringBuilder log = new StringBuilder();
            int sum = 0;

            // Version 1: average

            foreach (var x in allstamps)
            {
                sum += x.Change;
                log.Append(((double)x.Time/1000).ToString() + '\t' + sum.ToString() + '\t' + x.Change.ToString() + Environment.NewLine);
            }

            // Open the last value in Notepad so that it can be Ctrl+V'd to Excel.
            var tmpfile = System.IO.Path.GetTempPath()+@"/gainloop.txt";

            System.IO.File.WriteAllText(tmpfile, log.ToString());
            Process.Start(tmpfile);
        }
    }
}