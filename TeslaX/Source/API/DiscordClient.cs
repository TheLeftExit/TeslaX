using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using TeslaX.Properties;

namespace TeslaX
{
    enum DiscordStatus
    {
        Disabled = 0,
        Idle = 1,
        Breaking = 2,
        Advancing = 3
    }

    static class Discord
    {
        private static DiscordRpcClient client;
        private static bool initialized;

        private static void Initialize()
        {
            client = new DiscordRpcClient("597852128232341506");
            client.Initialize();
            initialized = true;
        }

        public static void Update(DiscordStatus status, int rows = 0)
        {
            if (!Settings.Default.RichPresence)
                return;
            if (!initialized && status != DiscordStatus.Disabled)
                Initialize();
            switch (status)
            {
                case DiscordStatus.Idle:
                    client.SetPresence(new RichPresence()
                    {
                        State = "Not breaking anything",
                        Timestamps = Timestamps.Now
                    });
                    break;
                case DiscordStatus.Breaking:
                    client.SetPresence(new RichPresence()
                    {
                        State = Settings.Default.DebugMode ? "Debugging" : "Breaking " + App.Sprites[Settings.Default.SelectedBlock].Name,
                        Details = Settings.Default.DebugMode ? null : "Broke " + rows.ToString() + " rows so far.",
                        Assets = new Assets
                        {
                            LargeImageKey = App.Sprites[Settings.Default.SelectedBlock].AssetName
                        },
                        Timestamps = Timestamps.Now
                    });
                    break;
                case DiscordStatus.Advancing:
                    client.SetPresence(new RichPresence()
                    {
                        State = "Moving to the next row.",
                        Details = "Broke " + rows.ToString() + " rows so far.",
                        Assets = new Assets
                        {
                            LargeImageKey = App.Sprites[Settings.Default.SelectedBlock].AssetName
                        },
                        Timestamps = Timestamps.Now
                    });
                    break;
            }
        }

        public static void Hide()
        {
            if (initialized)
                client.Deinitialize();
        }

        public static void Dispose()
        {
            if (client != null)
                client.Dispose();
        }
    }
}
