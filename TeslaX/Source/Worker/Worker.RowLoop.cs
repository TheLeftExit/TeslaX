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
using TeslaX.Properties;

namespace TeslaX
{
    public static partial class Worker
    {
        private static void RowLoop()
        {
            Distance = new Smooth<int>(DistanceSpikeLength, DistanceSpikeCondition);

            if (Settings.Default.SimulateInput)
                Input.Initialize();

            #region [Debug] Initializing.
            DebugForm debugForm = new DebugForm();
            StringBuilder debugInfo = new StringBuilder();
            if (Settings.Default.Debug)
                new Thread(() => { debugForm.ShowDialog(); }).Start();
            #endregion

            if (Settings.Default.RichPresence)
                Discord.ToBreaking();

            Busy = true;
            while (Busy)
            {
                using (shot = new Screenshot(
                    LastKnown.Value.X + (Right ? -BlocksBehind * 32 : -BlocksAhead * 32), 
                    LastKnown.Value.Y, 
                    (BlocksAhead + BlocksBehind + 1) * 32, 
                    64))
                {
                    #region [Debug] Clearing.
                    if (Settings.Default.Debug)
                    {
                        debugInfo.Clear();
                    }
                    #endregion

                    if (!shot.SetOffset())
                    {
                        #region [Debug] Appending Offset and updating.
                        if (Settings.Default.Debug)
                        {
                            debugInfo.AppendLine("Offset:   N/A");
                            debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                        }
                        #endregion
                        continue;
                    }
                    else
                    #region [Debug] Appending Offset.
                    if (Settings.Default.Debug)
                        debugInfo.AppendLine("Offset: " + Offset.ToString());
                    #endregion

                    // Only recorded for debug purposes. Will put in real use or restructure, this is ugly.
                    bool pfound = shot.SetPlayer();

                    #region [Debug] Appending Player, Direction and updating.
                    if (Settings.Default.Debug)
                    {
                        debugInfo.AppendLine("Player: " + LastKnown.ToString() + (pfound ? "" : "[?]"));
                        debugInfo.AppendLine("Direction: " + (Right ? "Right" : "Left"));
                    }
                    #endregion
                    
                    shot.SetDistance();

                    int OldDistance = Distance;

                    if(NewDistance - Distance >= 24 || NewDistance <= Distance)
                        Distance.Value = NewDistance;
                    if (Distance - OldDistance >= 24)
                        Settings.Default.TotalBlocks++;

                    // Feeding the Distance value into the input machine. It'll take it from here.
                    if (Settings.Default.SimulateInput)
                        Input.Distance = Distance;
                    
                    #region [Debug] Appending more stuff and updating.
                    if (Settings.Default.Debug)
                    {
                        debugInfo.AppendLine("NewDistance: " + (NewDistance == -1 ? "N/A" : NewDistance.ToString()));
                        debugInfo.AppendLine("Distance: " + (Distance == -1 ? "N/A" : Distance.ToString()));
                        debugInfo.AppendLine("TotalBlocks: " + Settings.Default.TotalBlocks.ToString());
                        debugForm.UpdateDebugInfo(shot.Location.Add(Window.Location), debugInfo.ToString());
                    }
                    #endregion
                }
            }

            if (Settings.Default.RichPresence)
                Discord.ToIdle();
            else
                Discord.Hide();
        }
    }
}