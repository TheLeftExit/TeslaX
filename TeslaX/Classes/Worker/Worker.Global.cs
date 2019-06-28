using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public static partial class Worker
    {

        public static MainForm mainForm;
        public static bool Busy;

        public static Point Offset;
        public static Smooth<Point> LastKnown;
        public static bool Right;
    }
}
