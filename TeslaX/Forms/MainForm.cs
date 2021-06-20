using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using RTPackConverter;

namespace TheLeftExit.TeslaX
{
    public partial class MainForm : Form
    {
        private readonly string cfgpath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\teslax.cfg";

        private readonly string gamepath =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Growtopia";

        private Worker worker;
        private PersistentSettings settings;
        private List<DecodedItem> items;

        private void EnableEverything(bool enable)
        {
            blockIDbutton.Enabled = enable;
            detectbutton.Enabled = enable;
            debugbutton.Enabled = enable;
            startbutton.Enabled = enable;
            startbutton.Text = enable ? "Start" : "Stop";
        }
        public MainForm()
        {
            InitializeComponent();

            // Loading settings
            settings = new PersistentSettings(cfgpath);

            // Loading items.dat
            using (FileStream itemsdat = File.OpenRead(gamepath + @"\cache\items.dat"))
            {
                var decoder = new ItemDecoder(itemsdat);

                var header = decoder.ReadHeader();

                items = new List<DecodedItem>(header.ItemCount);
                for (int i = 0; i < header.ItemCount; i++)
                    items.Add(decoder.DecodeItem());
            }

            EnableEverything(false);
        }

        public void InvokeStatusUpdate(string newStatus) =>
            Invoke((MethodInvoker)(() => toolStripStatusLabel1.Text = newStatus));

        public string BlockToString(short blockID) =>
            items.Find(x => x.ItemID == blockID).Name;

        public void Panic() => Invoke((MethodInvoker)(async () =>
        {
            toolStripStatusLabel1.Text = "Aborting...";
            EnableEverything(false);
            await worker.Stop();
            toolStripStatusLabel1.Text = "Grotopia was closed.";
        }));

        private async void button1_Click(object sender, EventArgs e)
        {
            var selectedItem = await OpenItemSelector();
            if (selectedItem == null) {
                blockIDbutton.Text = "any";
                return;
            }
            var sourceImage = TextureDecoder.ConvertRTPACKFile(gamepath + @"\game\" + selectedItem.Texture);
            blockIDbutton.Image = sourceImage.Clone(
                new Rectangle(selectedItem.TextureRealX * 32, selectedItem.TextureRealY * 32, 32, 32),
                sourceImage.PixelFormat);
            settings.User.BlockID = (short)selectedItem.ItemID;
            blockIDbutton.Text = "";
        }

        private async Task<DecodedItem> OpenItemSelector()
        {
            //if (!await ConfirmWorker())
            //    return null;
            var worldblocks = await worker.WorldScanAsync();
            var itemlist = worldblocks.Select(x => items.Find(y => y.ItemID == x)).ToList();
            using (var itemSelectorForm = new ItemSelector(itemlist))
            {
                itemSelectorForm.ShowDialog();
                return itemSelectorForm.Result;
            }
        }

        // Fully initializes the worker.
        // Hard requirement: PersistentSettings are loaded.
        // Soft requirement: single instance of Growtopia is open and a world is loaded.
        private void workerSpawner_DoWork(object sender, DoWorkEventArgs e)
        {
            // Retrieving Growtopia process
            var growtopiaQuery = Process.GetProcessesByName("Growtopia");
            if(growtopiaQuery.Count() == 0)
            {
                e.Result = "Growtopia isn't open.";
                return;
            }
            if(growtopiaQuery.Count() > 1)
            {
                e.Result = "More than one instance of Growtopia open.";
                return;
            }
            Process growtopia = growtopiaQuery.Single();

            // Checking if address cache is up to date, and reinitializing it if not
            long gamever = File.GetCreationTimeUtc(growtopia.MainModule.FileName).Ticks;
            bool upToDate = settings.AddressCache.ContainsKey("gamever") && settings.AddressCache["gamever"] == gamever;
            if (!upToDate)
                settings.AddressCache = new Dictionary<string, long> { { "gamever", gamever } };

            // Getting Worker ready
            worker = new Worker(growtopia, InvokeStatusUpdate, BlockToString, Panic, settings.AddressCache);
            if (!upToDate) // This can throw if our Process isn't Growtopia, the game hasn't loaded a world yet, or the new game version lacks RTTI.
                try
                {
                    settings.AddressCache = worker.GenerateAddressCacheAsync().Result;
                }
                catch (Exception ex)
                {
                    e.Result = ex.Message;
                    return;
                }

            // This instruction is never executed! WTF?
            e.Result = "OK";
        }

        private void workerSpawner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string res = (string)e.Result;
            if (res != "OK")
            {
                toolStripStatusLabel1.Text = res;
                return;
            }
            toolStripStatusLabel1.Text = "Successfully initialized.";
            initializebutton.Enabled = false;
            EnableEverything(true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            workerSpawner.RunWorkerAsync();
        }

        private async void startbutton_Click(object sender, EventArgs e)
        {
            if (workerWorker.IsBusy)
            {
                await worker.Stop();
                EnableEverything(true);
            }
            else
            {
                EnableEverything(false);
                workerWorker.RunWorkerAsync(workerAction.Break);
            }
        }

        private void workerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch ((workerAction)e.Argument)
            {
                case workerAction.Break:
                    worker.BreakAsync(settings.User.BlockID, settings.Tuning).Wait();
                    break;
                case workerAction.Debug:
                    worker.DebugAsync().Wait();
                    break;
                case workerAction.Detect:
                    worker.BlockAheadToStatus();
                    break;
            }
        }

        private enum workerAction
        {
            Break = 0,
            Detect = 1,
            Debug = 2,
            ExecuteScript = 3
        }

        private void workerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableEverything(true);
        }

        private void detectbutton_Click(object sender, EventArgs e)
        {
            workerWorker.RunWorkerAsync(workerAction.Detect);
        }

        private void debugbutton_Click(object sender, EventArgs e)
        {
            EnableEverything(false);
            workerWorker.RunWorkerAsync(workerAction.Debug);
        }
    }
}

/*
    States:

    "Not initialized"
        Initialize enabled as "Attach" => workerSpawner.RunIdle => "Idle"
        Button disabled as "Not initialized"
        Everything else disabled
    "Idle"
        Initialize enabled as "Detach"
        Button enabled as "Start"
        Everything else enabled
    "Working"
        Initialize disabled
        Button enabled as "Stop"
        Everything else disabled
    on panic
        if "Idle"
            Detach & enter "Not initialized"
        if "Working"
            Curse user's relatives
            Await worker.Stop
            Detach & enter "Not initialized"

*/