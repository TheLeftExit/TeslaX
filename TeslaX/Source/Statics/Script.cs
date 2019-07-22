using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeslaX.Properties;

namespace TeslaX
{
    static class Script
    {
        public struct CommandInfo
        {
            public string Name;
            public string Code;
            public string[] Parameters;
            public CommandInfo(string name, string code, params string[] parameters)
            {
                Name = name;
                Code = code;
                Parameters = parameters;
            }
        }

        public static CommandInfo[] Commands = new CommandInfo[]
        {
            new CommandInfo("Wait", "wait", "Duration"),
            new CommandInfo("Move left", "left", "Duration"),
            new CommandInfo("Move right", "right", "Duration"),
            new CommandInfo("Jump", "jump", "Duration"),
            new CommandInfo("Punch", "punch", "Duration")
        };

        struct Command
        {
            public string Name;
            private ushort[] arguments;
            public ushort this[int i] { get { return arguments[i]; } }

            public static readonly Command Empty = new Command()
            {
                arguments = new ushort[0],
                Name = ""
            };

            // Examples: "punch" "move 1" "var 0 100 200"
            public Command(string s)
            {
                string[] words = s.Split(' ');
                List<ushort> args = new List<ushort>();
                Name = words[0];
                for (int i = 1; i < words.Length; i++)
                {
                    ushort o;
                    if (ushort.TryParse(words[i], out o))
                        args.Add(o);
                    else
                    {
                        this = Empty;
                        return;
                    }
                }
                arguments = args.ToArray();
            }
        }

        public static void Execute(WindowManager window)
        {
            if (UserSettings.Current.ContinueScript == null)
                return;
            foreach (string s in UserSettings.Current.ContinueScript)
            {
                Command cmd = new Command(s);
                switch (cmd.Name)
                {
                    case "wait":
                        window.HoldKey(Keys.None, cmd[0]);
                        break;
                    case "left":
                        window.HoldKey(Keys.A, cmd[0]);
                        break;
                    case "right":
                        window.HoldKey(Keys.D, cmd[0]);
                        break;
                    case "jump":
                        window.HoldKey(Keys.W, cmd[0]);
                        break;
                    case "punch":
                        window.HoldKey(Keys.Space, cmd[0]);
                        break;
                }
                ;
            }
            ;
        }
    }
}
