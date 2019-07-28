using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using TheLeftExit.TeslaX.Entities;
using TheLeftExit.TeslaX.Helpers;
using TheLeftExit.TeslaX.Interface;
using TheLeftExit.TeslaX.Properties;
using System.Linq;
using System.Threading;

namespace TheLeftExit.TeslaX.Static
{
    internal static partial class Workflow
    {
        public static bool Active = false;

        private static int rows = 0;

        // Return value: whether to attempt breaking again.
        public static bool Start()
        {
            // Retrieve ProcessHandle.
            var ps = Process.GetProcessesByName("Growtopia");
            if (ps.Length == 0)
            {
                App.Status = "Growtopia isn't open.";
                return false;
            }
            else if (ps.Length > 1)
            {
                App.Status = "More than one Growtopia detected.";
                return false;
            }

            Process process = ps.Single();
            ProcessHandle handle = process.GetHandle();

            while (Active)
            {
                var next = handle.GetNextBlockInfo();
                App.Status = (next?.Distance.ToString() ?? "Unknown!");
                Thread.Sleep(10);
            }

            App.Status = "Finished detecting!";

            return false;
        }
    }
}
