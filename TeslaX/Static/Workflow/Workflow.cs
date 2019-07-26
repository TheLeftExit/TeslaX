using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLeftExit.TeslaX.Entities;
using TheLeftExit.TeslaX.Helpers;
using TheLeftExit.TeslaX.Interface;
using TheLeftExit.TeslaX.Properties;

namespace TheLeftExit.TeslaX.Static
{
    internal static partial class Workflow
    {
        public static bool Active = false;

        private static int rows = 0;

        // Return value: whether to attempt breaking again.
        public static bool Start()
        {
            Active = true;
            App.Status = "Initializing...";

            // Checking for custom textures.
            UserSettings.Current.CustomTextures = false; //Texture.Replaced();

            // Initializing State.
            State s = new State();

            // Initializing finders.
            s.offsetFinder = new OffsetFinder();
            s.blockFinder = new BlockFinder(App.Sprites[UserSettings.Current.SelectedBlock].Sprite(), Resources.dust, Resources.gems, Game.GetFistBitmap(UserSettings.Current.SkinColor));
            s.playerFinder = new PlayerFinder(UserSettings.Current.SkinColor);

            // Initializing managers.
            s.movementManager = new MovementManager();
            s.punchManager = new PunchManager();

            // Preparing workflow variables. Compiler gets mad if we don't explicitly initialize them.
            s.offsetPosition = App.InvalidPoint;
            s.playerPosition = App.InvalidPoint;
            s.playerDirection = false;
            s.distance = new Smooth<int>(150, (ov, nv) => Math.Abs(ov - nv) > 24 || nv < 0); // -1 for no blocks, -2 for no player/offset, -3 for full inventory.

            // Initializing window.
            s.windowManager = new WindowManager(UserSettings.Current.Windowed);
            if (s.windowManager.HwndObject.Hwnd == IntPtr.Zero)
            {
                App.Status = "Window not found.";
                return false;
            }

            // Finding offset and player.
            using (Screenshot shot = s.windowManager.Shoot())
            {
                if (!s.SetNewOffset(shot))
                {
                    App.Status = "Offset not found.";
                    return false;
                }
                if (!s.SetNewPlayer(shot))
                {
                    App.Status = "Player not found.";
                    return false;
                }
            }

            // Checking for distance.
            if (!UserSettings.Current.DebugMode)
                using (Screenshot shot = s.Shoot())
                    if (!s.SetDistance(shot))
                    {
                        App.Status = "No blocks found.";
                        return false;
                    }

            // If we've been cancelled during preparations, act accordingly.
            if (!Active)
            {
                App.Status = "Finished: manual request.";
                Discord.Update(DiscordStatus.Idle);
                return false;
            }

            // Update status.
            if (UserSettings.Current.DebugMode)
            {
                App.Status = "Detecting...";
                Discord.Update(DiscordStatus.Debugging);
            }
            else
            {
                App.Status = "Breaking...";
                Discord.Update(DiscordStatus.Breaking, rows);
            }

            // Initialize debug form.
            DebugForm debugForm = null;
            if (UserSettings.Current.DebugForm)
            {
                debugForm = new DebugForm();
                new Task(() => debugForm.ShowDialog()).Start();
            }

            // Proceed.
            while (Active)
                using (Screenshot shot = s.Shoot())
                {
                    int stage = 0;

                    // Finding offset.
                    if (s.SetOffset(shot))
                    {
                        stage++;

                        // Finding player.
                        if (s.SetPlayer(shot))
                        {
                            stage++;

                            // Finding blocks and calculating distance.
                            // If there are no blocks, distance will be set to -1.
                            s.SetDistance(shot);

                            // If no blocks are found, we're done. Unless we're debugging.
                            if (s.distance == -1 && !UserSettings.Current.DebugMode)
                                break;
                        }
                    }

                    // If we didn't get to distance setting, point that out.
                    if (stage < 2)
                    {
                        s.distance.Value = -2;

                        // If this persists, pause and attempt to find offset and player again.
                        if (s.distance == -2)
                            break;
                    }

                    // If we found any dropped blocks, stop immediately.
                    if (UserSettings.Current.StopOnFull && s.DropsBehind(shot))
                    {
                        s.distance.Force(-3);
                        break;
                    }

                    // Determining movement based on time/distance.
                    if (!UserSettings.Current.DebugMode)
                    {
                        bool? down = s.movementManager.Update(s.Move());
                        if (down != null)
                            s.windowManager.SendKey(s.playerDirection ? Keys.D : Keys.A, down.Value);
                    }

                    // Determinine punching based on time.
                    if (UserSettings.Current.Punch && !UserSettings.Current.DebugMode)
                    {
                        bool? punch = s.punchManager.Update();
                        if (punch != null)
                            s.windowManager.SendKey(Keys.Space, punch.Value);
                    }

                    // Update debug form.
                    if (UserSettings.Current.DebugForm)
                    {
                        #region Managing debugForm.
                        StringBuilder debugInfo = new StringBuilder();
                        debugInfo.AppendLine("Offset:      " + s.offsetPosition.ToString() + (stage < 1 ? "[?]" : ""));
                        debugInfo.AppendLine("Player:      " + s.playerPosition.ToString() + (stage < 2 ? "[?]" : ""));
                        debugInfo.AppendLine("Distance:    " + (s.distance > 0 ? s.distance.ToString() : "N/A"));
                        debugInfo.AppendLine("NewDistance: " + (s.distance.UnsafeValue > 0 ? s.distance.UnsafeValue.ToString() : "N/A"));
                        debugForm.Invoke((MethodInvoker)delegate
                        {
                            debugForm.Location = shot.Location.Add(s.windowManager.Location).Add(0, -debugForm.Size.Height);
                            debugForm.DebugLabel.Text = debugInfo.ToString();
                            debugForm.DebugPlayerButton.Location = new Point(s.playerPosition.X - shot.X, debugForm.Size.Height - DebugForm.ph - 1);
                            debugForm.DebugBlockButton.Location = new Point(
                                s.distance > -1 ? s.playerPosition.X + (s.playerDirection ? 1 : -1) * (s.distance + 32) + s.windowManager.X - debugForm.Location.X : -33,
                                debugForm.Size.Height - DebugForm.bh - 1
                                );
                        });
                        #endregion
                    }
                }

            // Making sure we're idle.
            s.windowManager.SendKey(s.playerDirection ? Keys.D : Keys.A, false);
            s.windowManager.SendKey(Keys.Space, false);

            if (UserSettings.Current.DebugForm)
                debugForm.Done();

            if (UserSettings.Current.DebugMode)
            {
                App.Status = "Finished: manual request.";
                return false;
            }

            if (s.distance == -1)
            {
                rows++;
                if (UserSettings.Current.Continue && !UserSettings.Current.DebugMode)
                {
                    App.Status = "Executing custom script...";
                    Discord.Update(DiscordStatus.Advancing, rows);
                    Script.Execute(s.windowManager);
                    // Shortly status will be updated with either "Breaking..." or a "not found" message.
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
            else if (s.distance == -2)
                App.Status = "Finished: lost the player.";
            else if (s.distance == -3)
                App.Status = "Finished: full inventory.";
            else
                App.Status = "Finished: manual request.";

            return false;
        }
    }
}
