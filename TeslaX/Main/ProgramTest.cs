using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace TeslaX.Main
{
    public class ProgramTest
    {
        public static void Main()
        {
            var f = new Form();
            Point oo = new Point(-1, -1);
            var sw = Stopwatch.StartNew();
            var t = new Task(() =>
            {
                while (true)
                {
                    //Worker.CurrentShot = Worker.Screenshot(0, 0, 1366, 768);
                    Worker.CurrentShot = Worker.Screenshot(500, 250, 100, 100);
                    Worker.Offset = Worker.GetOffset(Worker.CurrentShot).ify();
                    sw.Stop();
                    double change = sw.ElapsedMilliseconds;//Math.Abs(oo.X - Worker.Offset.X);
                    change /= sw.Elapsed.Milliseconds;
                    change *= 1000;
                    sw.Restart();
                    f.Invoke((MethodInvoker)delegate {
                        f.Text = Worker.Offset.ToString() + ' ' + change.ToString();
                    });
                    oo = Worker.Offset;
                }
            });
            void Foo(object Sender, EventArgs e)
            {
                t.Start();
            }
            f.Load += Foo;
            Application.Run(f);
            /*
             Speed = Math.Abs(Offset.X - NewOffset.X);
                if ((Right && (NewOffset.X > Offset.X)) || (!Right && NewOffset.X < Offset.X))
                    Speed = 32 - Speed;
                Speed += Math.Abs(LastKnown.X - NewLastKnown.X);
                Speed /= (double)Watch.ElapsedMilliseconds / 1000;
                Watch.Restart();
                // Might throw this out because precision isn't great.
            */
        }
    }
}
