using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TeslaX
{
    // Unused.
    public static class Physics
    {
        // Speed, pixels per second.
        public static int Speed = 0;

        // Acceleration, or "speed increase per second if button is held and speed limit is removed".
        // Somewhere around 500-700. Real pain to calculate.
        private static readonly int a = 600;

        // Speed limit.
        // Exact!
        private static readonly int sl = 240;
    }
}
