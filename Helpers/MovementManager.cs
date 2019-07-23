using System.Diagnostics;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX.Helpers
{
    public class MovementManager : TimedManager
    {
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
    }
}