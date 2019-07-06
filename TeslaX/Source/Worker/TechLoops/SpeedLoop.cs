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
        private static void SpeedLoop()
        {
            Distance = new Smooth<int>(Settings.DistanceSpikeLength, Settings.DistanceSpikeCondition);
            Distance.Value = -1;

            DebugForm debugForm = new DebugForm();
            StringBuilder debugInfo = new StringBuilder();
            new Thread(() => { debugForm.ShowDialog(); }).Start();

            var stamps = new List<(long Time, int Change)>();
            var vals = new List<int>();

            Stopwatch sw = Stopwatch.StartNew();

            Busy = true;
            while (Busy)
            {
                using (shot = new Screenshot(
                    LastKnown.Value.X + (Right ? -Settings.BlocksBehind * 32 : -Settings.BlocksAhead * 32),
                    LastKnown.Value.Y,
                    (Settings.BlocksAhead + Settings.BlocksBehind + 1) * 32,
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

                    // Calculating distance traveled.
                    (long Time, int Change) entry;
                    entry.Time = sw.ElapsedMilliseconds;
                    entry.Change = (oldx + 32 - newx) % 32;

                    stamps.Add(entry);
                    
                    // Removing entries older than 1 second.
                    while (true)
                        if (sw.ElapsedMilliseconds - stamps[0].Time > 1000)
                            stamps.RemoveAt(0);
                        else
                            break;

                    // Getting pixels per second.
                    int spd = stamps.Sum(x => x.Change);

                    // If I hold E, add it to "average" pool.
                    if (Keys.E.IsDown())
                        vals.Add(spd);

                    // Updating according to this thing.
                    // We should see total amount of pixels traveled last second.
                    debugInfo.AppendLine(entry.Change.ToString());
                    debugInfo.AppendLine(spd.ToString());
                    if (vals.Count > 0)
                        debugInfo.AppendLine(vals.Average().ToString());
                    debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                }
            }
        }
    }
}