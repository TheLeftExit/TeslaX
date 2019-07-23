using System;
using System.Windows.Forms;
using TheLeftExit.TeslaX.Interface;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX
{
    public class Program
    {
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
