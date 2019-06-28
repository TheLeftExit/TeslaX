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
            var res = shot.SeekPlayer(LastKnown.Value.X - shot.X - Settings.Radius, LastKnown.Value.X - shot.X + Settings.Radius, 0);
            if (res.x == -1)
                return false;
            LastKnown.Value = new Point(shot.X + res.x, LastKnown.Value.Y);
            Right = res.Right;
            return true;
        }

        public static void RowLoop()
        {
            Screenshot shot;

            Smooth<int> Distance = new Smooth<int>(150, ((ov, nv) => Math.Abs(ov - nv) > 24 || nv == -1));
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
                shot = new Screenshot(LastKnown.Value.X + (Right ? -behind * 32 : -ahead * 32), LastKnown.Value.Y, (ahead + behind + 1) * 32, 64);

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

                List<int> ToCheck;
                NewDistance = -1;
                // This "if" feels a lot like spaghetti code.
                // Might generalize it to a single case with a few more ternaries later.
                if (Right)
                {
                    ToCheck = Global.EligibleBetween(LastKnown.Value.X + 32, LastKnown.Value.X + 32 + ahead * 32, Offset.X).AddInt(-shot.X);
                    for (int x = 0; x < ToCheck.Count; x++)
                    {
                        BlockState next = shot.HasBlock(ToCheck[x], 0);
                        if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.UncertainIsBlock))
                        {
                            NewDistance = (ToCheck[x] + shot.X) - LastKnown.Value.X - 32;
                            break;
                        }
                    }
                }
                else
                {
                    ToCheck = Global.EligibleBetween(LastKnown.Value.X - 32 - ahead * 32, LastKnown.Value.X - 32, Offset.X).AddInt(-shot.X);
                    for (int x = ToCheck.Count - 1; x >= 0; x--)
                    {
                        BlockState next = shot.HasBlock(ToCheck[x], 0);
                        if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.UncertainIsBlock))
                        {
                            NewDistance = LastKnown.Value.X - (ToCheck[x] + shot.X) - 32;
                            break;
                        }
                    }
                }

                Distance.Value = NewDistance;

                // If facing left, maximum distance to reach the block is 58.
                // If right, 38.
                KeyDown.Value = Distance.Value > (Right ? 38 : 58) && Distance != -1;

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