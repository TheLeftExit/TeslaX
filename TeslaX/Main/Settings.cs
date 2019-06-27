using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaX
{
    public static class Settings
    {
        // Whether debug information should be displayed.
        public static bool Debug = true;

        // Whether input should be simulated.
        public static bool SimulateInput = false;

        public static int BlocksAhead = 3;

        public static int SkinColor = 3;

        // Whether "Uncertain" block should be functionally treated as Block or Air.
        public static bool UncertainIsBlock = false;

        // Largest gem to be considered. Might affect performance, not sure.
        // 0 - no gems
        // 1 - yellow (1)
        // 2 - blue   (5)
        // 3 - red    (10)
        // 4 - green  (50)
        // 5 - purple (100)
        public static int BiggestGem = 3;
    }
}
