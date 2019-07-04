using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public static partial class Worker
    {
        public static bool Busy;

        private static Point Offset;
        private static Smooth<Point> LastKnown;
        private static bool Right;

        private static int NewDistance = -1;
        private static Smooth<int> Distance;
        private static Screenshot shot;
        private static Smooth<bool> KeyDown;

        private static bool SetNewOffset(this Screenshot shot)
        {
            Point res = shot.GetOffset(true).Mod(32);
            if (res == Global.InvalidPoint)
                return false;
            Offset = res;
            return true;
        }

        private static bool SetNewPlayer(this Screenshot shot)
        {
            List<int> EligibleY = Global.EligibleBetween(0, shot.Height - 32, Offset.Y).AddInt(-shot.Y);
            foreach (int y in EligibleY)
            {
                var res = shot.SeekPlayer(0, shot.Width - 32 - 1, y);
                if (res.x != -1)
                {
                    LastKnown = new Smooth<Point>(Settings.PlayerSpikeLength, Settings.PlayerSpikeCondition);
                    LastKnown.Value = new Point(res.x, y);
                    Right = res.Right;
                    return true;
                }
            }
            return false;
        }

        private static bool SetOffset(this Screenshot shot)
        {
            Point res = shot.GetOffset();
            if (res == Global.InvalidPoint)
                return false;
            Offset = res;
            return true;
        }

        private static bool SetPlayer(this Screenshot shot)
        {
            foreach (var cmd in Settings.Order)
            {
                bool tRight = Right ^ !cmd.SameDirection;
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

        private static bool SetDistance(this Screenshot shot)
        {
            List<int> ToCheck = Global.EligibleBetween(shot.X - 31, shot.X + shot.Width - 1, Offset.X);
            List<int> Blocks = new List<int>();
            foreach (int x in ToCheck)
            {
                BlockState next = shot.HasBlock(x - shot.X, 0);
                if (next == BlockState.Block || (next == BlockState.Uncertain && Settings.UncertainIsBlock))
                    Blocks.Add(x);
            }

            for (int i = (Right ? 0 : Blocks.Count - 1); i >= 0 && i < Blocks.Count; i += Right ? 1 : -1)
            {
                int tDistance = Right ? (Blocks[i] - LastKnown.Value.X - 32) : (LastKnown.Value.X - Blocks[i] - 32);
                if (tDistance > 0)
                {
                    NewDistance = tDistance;
                    return true;
                }
            }
            NewDistance = -1;
            return false;
        }
    }
}
