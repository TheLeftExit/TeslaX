using System;
using System.Windows.Forms;
using System.Diagnostics;
using TheLeftExit.TeslaX.Interface;
using TheLeftExit.TeslaX;

namespace TheLeftExit.TeslaX
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
