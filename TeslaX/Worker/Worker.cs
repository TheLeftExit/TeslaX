using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLeftExit.TeslaX
{
    public partial class Worker
    {
        private Process growtopia;
        private IntPtr memoryHandle { get => growtopia.HasExited ? IntPtr.Zero : growtopia.Handle; }
        private IntPtr windowHandle { get => growtopia.HasExited ? IntPtr.Zero : growtopia.MainWindowHandle; }
        private Action<string> updateStatus;
        private Func<short, string> blockToString;
        private Action panic;

        public Worker(Process process, Action<string> writeToStatus, Func<short, string> blockParser, Action onProcessExit, Dictionary<string, long> addressCache)
        {
            growtopia = process;
            panic = onProcessExit;
            updateStatus = writeToStatus;
            blockToString = blockParser;

            process.Exited += (s, e) => panic();

            // MainForm will always pass an initialized Dictionary containing at least one key ("gamever").
            // If it's the only key, it always runs GenerateAddressCacheAsync afterward.
            if (addressCache.Count > 1)
            {
                var ph = new PointerHunter(growtopia, addressCache);
                playerXAddress = ph.GetPlayerX();
                playerYAddress = ph.GetPlayerY();
                playerDirAddress = ph.GetPlayerDir();
                worldBaseAddress = ph.GetWorldData();
            }
        }

        public async Task<Dictionary<string, long>> GenerateAddressCacheAsync()
        {
            var ph = new PointerHunter(growtopia);

            await Task.Run(() =>
            {
                playerXAddress = ph.GetPlayerX();
                playerYAddress = ph.GetPlayerY();
                playerDirAddress = ph.GetPlayerDir();
                worldBaseAddress = ph.GetWorldData();
            });

            return ph.AddressCache;
        }

        public bool ReportProcess() => growtopia.HasExited;
    }
}
