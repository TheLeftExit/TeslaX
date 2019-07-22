using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TeslaX.Properties;

namespace TeslaX
{
    class PunchManager
    {
        private Stopwatch sw;
        private bool down;
        private int last;

        // Experimental: quadratic distribution. Adds delay but greatly improves stealth.
        private RandomNumber punchUp = new RandomNumber(0.4, 0.9, x => x * x * 1000);
        private RandomNumber punchDown = new RandomNumber(0.9, 3.2, x => x * x * 1000);

        private void toggle()
        {
            down = !down;
            if (down)
                punchDown.Next();
            else
                punchUp.Next();
            last = (int)sw.ElapsedMilliseconds;
        }

        private int elapsed
        {
            get { return (int)sw.ElapsedMilliseconds - last; }
        }

        public bool? Update()
        {
            bool? res = null;
            if (down && elapsed > punchDown || !down && elapsed > punchUp)
            {
                toggle();
                res = down;
            }
            return res;
        }

        public PunchManager()
        {
            sw = Stopwatch.StartNew();
            down = false;
            last = 0;
        }
    }
}
