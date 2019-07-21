using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using TeslaX.Properties;

namespace TeslaX
{
    static class Workflow
    {
        public static bool Active = false;

        private static int rows = 0;

        // This one is a bit more global.
        private static WindowManager windowManager;

        public static void Start()
        {
            Active = true;
            App.Status = "Initializing...";

            // Initializing finders.
            OffsetFinder offsetFinder = new OffsetFinder();
            BlockFinder blockFinder = new BlockFinder(App.Sprites[Settings.Default.SelectedBlock].Sprite(), Resources.dust, Resources.gems, Game.GetFistBitmap((int)Settings.Default.SkinColor));
            PlayerFinder playerFinder = new PlayerFinder((int)Settings.Default.SkinColor);

            // Initializing managers.
            MovementManager movementManager = new MovementManager();
            PunchManager punchManager = new PunchManager();

            // Preparing workflow variables. Compiler gets mad if we don't explicitly initialize them.
            Point offsetPosition = App.InvalidPoint;
            Point playerPosition = App.InvalidPoint;
            bool playerDirection = false;

            // -1 for no blocks, -2 for no player/offset.
            Smooth<int> distance = new Smooth<int>(150, (ov, nv) => Math.Abs(ov - nv) > 24 || nv < 0);

            #region Local functions.
            // Find and set first offset value.
            // - shot: screenshot of the entire window.
            bool SetNewOffset(Screenshot shot)
            {
                offsetPosition = offsetFinder.GetOffset(shot, true).Mod(32);
                if (offsetPosition == App.InvalidPoint)
                    return false;
                return true;
            }

            // Find and set first player position value.
            // - shot: screenshot of the entire window.
            // - Offset must already be found.
            bool SetNewPlayer(Screenshot shot)
            {
                List<int> EligibleY = App.EligibleBetween(0, shot.Height - 32, offsetPosition.Y, -shot.Y);
                foreach (int y in EligibleY)
                    for (int x = 0; x < windowManager.Width - 32; x++)
                    {
                        if (playerFinder.HasPlayer(shot, x, y, true))
                        {
                            playerPosition = new Point(x, y);
                            playerDirection = true;
                            return true;
                        }
                        if (playerFinder.HasPlayer(shot, x, y, false))
                        {
                            playerPosition = new Point(x, y);
                            playerDirection = false;
                            return true;
                        }
                    }
                return false;
            }

            Screenshot Shoot() =>
                windowManager.Shoot(
                    playerPosition.X + (playerDirection ? -Settings.Default.BlocksBehind * 32 : -Settings.Default.BlocksAhead * 32),
                    playerPosition.Y,
                    (Settings.Default.BlocksAhead + Settings.Default.BlocksBehind + 1) * 32,
                    64);

            // Find and set offset value.
            // - shot: screenshot with vertical offset of 0.
            bool SetOffset(Screenshot shot)
            {
                Point res = offsetFinder.GetOffset(shot);
                if (res == App.InvalidPoint)
                    return false;
                offsetPosition = res;
                return true;
            }

            // Find and set player position value.
            // - shot: screenshot with vertical offset of 0.
            bool SetPlayer(Screenshot shot)
            {
                foreach (var cmd in App.PlayerFindingOrder)
                {
                    bool tRight = playerDirection ^ !cmd.SameDirection;
                    int inc = (cmd.x1 < cmd.x2 ? 1 : -1);
                    for (int x = cmd.x1; x != cmd.x2 + inc; x += inc)
                    {
                        if (playerFinder.HasPlayer(shot, playerPosition.X + (playerDirection ? x : -x) - shot.X, 0, tRight))
                        {
                            playerPosition = new Point(playerPosition.X + (playerDirection ? x : -x), shot.Y);
                            playerDirection = tRight;
                            return true;
                        }
                    }
                }
                return false;
            }

            // Find and set distance value.
            // - shot: screenshot with vertical offset of 0.
            bool SetDistance(Screenshot shot)
            {
                List<int> ToCheck = App.EligibleBetween(shot.X - 31, shot.X + shot.Width - 1, offsetPosition.X);
                List<int> Blocks = new List<int>();
                foreach (int x in ToCheck)
                {
                    BlockState next = blockFinder.HasBlock(shot, x - shot.X, 0);
                    if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.Default.UncertainIsBlock))
                        Blocks.Add(x);
                }

                for (int i = (playerDirection ? 0 : Blocks.Count - 1); i >= 0 && i < Blocks.Count; i += playerDirection ? 1 : -1)
                {
                    int tDistance = playerDirection ? (Blocks[i] - playerPosition.X - 32) : (playerPosition.X - Blocks[i] - 32);
                    if (tDistance > 0)
                    {
                        distance.Value = tDistance;
                        return true;
                    }
                }
                distance.Value = -1;
                return false;
            }

            // Check whether there are dropped items behind the player.
            bool DropsBehind(Screenshot shot)
            {
                int x = playerDirection ? 0 : shot.Width - 1;
                for (int y = 0; y < shot.Height; y++)
                {
                    Color c = shot.GetPixel(x, y);
                    if (c.R + c.B < 5)
                        return true;
                }
                return false;
            }
            #endregion

