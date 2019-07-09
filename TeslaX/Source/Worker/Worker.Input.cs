using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace TeslaX
{
    public static partial class Worker
    {
		private static class Input
        {
            private static Stopwatch sw;
            private static bool down;
            private static int lastchange;

            private static void toggle()
            {
                down = !down;
                lastchange = (int)sw.ElapsedMilliseconds;
                if (down)
                    (Right ? Keys.D : Keys.A).Down();
                else
                    (Right ? Keys.D : Keys.A).Up();
            }

            private static int elapsed
            {
                get { return (int)sw.ElapsedMilliseconds - lastchange; }
            }

            public static int Distance
            {
                set { newdist(value); }
            }

            public static void Initialize()
            {
                sw = Stopwatch.StartNew();
                down = false;
                lastchange = 0;
            }

            private static void newdist(int d)
            {
                bool newdown = (d) >= (/*Right ? 38 : 58*/ 32) && d != -1;

                // If we're idle, and for less than X ms, don't move yet.
                if (!down && elapsed < 150)
                    return;
                // If it's time to stop, ignore everything else and stop.
                if(!newdown)
                {
                    if(down)
                        toggle();
                    return;
                }
                // If we're moving for more than X ms, take a break.
                if(down && elapsed > 150)
                {
                    toggle();
                    return;
                }
                // If we pass all checks, then we're either idle and ready to move, or already moving.
                if (!down)
                    toggle();
            }
        }
    }
}
/* if distance<mindistance
 *     unconditional stop
 * else
 *     if 150 ms have passed
 *         unconditional stop
 * 
 * unconditional stop:
 *     give zero fucks about input for next 150 ms;
 *     if distance>mindistance
 *         start moving again;
 *         go to beginning
 * 
 * 
 * 
 */