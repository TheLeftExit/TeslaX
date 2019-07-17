using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeslaX.Properties;

namespace TeslaX
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Commands to be ran on click.
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!Workflow.Working)
            {
                StartButton.Text = "Stop";
                new Task(() =>
                {
                    Workflow.Start();
                    StartButton.Text = "Start";
                }).Start();
            }
            else
                Workflow.Working = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Workflow.Working = false;
            Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void SkinColor_ValueChanged(object sender, EventArgs e)
        {
            Color newbg = Game.SkinColors[Convert.ToInt32(SkinColor.Value)];
            SkinColor.BackColor = newbg;
        }

        private void Texture_Click(object sender, EventArgs e)
        {
            (new TextureForm()).ShowDialog();
        }

        private void BlockSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlockSelector.SelectedIndex == BlockSelector.Items.Count - 1)
            {
                Bitmap src;
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Select spritesheet image";
                    dlg.Filter = "PNG files (*.png)|*.png";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // No safety check. If you've gone out of your way to load a non-image, deal with it.
                        src = new Bitmap(dlg.FileName);
                        App.CustomSprite = src;
                        return;
                    }

                    BlockSelector.SelectedIndex = 0;
                    return;
                }
            }
        }
    }
}
