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
            bool Testing = false;

            if (!Testing)
            {
                Application.EnableVisualStyles();
                Application.Run(new MainForm());
                return;
            }

            Ignorable.Load();
            Block.Load();
            Cracks.Load();
            Player.Load();

            Bitmap load = new Bitmap(@"C:/load/load.png");
            Screenshot shot = new Screenshot(load, 400, 355);
            Worker.Offset = new Point(1, 3);
            Worker.LastKnown = new Smooth<Point>(0, (ov, nv) => false);
            Worker.LastKnown.Value = new Point(496, 355);
            var p = shot.HasBlock(49, 0);
            ;
        }
    }
}
