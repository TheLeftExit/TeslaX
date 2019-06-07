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
            double Speed = 0;
            Stopwatch Watch = new Stopwatch();
            while (true)
            {
                SeekArea = new Rectangle(LastKnown.X + (Right ? 0 : 64), LastKnown.Y, 96, 64);
                Screenshot();
                var NewOffset = GetOffset(CurrentShot).ify();
                var NewLastKnown = GetPlayer(CurrentShot);
                // Spreading speed calculation over several lines for simplicity.
                // Assuming that player can't move faster than 32 pixels per capture.
                // Otherwise the whole offset mechanic will need a rewrite, not just this loop.
                Speed = Math.Abs(Offset.X - NewOffset.X);
                if ((Right && (NewOffset.X > Offset.X)) || (!Right && NewOffset.X < Offset.X))
                    Speed = 32 - Speed;
                Speed += Math.Abs(LastKnown.X - NewLastKnown.X);
                Speed /= Watch.ElapsedMilliseconds / 1000;
                Watch.Restart();
                // Will finish later.
            }
        }
    }
}