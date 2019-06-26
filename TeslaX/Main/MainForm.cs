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
            
            Task WorkThread = new Task(Worker.Init);
            if (!Worker.Busy)
            {
                Window.Windowed = checkBox1.Checked;
                Worker.Right = radioButton2.Checked || radioButton4.Checked;
                Worker.Down = radioButton3.Checked || radioButton4.Checked;
                WorkThread.Start();
                button1.Text = "Select area";
            }
            else
            {
                Worker.Busy = false;
            }
        }
    }
}