            windowManager = new WindowManager(Settings.Default.Windowed);
            if(windowManager.HwndObject.Hwnd == IntPtr.Zero)
            {
                App.Status = "Window not found.";
                return;
            }

            // Finding offset and player.
            using(Screenshot shot = windowManager.Shoot())
            {
                if(!SetNewOffset(shot))
                {
                    App.Status = "Offset not found.";
                    return;
                }
                if (!SetNewPlayer(shot))
                {
                    App.Status = "Player not found.";
                    return;
                }
            }

            // Checking for distance.
            if (!Settings.Default.DebugMode)
                using (Screenshot shot = Shoot())
                    if (!SetDistance(shot))
                    {
                        App.Status = "No blocks found.";
                        return;
                    }

            // If we've been cancelled during preparations, count it as failed loading.
            if (!Active)
                return;

            // Preparing for working loop.
            /* Discord: to breaking. */
            App.Status = "Breaking...";
            Discord.Update(DiscordStatus.Breaking, rows);

            DebugForm debugForm = null;
            if (Settings.Default.DebugForm)
            {
                debugForm = new DebugForm();
                new Task(() =>
                {
                    debugForm.ShowDialog();
                }).Start();
            }

            // Otherwise, proceed.
            while (Active)
                using (Screenshot shot = Shoot())
                {
                    int stage = 0;

                    // Finding offset.
                    if (SetOffset(shot))
                    {
                        stage++;

                        // Finding player.
                        if (SetPlayer(shot))
                        {
                            stage++;

                            // Finding blocks and calculating distance.
                            // If there are no blocks, distance will be set to -1.
                            SetDistance(shot);
                        }
                    }

                    // If we didn't get to distance setting, point that out.
                    if (stage < 2)
                    {
                        distance.Value = -2;
                    }

                    // If we found any dropped blocks, stop immediately.
                    if (Settings.Default.StopOnFull && DropsBehind(shot))
                    {
                        distance.Value = -3;
                        break;
                    }

                    // If no blocks are found, we're done. Unless we're debugging.
                    if (distance < 0 && !Settings.Default.DebugMode)
                        break;

                    // Determining movement based on time/distance.
                    if (!Settings.Default.DebugMode)
                    {
                        bool? down = movementManager.Update(distance, playerDirection);
                        if (down != null)
                            windowManager.SendKey(playerDirection ? Keys.D : Keys.A, down.Value);
                    }

                    // Determinine punching based on time.
                    if (Settings.Default.SimulatePunch && !Settings.Default.DebugMode)
                    {
                        bool? punch = punchManager.Update();
                        if (punch != null)
                            windowManager.SendKey(Keys.Space, punch.Value);
                    }

                    if (Settings.Default.DebugForm)
                    {
                        #region Managing debugForm.
                        StringBuilder debugInfo = new StringBuilder();
                        debugInfo.AppendLine("Offset:      " + offsetPosition.ToString() + (stage < 1 ? "[?]" : ""));
                        debugInfo.AppendLine("Player:      " + playerPosition.ToString() + (stage < 2 ? "[?]" : ""));
                        debugInfo.AppendLine("Distance:    " + (distance > 0 ? distance.ToString() : "N/A"));
                        debugInfo.AppendLine("NewDistance: " + (distance.UnsafeValue > 0 ? distance.UnsafeValue.ToString() : "N/A"));
                        debugForm.Invoke((MethodInvoker)delegate
                        {
                            debugForm.Location = shot.Location.Add(windowManager.Location).Add(0, -debugForm.Size.Height);
                            debugForm.DebugLabel.Text = debugInfo.ToString();
                            debugForm.DebugPlayerButton.Location = new Point(playerPosition.X - shot.X, debugForm.Size.Height - DebugForm.ph - 1);
                            debugForm.DebugBlockButton.Location = new Point(
                                distance > -1 ? playerPosition.X + (playerDirection ? 1 : -1) * (distance + 32) + windowManager.X - debugForm.Location.X : -33,
                                debugForm.Size.Height - DebugForm.bh - 1
                                );
                        });
                        #endregion
                    }
                }
            // Making sure we're idle.
            windowManager.SendKey(playerDirection ? Keys.D : Keys.A, false);
            windowManager.SendKey(Keys.Space, false);

            if (Settings.Default.DebugForm)
                debugForm.Done();

            bool tocont = Settings.Default.Continue && Active && (distance == -1);
            if (tocont)
            {
                App.Status = "Executing custom script...";
                Discord.Update(DiscordStatus.Advancing, rows);
                Script.Execute(windowManager);
            }

            if (distance == -1)
            {
                App.Status = "Finished: out of blocks.";
                // Only updating rows on supposed end-of-row cases. It's only for DRP anyway.
                rows++;
            }
            else if (distance == -2)
                App.Status = "Finished: lost the player.";
            else if (distance.UnsafeValue == -3)
                App.Status = "Finished: full inventory.";
            else
                App.Status = "Finished: manual request.";

            return;
        }
    }
}
