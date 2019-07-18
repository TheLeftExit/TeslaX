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

        private bool Started = false;

        // Commands to be ran on click.
        private async void Button1_Click(object sender, EventArgs e)
        {
            if (!Workflow.Working)
            {
                Started = true;
                StartButton.Text = "Stop";
                StartButton.Enabled = false;
                new Task(() =>
                {
                    Workflow.Start();
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartButton.Text = "Start";
                        StartButton.Enabled = true;
                    });
                    Started = false;
                }).Start();
                while (Started && !Workflow.Working)
                    await Task.Delay(50);
                StartButton.Enabled = true;
            }
            else
            {
                StartButton.Enabled = false;
                Workflow.Working = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Workflow.Working = false;
            Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var b in App.Sprites)
                BlockSelector.Items.Add(b.Name);
            BlockSelector.SelectedIndex = 0;
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
                        Settings.Default.SelectedBlock = BlockSelector.SelectedIndex;
                        App.CustomSprite = src;
                        return;
                    }
                    BlockSelector.SelectedIndex = 0;
                }
            }

            Settings.Default.SelectedBlock = BlockSelector.SelectedIndex;
        }
    }
}
