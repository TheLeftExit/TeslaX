using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TeslaX.Main
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Worker.mainForm = new MainForm();
            Application.Run(Worker.mainForm);
            //var bitmap = new Bitmap(@"C:\Users\nikit\Desktop\TeslaX\output.png");
            //Worker.SeekArea = new Rectangle(new Point(0,0),bitmap.Size);
            //Worker.Right = true;
            //var q = Worker.GetPlayer(bitmap);
            //;
        }
    }
}
