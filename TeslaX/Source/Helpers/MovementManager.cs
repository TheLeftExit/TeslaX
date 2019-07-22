using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using TeslaX.Properties;

namespace TeslaX
{
    public class MovementManager
    {
        private Stopwatch sw;
        private bool down;
        private int last;

        private void toggle()
        {
            down = !down;
            last = (int)sw.ElapsedMilliseconds;
        }

        private int elapsed
        {
            get { return (int)sw.ElapsedMilliseconds - last; }
        }

        private void newdist(int d, bool right)
        {
            bool newdown = (d) >= (right ? UserSettings.Current.DistanceRight : UserSettings.Current.DistanceLeft) && d > -1;

            // If we're idle, and for less than X ms, don't move yet.
            if (!down && elapsed < UserSettings.Current.MinStop)
                return;
            // If it's time to stop, ignore everything else and stop.
            if (!newdown)
            {
                if (down)
                    toggle();
                return;
            }
            // If we're moving for more than X ms, take a break.
            if (down && elapsed > UserSettings.Current.MaxMove)
            {
                toggle();
                return;
            }
            // If we pass all checks, then we're either idle and ready to move, or already moving.
            if (!down)
                toggle();
        }

        public bool? Update(int value, bool right)
        {
            bool last = down;
            newdist(value, right);
            if (down == last)
                return null;
            return down;
        }

        public MovementManager()
        {
            sw = Stopwatch.StartNew();
            down = false;
            last = 0;
        }
    }
}