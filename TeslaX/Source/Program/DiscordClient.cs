using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;

namespace TeslaX
{
    public static class Discord
    {
        private static DiscordRpcClient client;
        private static bool initialized;

        private static void Initialize()
        {
            client = new DiscordRpcClient("597852128232341506");
            client.Initialize();
            initialized = true;
        }

        public static void ToIdle()
        {
            if (!initialized)
                Initialize();

            client.SetPresence(new RichPresence()
            {
                State = "Not breaking anything",
                Timestamps = Timestamps.Now
            });
        }

        public static void ToBreaking()
        {
            if (!initialized)
                Initialize();

            client.SetPresence(new RichPresence()
            {
                State = Settings.SimulateInput ? "Breaking " + Settings.CurrentBlock.PluralName : "Debugging",
                Assets = new Assets
                {
                    LargeImageKey = Settings.CurrentBlock.Code
                },
                Timestamps = Timestamps.Now
            });
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
