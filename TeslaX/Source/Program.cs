using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.IO;
using Newtonsoft.Json;

namespace TeslaX
{
    public class Program
    {
        private static string cfgpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\teslax.cfg";

        [STAThread]
        public static void Main()
        {
            // Loading settings.
            try
            { UserSettings.Current = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(cfgpath)); }
            catch
            { UserSettings.Current = new UserSettings(); }

            // Running the form
            Application.EnableVisualStyles();
            Application.Run(new NewMainForm());

            // Exporting settings.
            File.WriteAllText(cfgpath, JsonConvert.SerializeObject(UserSettings.Current));
        }
    }
}
