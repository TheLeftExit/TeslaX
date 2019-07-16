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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!Worker.Busy)
            {
                Settings.Default.Debug = Debug.Checked;
                Settings.Default.SimulateInput = !SimulateInput.Checked;
                Window.Windowed = Windowed.Checked;
                Settings.Default.SkinColor = Convert.ToInt32(SkinColor.Value);
                BlockInfo.BlockID = (BlockID.SelectedIndex == BlockID.Items.Count -1) ? -1 : BlockID.SelectedIndex;
                Settings.Default.RichPresence = RichPresence.Checked;
                Settings.Default.DistanceLeft = Convert.ToInt32(DistanceLeft.Value);
                Settings.Default.DistanceRight = Convert.ToInt32(DistanceRight.Value);
                Settings.Default.MaxMove = Convert.ToInt32(MaxMove.Value);
                Settings.Default.MinStop = Convert.ToInt32(MinStop.Value);

                Ignorable.Load();
                Block.Load();
                Cracks.Load();
                Player.Load();

                if (Worker.Init())
                    StartButton.Text = "Working";
            }
            else
            {
                Worker.Busy = false;
                StartButton.Text = "Start";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Worker.Busy = false;
            Discord.Dispose();
            Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var x in BlockInfo.Blocks)
                BlockID.Items.Add(x.SingleName);
            BlockID.Items.Add("Custom spritesheet");
            BlockID.SelectedItem = BlockID.Items[0];

            SimulateInput.Checked = Settings.Default.SimulateInput;
            Debug.Checked = Settings.Default.Debug;
            DistanceLeft.Value = Settings.Default.DistanceLeft;
            DistanceRight.Value = Settings.Default.DistanceRight;
            RichPresence.Checked = Settings.Default.RichPresence;
            SkinColor.Value = Settings.Default.SkinColor;
            MinStop.Value = Settings.Default.MinStop;
            MaxMove.Value = Settings.Default.MaxMove;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlockID.SelectedIndex == BlockID.Items.Count - 1)
                CustomSpriteSelect.ShowDialog();
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Bitmap src;
            try
            {
                src = new Bitmap(CustomSpriteSelect.FileName);
                if (src.Width % 32 > 0 || src.Height != 32)
                    throw new Exception();
            }
            catch
            {
                Message.NoCustomSpritesheet();
                BlockID.SelectedIndex = 0;
                return;
            }

            BlockInfo.CustomBlock.Source = src;
            BlockInfo.BlockID = -1;
        }

        private void SkinColor_ValueChanged(object sender, EventArgs e)
        {
            Color newbg = Player.SkinColors[Convert.ToInt32(SkinColor.Value)];
            SkinColor.BackColor = newbg;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string msg = "Press Yes to remove textures from /game."+Environment.NewLine+
                "Press No to replace textures in /cache/game with modified ones."+Environment.NewLine+
                "Press Cancel to restore original textures in /cache/game";
            var res = MessageBox.Show(msg, "Texture swap", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                if (Texture.Delete())
                    Message.TextureDeleted();
                else
                    Message.TextureAlreadyDeleted();
            }
            if(res == DialogResult.No)
            {
                Texture.Replace();
                Message.TextureSwapped();
            }
            if(res == DialogResult.Cancel)
            {
                Texture.Restore();
                Message.TextureRestored();
            }
        }
    }
}
