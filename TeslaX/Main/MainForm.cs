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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            if (!Worker.Busy)
            {
                Window.Windowed = checkBox1.Checked;
                Worker.Right = radioButton2.Checked || radioButton4.Checked;
                Worker.Down = radioButton3.Checked || radioButton4.Checked;
                if(Worker.Init())
                    button1.Text = "Working";
            }
            else
            {
                Worker.Busy = false;
                button1.Text = "Start";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Worker.Busy = false;
        }
    }
}
