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

            bool direction = false;

            if (!UserSettings.Current.DebugMode && (!handle.GetNextBlockInfo()?.IsBlock() ?? true))
            {
                App.Status = "No blocks found.";
                return false;
            }

            /* var sw = Stopwatch.StartNew(); */

            // Breaking.
            Discord.Update(DiscordStatus.Breaking, rows);
            App.Status = "Breaking...";
            bool NextRowCondition = false;
            while (Active)
            {
                /* App.Status = ((double)sw.ElapsedTicks * 1000 / Stopwatch.Frequency).ToString(); */
                /* sw.Restart(); */

                // Getting distance.
                var info = handle.GetNextBlockInfo();
                int distance;
                if (!UserSettings.Current.DebugMode)
                {
                    if (!info?.IsBlock() ?? true)
                    {
                        if (info?.IsDoor() ?? false)
                            NextRowCondition = true;
                        break;
                    }
                    distance = info.Distance;
                }
                else
                    distance = info?.Distance ?? -128;

                direction = handle.GetDirection();

                // Simulating punch.
                if (!UserSettings.Current.DebugMode && UserSettings.Current.Punch)
                {
                    bool? punch = punchManager.Update();
                    if (punch != null)
                        handle.SendKey(Keys.Space, punch.Value);
                }

                // Simulating movement.
                // Determining movement based on time/distance.
                if (!UserSettings.Current.DebugMode)
                {
                    bool? down = movementManager.Update(Move(distance, direction));
                    if (down != null)
                        handle.SendKey(direction ? Keys.D : Keys.A, down.Value);
                }

                if (info == null)
                    App.Status = "No blocks found.";
                else
                    App.Status = "FG: " + info.Foreground + ", BG: " + info.Background + ", distance: " + info.Distance + ".";
            }

            // Cancelling input if there's any.
            handle.SendKey(direction ? Keys.D : Keys.A, false);
            handle.SendKey(Keys.Space, false);

            if (Active == true)
            {
                rows++;
                if (!UserSettings.Current.DebugMode && UserSettings.Current.Continue && NextRowCondition)
                {
                    App.Status = "Executing custom script...";
                    Discord.Update(DiscordStatus.Advancing, rows);
                    handle.ExecuteScript(direction);
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

        public static void BlockAheadToStatus()
        {
            App.Status = "Initializing...";

            // Retrieve ProcessHandle.
            var ps = Process.GetProcessesByName("Growtopia");
            if (ps.Length == 0)
            {
                App.Status = "Growtopia isn't open.";
                return;
            }
            else if (ps.Length > 1)
            {
                App.Status = "More than one Growtopia detected.";
                return;
            }
            Process process = ps.Single();
            ProcessHandle handle = process.GetHandle();

            var info = handle.GetNextBlockInfo();
            if (info == null)
                App.Status = "No blocks found.";
            else
                App.Status = "FG: " + info.Foreground + ", BG: " + info.Background + ", distance: " + info.Distance + ".";
        }
    }
}
