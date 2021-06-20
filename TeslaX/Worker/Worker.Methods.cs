using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace TheLeftExit.TeslaX
{
    public partial class Worker
    {
        private bool toStop = false;
        public async Task Stop()
        {
            toStop = true;
            while (toStop)
                await Task.Delay(10);
        }

        public async Task BreakAsync(short BlockID, TuningSettings settings)
        {
            updateStatus("Breaking...");

            var movementManager = new MovementManager(settings.MinStop, settings.MaxMove);
            var punchManager = new PunchManager();

            bool right = playerDir();

            while (!toStop)
            {
                Block block = blockAhead();
                // Are there any blocks at all?
                if(block == null)
                {
                    updateStatus("Finished: no blocks in range.");
                    break;
                }
                int distance = punchingDistance(block);
                // Is whatever we've found in range?
                if(distance > settings.Range)
                {
                    updateStatus("Finished: no blocks in range.");
                }
                // Is whatever we've found our target?
                if(BlockID != -1 && block.FG != BlockID && block.BG != BlockID)
                {
                    updateStatus("Finished: different block detected.");
                    break;
                }
                // Determine and perform inputs.
                bool? toMove = movementManager.Update(distance > settings.TargetDistance);
                if (toMove.HasValue)
                    windowHandle.SendKey(right ? Keys.D : Keys.A, toMove.Value);
                bool? toPunch = punchManager.Update();
                if (toPunch.HasValue)
                    windowHandle.SendKey(Keys.Space, toPunch.Value);
                // Take a break.
                await Task.Delay(10);
            }

            // Wrapping up
            if(movementManager.IsDown)
                windowHandle.SendKey(right ? Keys.D : Keys.A, false);
            if(punchManager.IsDown)
                windowHandle.SendKey(Keys.Space, false);

            toStop = false;
        }

        public void BlockAheadToStatus()
        {
            Block block = blockAhead();
            if (block == null)
                updateStatus("Detected: nothing.");
            else if (block.FG != 0 && block.BG != 0)
                updateStatus($"Detected: {blockToString(block.FG)} & {blockToString(block.BG)} (distance: {punchingDistance(block)}).");
            else
                updateStatus($"Detected: {blockToString(block.BG == 0 ? block.FG : block.BG)} (distance: {punchingDistance(block)}).");
        }

        public async Task DebugAsync()
        {
            while (!toStop)
            {
                BlockAheadToStatus();
                await Task.Delay(10);
            }

            toStop = false;
        }

        public async Task<HashSet<short>> WorldScanAsync()
        {
            var res = new HashSet<short>();
            await Task.Run(() =>
            {
                for (int x = 0; x < 100; x++)
                    for (int y = 0; y < 54; y++)
                    {
                        Block block = blockAt(x, y);
                        res.Add(block.FG);
                        res.Add(block.BG);
                    }
            });
            
            return res;
        }

        // Yea sure I'll do it with enum tuple arrays later.
        // The whole script system needs reworking, but it'll hardly impact appearance or performance, so it's low priority.
        public async Task ExecuteScriptAsync(StringCollection script)
        {
            foreach (var s in script)
            {
                if (toStop)
                {
                    toStop = false;
                    return;
                }
                var split = s.Split(" ");
                int duration = Convert.ToInt32(split[1]);
                switch (split[0])
                {
                    case "wait":
                        await windowHandle.HoldKeyAsync(Keys.None, duration);
                        break;
                    case "forward":
                        await windowHandle.HoldKeyAsync(playerDir() ? Keys.D : Keys.A, duration);
                        break;
                    case "backward":
                        await windowHandle.HoldKeyAsync(playerDir() ? Keys.A : Keys.D, duration);
                        break;
                    case "jump":
                        await windowHandle.HoldKeyAsync(Keys.W, duration);
                        break;
                    case "punch":
                        await windowHandle.HoldKeyAsync(Keys.Space, duration);
                        break;
                }
            }
        }
    }
}
