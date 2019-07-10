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
            Distance = new Smooth<int>(TechSettings.DistanceSpikeLength, TechSettings.DistanceSpikeCondition);

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
                    LastKnown.Value.X + (Right ? -TechSettings.BlocksBehind * 32 : -TechSettings.BlocksAhead * 32), 
                    LastKnown.Value.Y, 
                    (TechSettings.BlocksAhead + TechSettings.BlocksBehind + 1) * 32, 
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

                    Distance.Value = NewDistance;

                    // Feeding the Distance value into the input machine. It'll take it from here.
                    if (Settings.Default.SimulateInput)
                        Input.Distance = Distance;
                    
                    #region [Debug] Appending NewDistance, Distance, Keydown and updating.
                    if (Settings.Default.Debug)
                    {
                        debugInfo.AppendLine("NewDistance: " + (NewDistance == -1 ? "N/A" : NewDistance.ToString()));
                        debugInfo.AppendLine("Distance: " + (Distance == -1 ? "N/A" : Distance.ToString()));
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