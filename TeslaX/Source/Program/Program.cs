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
        public static void Main()
        {
            Ignorable.Load();
            Block.Load();
            Cracks.Load();
            Player.Load();

            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
