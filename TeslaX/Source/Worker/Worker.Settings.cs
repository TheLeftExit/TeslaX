using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public static partial class Worker
    {
        private static readonly int BlocksAhead = 3;

        private static readonly int BlocksBehind = 1;

        private static readonly (int x1, int x2, bool SameDirection)[] Order = new (int, int, bool)[]
        {
            (0, 0, true), // First guess: same as before (center of the screen).
            (0, 0, false), // Mid-loop swap compatibility. To be commented in/out depending on situation.
            (1, 24, true), // Second guess: ahead (before or after reaching center).
            (-24, -1, true), // MLSC.
            (-24, 24, false), // MLSC.
        };

        private static readonly bool UncertainIsBlock = false;

        private static readonly Func<Point, Point, bool> PlayerSpikeCondition =
            (ov, nv) => Math.Abs(ov.X - nv.X) > 32 || (ov.X == Window.Width / 2 - 16 && ov != nv);

        private static readonly int PlayerSpikeLength = 250;

        private static readonly Func<int, int, bool> DistanceSpikeCondition =
            ((ov, nv) => Math.Abs(ov - nv) > 24 || nv == -1);

        private static readonly int DistanceSpikeLength = 150;
    }
}