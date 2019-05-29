using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeslaX.Main
{
    public class ProgramTest
    {
        public static void Main()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int x in Worker.EligibleBetween(100, 200, 0))
                sb.AppendLine(x.ToString());
            MessageBox.Show(sb.ToString());
        }
    }
}
