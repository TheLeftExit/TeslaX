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

            Application.EnableVisualStyles();
            Worker.mainForm = new MainForm();
            Application.Run(Worker.mainForm);
        }
    }
}
