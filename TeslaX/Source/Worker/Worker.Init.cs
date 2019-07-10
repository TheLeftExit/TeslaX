using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public partial class Worker
    {
        public static bool Init()
        {
            if (!Window.Load())
            {
                Message.NoWindow();
                return false;
            }

            using (Screenshot firstshot = new Screenshot())
            {
                if (!firstshot.SetNewOffset())
                {
                    Message.NoNewOffset();
                    return false;
                }

                if (!firstshot.SetNewPlayer())
                {
                    Message.NoNewPlayer();
                    return false;
                }
            }

            new Thread(RowLoop).Start();
            return true;
        }
    }
}
