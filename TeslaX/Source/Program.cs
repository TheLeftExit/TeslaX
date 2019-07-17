using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TeslaX
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
            return;
            /*
            var pf = new PlayerFinder(3);
            var ss = new Screenshot()
            {
                Picture = new Bitmap(@"C:/load/load.png")
            };

            var res = pf.HasPlayer(ss, 0, 0, true);

            ;
            */
        }
    }
}
