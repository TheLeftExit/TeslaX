using DiscordRPC;

namespace TheLeftExit.TeslaX.Static
{
    internal enum DiscordStatus
    {
        Disabled = 0,
        Idle = 1,
        Breaking = 2,
        Advancing = 3,
        Debugging = 4
    }

    internal static class Discord
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
            if (!UserSettings.Current.RichPresence)
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
                        State = UserSettings.Current.DebugMode ? "Debugging." : "Breaking.",
                        Details = UserSettings.Current.DebugMode ? null : "Broke " + rows.ToString() + " rows so far.",
                        Assets = new Assets
                        {
                            //LargeImageKey = App.Sprites[UserSettings.Current.SelectedBlock].AssetName
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
                            //LargeImageKey = App.Sprites[UserSettings.Current.SelectedBlock].AssetName
                        },
                        Timestamps = Timestamps.Now
                    });
                    break;
                case DiscordStatus.Debugging:
                    client.SetPresence(new RichPresence()
                    {
                        State = "Testing detection.",
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
