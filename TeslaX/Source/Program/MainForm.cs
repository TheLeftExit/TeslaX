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
                Settings.Debug = checkBox2.Checked;
                Settings.SimulateInput = !checkBox3.Checked;
                Window.Windowed = checkBox1.Checked;
                Settings.SkinColor = Convert.ToInt32(numericUpDown1.Value);
                Settings.BlockID = comboBox1.SelectedIndex;

                Ignorable.Load();
                Block.Load();
                Cracks.Load();
                Player.Load();

                if (Worker.Init())
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = comboBox1.Items[0];
        }
    }
}
