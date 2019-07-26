using System.Collections.Generic;
using System.Drawing;
using TheLeftExit.TeslaX.Entities;
using TheLeftExit.TeslaX.Helpers;

namespace TheLeftExit.TeslaX.Static
{
    internal static partial class Workflow
    {
        // Wrapper class for current workflow state, to be passed into functions.
        private class State
        {
            public OffsetFinder offsetFinder;
            public BlockFinder blockFinder;
            public PlayerFinder playerFinder;

            public MovementManager movementManager;
            public PunchManager punchManager;
            public WindowManager windowManager;

            public Point offsetPosition;
            public Point playerPosition;
            public bool playerDirection;

            public Smooth<int> distance;
        }

        // Here we're abstracting most of complicated logic into state-dependant functions.

        private static bool SetNewOffset(this State s, Screenshot shot)
        {
            s.offsetPosition = s.offsetFinder.GetOffset(shot, true).Mod(32);
            if (s.offsetPosition == App.InvalidPoint)
                return false;
            return true;
        }

        private static bool SetNewPlayer(this State s, Screenshot shot)
        {
            List<int> EligibleY = App.EligibleBetween(0, shot.Height - 32, s.offsetPosition.Y, -shot.Y);
            foreach (int y in EligibleY)
                for (int x = 0; x < s.windowManager.Width - 32; x++)
                {
                    if (s.playerFinder.HasPlayer(shot, x, y, true))
                    {
                        s.playerPosition = new Point(x, y);
                        s.playerDirection = true;
                        return true;
                    }
                    if (s.playerFinder.HasPlayer(shot, x, y, false))
                    {
                        s.playerPosition = new Point(x, y);
                        s.playerDirection = false;
                        return true;
                    }
                }
            return false;
        }

        private static Screenshot Shoot(this State s) =>
                s.windowManager.Shoot(
                    s.playerPosition.X + (s.playerDirection ? -UserSettings.Current.BlocksBehind * 32 : -UserSettings.Current.BlocksAhead * 32),
                    s.playerPosition.Y,
                    (UserSettings.Current.BlocksAhead + UserSettings.Current.BlocksBehind + 1) * 32,
                    64);

        private static bool SetOffset(this State s, Screenshot shot)
        {
            Point res = s.offsetFinder.GetOffset(shot);
            if (res == App.InvalidPoint)
                return false;
            s.offsetPosition = res;
            return true;
        }

        private static bool SetPlayer(this State s, Screenshot shot)
        {
            foreach (var cmd in App.PlayerFindingOrder)
            {
                bool tRight = s.playerDirection ^ !cmd.SameDirection;
                int inc = (cmd.x1 < cmd.x2 ? 1 : -1);
                for (int x = cmd.x1; x != cmd.x2 + inc; x += inc)
                {
                    if (s.playerFinder.HasPlayer(shot, s.playerPosition.X + (s.playerDirection ? x : -x) - shot.X, 0, tRight))
                    {
                        s.playerPosition = new Point(s.playerPosition.X + (s.playerDirection ? x : -x), shot.Y);
                        s.playerDirection = tRight;
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool SetDistance(this State s, Screenshot shot)
        {
            List<int> ToCheck = App.EligibleBetween(shot.X - 31, shot.X + shot.Width - 1, s.offsetPosition.X);
            List<int> Blocks = new List<int>();
            foreach (int x in ToCheck)
            {
                BlockState next = s.blockFinder.HasBlock(shot, x - shot.X, 0);
                if (next == BlockState.Block || (next == BlockState.Uncertain && UserSettings.Current.UncertainIsBlock))
                    Blocks.Add(x);
            }

            for (int i = (s.playerDirection ? 0 : Blocks.Count - 1); i >= 0 && i < Blocks.Count; i += s.playerDirection ? 1 : -1)
            {
                int tDistance = s.playerDirection ? (Blocks[i] - s.playerPosition.X - 32) : (s.playerPosition.X - Blocks[i] - 32);
                if (tDistance > 0)
                {
                    s.distance.Value = tDistance;
                    return true;
                }
            }
            s.distance.Value = -1;
            return false;
        }

        private static bool DropsBehind(this State s, Screenshot shot)
        {
            int x = s.playerDirection ? 0 : shot.Width - 1;
            for (int y = 0; y < shot.Height; y++)
            {
                Color c = shot.GetPixel(x, y);
                if (c.R + c.G + c.B < 7)
                    return true;
            }
            return false;
        }

        private static bool Move(this State s)
        {
            if (s.distance == -1)
                return false;
            int target = s.playerDirection ? UserSettings.Current.DistanceRight : UserSettings.Current.DistanceLeft;
            return s.distance > target;
        }
    }
}