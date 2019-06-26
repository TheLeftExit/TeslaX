using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WindowScrape;
using HwndObject = WindowScrape.Types.HwndObject;

namespace TeslaX
{
    public partial class Worker
    {
        // Form used to set SeekArea based on user input.
        // To be deprecated in favor of initial full window search.
        class SlaveForm : Form
        {
            PictureBox pictureBox;

            private void onLoad(object Sender, EventArgs e)
            {
                Bitmap shot = Worker.Screenshot(WindowPos.X, WindowPos.Y, WindowPos.Width, WindowPos.Height);
                this.Size = WindowPos.Size;
                this.Location = WindowPos.Location;
                pictureBox = new PictureBox();
                pictureBox.Image = shot;
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.MouseUp += onMouseUp;
                this.Controls.Add(pictureBox);
                this.FormBorderStyle = FormBorderStyle.None;
            }

            private void onMouseUp(object Sender, MouseEventArgs e)
            {
                SeekArea.X = Cursor.Position.X - WindowPos.X - 32;
                SeekArea.Width = 64;
                SeekArea.Y = Cursor.Position.Y - WindowPos.Y - 48;
                SeekArea.Height = 96;
                this.Close();
            }

            public SlaveForm()
            {
                this.Load += onLoad;
            }
        }
    }
}
