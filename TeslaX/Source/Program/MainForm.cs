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
                Settings.BlockID = (comboBox1.SelectedIndex == comboBox1.Items.Count -1) ? -1 : comboBox1.SelectedIndex;
                Settings.RichPresence = checkBox8.Checked;

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
            Discord.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var x in Settings.Blocks)
                comboBox1.Items.Add(x.SingleName);
            comboBox1.Items.Add("Custom spritesheet");
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox1.Items.Count - 1)
                openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // This feels messy.
            Bitmap src;
            try
            {
                src = new Bitmap(openFileDialog1.FileName);
                if (src.Width % 32 > 0 || src.Height != 32)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Could not load custom spritesheet.");
                comboBox1.SelectedIndex = 0;
                return;
            }

            MessageBox.Show("Custom spritesheet successfully loaded.");
            Settings.CustomBlock.Source = src;
            Settings.BlockID = -1;
        }
    }
}
