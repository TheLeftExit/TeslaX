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

        private void toggle()
        {
            down = !down;
            last = (int)sw.ElapsedMilliseconds;
        }

        private int elapsed
        {
            get { return (int)sw.ElapsedMilliseconds - last; }
        }

        public bool? Update()
        {
            bool? res = null;
            if (down && elapsed > Settings.Default.PunchDown || !down && elapsed > Settings.Default.PunchUp)
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
