using TheLeftExit.TeslaX.Entities;

namespace TheLeftExit.TeslaX.Helpers
{
    internal class PunchManager : TimedManager
    {
        // Experimental: quadratic distribution. Adds delay but greatly improves stealth.
        private RandomNumber punchUp = new RandomNumber(0.4, 0.9, x => x * x * 1000);
        private RandomNumber punchDown = new RandomNumber(0.9, 3.2, x => x * x * 1000);

        public bool? Update()
        {
            bool? res = null;
            if (down && elapsed > punchDown || !down && elapsed > punchUp)
            {
                toggle();
                if (down)
                    punchDown.Next();
                else
                    punchUp.Next();
                res = down;
            }
            return res;
        }
    }
}
