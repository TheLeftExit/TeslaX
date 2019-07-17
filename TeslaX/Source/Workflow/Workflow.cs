using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TeslaX.Properties;

namespace TeslaX
{
    static partial class Workflow
    {
        public static bool Working = false;

        public static void Start()
        {
            // Initializing finders.
            OffsetFinder offsetFinder = new OffsetFinder();
            BlockFinder blockFinder = new BlockFinder(App.Sprites[Settings.Default.SelectedBlock], Resources.dust, Resources.gems, Game.GetFistBitmap((int)Settings.Default.SkinColor));
            PlayerFinder playerFinder = new PlayerFinder((int)Settings.Default.SkinColor);

            // Preparing managers.
            WindowManager windowManager;
            InputManager inputManager;

            // Preparing workflow variables. Compiler gets mad if we don't explicitly initialize them.
            Point offsetPosition = App.InvalidPoint;
            Point playerPosition = App.InvalidPoint;
            bool playerDirection = false;

            Smooth<int> distance;

            #region Local finders.
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
                List<int> EligibleY = App.EligibleBetween(0, shot.Height - 32, offsetPosition.Y).AddInt(-shot.Y);
                foreach (int y in EligibleY)
                    for (int x = 0; x < windowManager.X - 32; x++)
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
            #endregion

            // Finding window.
            windowManager = new WindowManager(Settings.Default.Windowed);
            if (windowManager.HwndObject.Hwnd == IntPtr.Zero)
            {
                Message.NoWindow();
                return;
            }

            // Finding offset and player.
            using(Screenshot shot = windowManager.Shoot())
            {
                if(!SetNewOffset(shot))
                {
                    Message.NoNewOffset();
                    return;
                }
                if (!SetNewPlayer(shot))
                {
                    Message.NoNewPlayer();
                    return;
                }
            }

            // Preparing for working loop.
            distance = new Smooth<int>(150, (ov, nv) => Math.Abs(ov - nv) > 24 || nv == -1);
            inputManager = new InputManager();
            /* Discord: to breaking. */

            DebugForm debugForm = null;
            if (Settings.Default.Debug)
            {
                debugForm = new DebugForm();
                new Task(() => debugForm.ShowDialog()).Start();
            }

            // Loop.
            Working = true;
            while (Working)
                using (Screenshot shot = new Screenshot(
                    playerPosition.X + (playerDirection ? -Settings.Default.BlocksBehind * 32 : -Settings.Default.BlocksAhead * 32),
                    playerPosition.Y,
                    (Settings.Default.BlocksAhead + Settings.Default.BlocksBehind + 1) * 32,
                    64))
                {
                    int stage = 0; // How far we've gotten. Used in debugging.

                    // Finding offset.
                    if (!SetOffset(shot))
                        goto MyLabel;
                    stage++;

                    // Finding player.
                    if (!SetPlayer(shot))
                        goto MyLabel;
                    stage++;

                    // Finding blocks and calculating distance.
                    SetDistance(shot);

                    // Determining input based on distance.
                    bool? down = inputManager.Update(distance, playerDirection);
                    if (down != null)
                        windowManager.SendKey(playerDirection ? Keys.D : Keys.A, down.GetValueOrDefault());

                    MyLabel:
                    if (Settings.Default.Debug)
                    {
                        debugForm.Invoke((MethodInvoker)delegate
                        {
                            debugForm.Location = shot.Location.Add(0, -debugForm.Size.Height);
                            debugForm.DebugLabel.Text = "";
                            debugForm.DebugPlayerButton.Location = new Point(playerPosition.X - shot.X, debugForm.Size.Height - DebugForm.ph - 1);
                            debugForm.DebugBlockButton.Location = new Point(
                                distance != -1 ? playerPosition.X + (playerDirection ? 1 : -1) * (distance + 32) + windowManager.X - debugForm.Location.X : -33,
                                debugForm.Size.Height - DebugForm.bh - 1
                                );
                        });
                    }
                }

            if(Settings.Default.Debug)
                debugForm.Invoke((MethodInvoker)delegate
                {
                    debugForm.Close();
                });

            Working = false;
        }
    }
}
