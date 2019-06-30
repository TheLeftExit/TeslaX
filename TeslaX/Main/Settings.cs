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
        public static bool SimulateInput = true;

        public static int BlocksAhead = 3; // 0 = crash.
        public static int BlocksBehind = 1; // 0 = crash.

        // Possible deviation of RGB values when comparing.
        public static int Distortion = 2;

        // Order of locations (horizontal) to check for player, relative to last known position.
        // For each command, every location from x1 to x2 is checked, looking for player facing same or different (from current) direction.
        // Main tool in calibrating LastKnown for accuracy.
        public static List<(int x1, int x2, bool SameDirection)> Order = new List<(int, int, bool)>
        {
            (0, 0, true), // First guess: same as before (center of the screen).
            (0, 0, false), // Mid-loop swap compatibility. To be commented in/out depending on situation.
            (1, 24, true), // Second guess: ahead (before or after reaching center).
            (-24, -1, true), // MLSC.
            (-24, 24, false), // MLSC.
        };

        // Maximum pixels-per-frame character speed. Best to leave somewhere high due to FPS drops.
        public static int MaxSpeed = 32;
        
        // Skin color, in order on color picking panel.
        public static int SkinColor = 3;

        // Whether "Uncertain" block should be functionally treated as Block instead of Air.
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
