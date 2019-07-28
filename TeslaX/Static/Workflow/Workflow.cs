using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using TheLeftExit.TeslaX.Helpers;

namespace TheLeftExit.TeslaX.Static
{
    internal static partial class Workflow
    {
        public static bool Active = false;

        private static int rows = 0;

        // Return value: whether to attempt breaking again.
        public static bool Start()
        {
            App.Status = "Initializing...";

            // Retrieve ProcessHandle.
            var ps = Process.GetProcessesByName("Growtopia");
            if (ps.Length == 0)
            {
                App.Status = "Growtopia isn't open.";
                return false;
            }
            else if (ps.Length > 1)
            {
                App.Status = "More than one Growtopia detected.";
                return false;
            }
            Process process = ps.Single();
            ProcessHandle handle = process.GetHandle();

            // Prepare local variables.
            MovementManager movementManager = new MovementManager();
            PunchManager punchManager = new PunchManager();
            WindowManager windowManager;

            bool direction = false;

            // Initializing.
            windowManager = new WindowManager();
            if (windowManager.HwndObject.Hwnd == IntPtr.Zero)
            {
                App.Status = "Window not found.";
                return false;
            }

            if (!UserSettings.Current.DebugMode && handle.GetNextBlockInfo() == null)
            {
                App.Status = "No blocks found.";
                return false;
            }

            /* var sw = Stopwatch.StartNew(); */

            // Breaking.
            Discord.Update(DiscordStatus.Breaking, rows);
            App.Status = "Breaking...";
            while (Active)
            {
                /* App.Status = ((double)sw.ElapsedTicks * 1000 / Stopwatch.Frequency).ToString(); */
                /* sw.Restart(); */
                // Checking if we've been manually cancelled.
                if (Keyboard.IsKeyDown(Key.S))
                {
                    Active = false;
                    break;
                }

                // Getting distance; exiting if next block isn't target or doesn't exist.
                int? rawdistance = handle.GetNextBlockInfo()?.Distance;
                if (!UserSettings.Current.DebugMode && rawdistance == null)
                {
                    break;
                }
                int distance = rawdistance.Value;

                direction = handle.GetDirection();

                // Simulating punch.
                if (!UserSettings.Current.DebugMode && UserSettings.Current.Punch)
                {
                    bool? punch = punchManager.Update();
                    if (punch != null)
                        windowManager.SendKey(Keys.Space, punch.Value);
                }

                // Simulating movement.
                // Determining movement based on time/distance.
                if (!UserSettings.Current.DebugMode)
                {
                    bool? down = movementManager.Update(Move(distance, direction));
                    if (down != null)
                        windowManager.SendKey(direction ? Keys.D : Keys.A, down.Value);
                }
            }

            // Cancelling input if there's any.
            windowManager.SendKey(direction ? Keys.D : Keys.A, false);
            windowManager.SendKey(Keys.Space, false);

            if (Active == true)
            {
                rows++;
                if (!UserSettings.Current.DebugMode && UserSettings.Current.Continue)
                {
                    App.Status = "Executing custom script...";
                    Discord.Update(DiscordStatus.Advancing, rows);
                    Script.Execute(windowManager);
                    if (!Active)
                    {
                        App.Status = "Finished: manual request.";
                        return false;
                    }
                }
                else
                    App.Status = "Finished: out of blocks.";
                return UserSettings.Current.Continue && !UserSettings.Current.DebugMode;
            }
            else
            {
                App.Status = "Finished: manual request.";
                return false;
            }
        }
    }
}
