using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;
using System.Diagnostics;

namespace TeslaX
{
    public static partial class Worker
    {
        public static bool SetOffset(this Screenshot shot)
        {
            Point res = shot.GetOffset();
            if (res == Global.InvalidPoint)
                return false;
            Offset = res;
            return true;
        }

        public static bool SetPlayer(this Screenshot shot)
        {
            foreach(var cmd in Settings.Order)
            {
                bool tRight = Right ^ !cmd.SameDirection;
                // All this trouble to check a range from x1 to x2 in their respective order.
                int inc = (cmd.x1 < cmd.x2 ? 1 : -1);
                for (int x = cmd.x1; x != cmd.x2 + inc; x += inc)
                {
                    if (shot.HasPlayer(LastKnown.Value.X + (Right ? x : -x) - shot.X, 0, tRight))
                    {
                        LastKnown.Value = new Point(LastKnown.Value.X + (Right ? x : -x), shot.Y);
                        Right = tRight;
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool SetDistance(this Screenshot shot)
        {
            return true;
        }

        public static Smooth<int> Distance;
        public static Screenshot shot;

        public static void RowLoop()
        {

            Distance = new Smooth<int>(150, ((ov, nv) => Math.Abs(ov - nv) > 24 || nv == -1));
            int NewDistance;

            Smooth<bool> KeyDown = new Smooth<bool>(150, (ov, nv) => ov != nv);

            int ahead = Settings.BlocksAhead;
            int behind = Settings.BlocksBehind;

            #region [Debug] Initializing.
            DebugForm debugForm = new DebugForm();
            StringBuilder debugInfo = new StringBuilder();
            if (Settings.Debug)
                new Thread(() => { debugForm.ShowDialog(); }).Start();
            #endregion

            Busy = true;
            while (Busy)
            {
                using (shot = new Screenshot(LastKnown.Value.X + (Right ? -behind * 32 : -ahead * 32), LastKnown.Value.Y, (ahead + behind + 1) * 32, 64))
                {

                    #region [Debug] Clearing.
                    if (Settings.Debug)
                    {
                        debugInfo.Clear();
                    }
                    #endregion

                    if (!shot.SetOffset())
                    {
                        #region [Debug] Appending Offset and updating.
                        if (Settings.Debug)
                        {
                            debugInfo.AppendLine("Offset:   N/A");
                            debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                        }
                        #endregion
                        continue;
                    }
                    else
                    #region [Debug] Appending Offset.
                    if (Settings.Debug)
                        debugInfo.AppendLine("Offset: " + Offset.ToString());
                    #endregion

                    // Only recorded for debug purposes. Will put in real use or restructure, this is ugly.
                    bool pfound = shot.SetPlayer();

                    #region [Debug] Appending Player, Direction and updating.
                    if (Settings.Debug)
                    {
                        debugInfo.AppendLine("Player: " + LastKnown.ToString() + (pfound ? "" : "[?]"));
                        debugInfo.AppendLine("Direction: " + (Right ? "Right" : "Left"));
                    }
                    #endregion

                    NewDistance = -1;

                    // New block finding mechanism.
                    // 1. Get list of all block locations.
                    List<int> ToCheck = Global.EligibleBetween(shot.X - 31, shot.X + shot.Width - 1, Offset.X);
                    List<int> Blocks = new List<int>();
                    foreach (int x in ToCheck)
                    {
                        BlockState next = shot.HasBlock(x - shot.X, 0);
                        if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.UncertainIsBlock))
                            Blocks.Add(x);
                    }

                    // 2. Go through them to find the first one in front of player.
                    for (int i = (Right ? 0 : Blocks.Count - 1); i >= 0 && i < Blocks.Count; i += Right ? 1 : -1)
                    {
                        int tDistance = Right ? (Blocks[i] - LastKnown.Value.X - 32) : (LastKnown.Value.X - Blocks[i] - 32);
                        if (tDistance > 0)
                        {
                            NewDistance = tDistance;
                            break;
                        }
                    }

                    Distance.Value = NewDistance;

                    int CrackState = shot.HasCracks(Worker.LastKnown.Value.X + (Worker.Right ? 1 : -1) * (Worker.Distance.Value + 32) + Window.X - shot.X, 0);
                    #region [Debug] Appending CrackState.
                    if (Settings.Debug)
                    {
                        debugInfo.AppendLine("CrackState: " + (CrackState == -1 ? "N/A" : CrackState.ToString()));
                    }
                    #endregion

                    // If facing left, maximum distance to reach the block is 58.
                    // If right, 38.
                    KeyDown.Value = Distance.Value > (Right ? 38 : 58) && Distance != -1;

                    // I realized I removed input completely while restructuring.
                    // When trying to reimplement, also realized that managing input is a problem of itself.
                    // Will take care of it later.

                    #region [Debug] Appending NewDistance, Distance, Keydown and updating.
                    if (Settings.Debug)
                    {
                        debugInfo.AppendLine("NewDistance: " + (NewDistance == -1 ? "N/A" : NewDistance.ToString()));
                        debugInfo.AppendLine("Distance: " + (Distance == -1 ? "N/A" : Distance.ToString()));
                        debugInfo.AppendLine("KeyDown: " + KeyDown.ToString());
                        debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                    }
                    #endregion
                }
            }
        }
    }
}