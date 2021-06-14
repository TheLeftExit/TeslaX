using System.Collections.Generic;
using System.Windows.Forms;

namespace TheLeftExit.TeslaX.Static
{
    internal static class Script
    {
        public struct CommandInfo
        {
            public string Name;
            public string Code;
            public CommandInfo(string name, string code)
            {
                Name = name;
                Code = code;
            }
        }

        public static CommandInfo[] Commands = new CommandInfo[]
        {
            new CommandInfo("Wait", "wait"),
            new CommandInfo("Move forward", "forward"),
            new CommandInfo("Move backward", "backward"),
            new CommandInfo("Jump", "jump"),
            new CommandInfo("Punch", "punch")
        };

        private struct Command
        {
            public string Name;
            public ushort Duration;

            public static readonly Command Empty = new Command()
            {
                Name = ""
            };

            public Command(string s)
            {
                string[] words = s.Split(' ');
                List<ushort> args = new List<ushort>();
                Name = words[0];
                if (!ushort.TryParse(words[1], out Duration))
                    this = Empty;
            }
        }

        public static void ExecuteScript(this ProcessHandle window, bool right)
        {
            if (UserSettings.Current.ContinueScript == null)
                return;
            foreach (string s in UserSettings.Current.ContinueScript)
            {
                Command cmd = new Command(s);
                switch (cmd.Name)
                {
                    case "wait":
                        window.HoldKey(Keys.None, cmd.Duration);
                        break;
                    case "forward":
                        window.HoldKey(right ? Keys.D : Keys.A, cmd.Duration);
                        break;
                    case "backward":
                        window.HoldKey(right ? Keys.A : Keys.D, cmd.Duration);
                        break;
                    case "jump":
                        window.HoldKey(Keys.W, cmd.Duration);
                        break;
                    case "punch":
                        window.HoldKey(Keys.Space, cmd.Duration);
                        break;
                }
            }
        }
    }
}
