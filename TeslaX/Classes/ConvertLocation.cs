using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaX
{
    /// <summary>
    /// Point of reference.
    /// </summary>
    public enum PoR
    {
        /// <summary>
        /// Relative to screen.
        /// </summary>
        Screen = 0,
        /// <summary>
        /// Relative to window.
        /// </summary>
        Window = 1,
        /// <summary>
        /// Relative to Worker.shot.
        /// </summary>
        Screenshot = 2,
        /// <summary>
        /// Relative to player's face, including direction (negative if behind).
        /// </summary>
        Face = 3
    }
    public static class ConvertLocation
    {
        public static int ConvertX(this int val, PoR from, PoR to)
        {
            int screen = -1;
            switch (from)
            {
                case PoR.Screen:
                    screen = val;
                    break;
                case PoR.Window:
                    screen = val + Window.X;
                    break;
                case PoR.Screenshot:
                    screen = val + Window.X + Worker.shot.X;
                    break;
                case PoR.Face:
                    screen = Window.X + Worker.LastKnown.Value.X + (Worker.Right ? val + 32 : -val);
                    break;
            }
            switch (to)
            {
                case PoR.Screen:
                    return screen;
                case PoR.Window:
                    return screen - Window.X;
                case PoR.Screenshot:
                    return screen - (Window.X + Worker.shot.X);
                case PoR.Face:
                    screen -= (Window.X + Worker.LastKnown.Value.X);
                    return Worker.Right ? screen - 32 : -screen;
                default:
                    return -1;
            }
        }
    }
}
