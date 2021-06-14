using System;
using System.Windows.Forms;
using System.Diagnostics;
using TheLeftExit.TeslaX.Interface;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX
{
    public static class Program
    {
        public static string GameDirectory = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Growtopia";
        public static Process Growtopia;
        
        [STAThread]
        public static void Main()
        {
            // Loading settings.
            UserSettings.Load();

            // Running the form
            Application.EnableVisualStyles();
            Application.Run(new NewMainForm());

            // Exporting settings.
            UserSettings.Save();
        }
    }
}
