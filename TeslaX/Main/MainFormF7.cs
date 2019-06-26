using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeslaX
{
    partial class MainForm
    {
        public void Restore(int state)
        {
            switch (state)
            {
                case 0:
                    button1.Text = "Start";
                    this.Text = "TeslaX";
                    break;
                case 1:
                    button1.Text = "Working";
                    break;
                // to be deprecated
                case 2:
                    button1.Text = "Select area";
                    break;
            }
        }

        public void Log(string s)
        {
            this.Text = s;
        }
    }
}
