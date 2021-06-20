using TheLeftExit.TeslaX;

namespace TheLeftExit.TeslaX
{
    public class MovementManager : TimedManager
    {
        private int minStop;
        private int maxMove;

        public MovementManager(int tMinStop, int tMaxMove)
        {
            minStop = tMinStop;
            maxMove = tMaxMove;
        }

        private void newdist(bool newdown)
        {
            // If we're idle, and for less than X ms, don't move yet.
            if (!down && elapsed < minStop)
                return;
            // If it's time to stop, ignore everything else and stop.
            if (!newdown)
            {
                if (down)
                    toggle();
                return;
            }
            // If we're moving for more than X ms, take a break.
            if (down && elapsed > maxMove)
            {
                toggle();
                return;
            }
            // If we pass all checks, then we're either idle and ready to move, or already moving.
            if (!down)
                toggle();
        }

        public bool? Update(bool newdown)
        {
            bool last = down;
            newdist(newdown);
            if (down == last)
                return null;
            return down;
        }
    }
}