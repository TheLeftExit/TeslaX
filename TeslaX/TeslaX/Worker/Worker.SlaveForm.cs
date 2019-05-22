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
        /// <summary>
        /// Form used to set SeekArea based on user input.
        /// </summary>
        class SlaveForm : Form
        {
            PictureBox pictureBox;

            Bitmap shot;
            // bool isMouseDown;

            private void onLoad(object Sender, EventArgs e)
            {
                shot = Worker.Screenshot(Window.Location.X, Window.Location.Y, Window.Size.Width, Window.Size.Height);
                this.Size = Window.Size;
                this.Location = Window.Location;
                pictureBox = new PictureBox();
                pictureBox.Image = shot;
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.MouseDown += onMouseDown;
                pictureBox.MouseUp += onMouseUp;
                //pictureBox.MouseMove += onMouseMove;
                this.Controls.Add(pictureBox);
                this.FormBorderStyle = FormBorderStyle.None;
                // isMouseDown = false;
            }

            private void onMouseDown(object Sender, MouseEventArgs e)
            {
                SeekArea.X = Cursor.Position.X - WindowPos.X;
                SeekArea.Y = Cursor.Position.Y - WindowPos.Y;
                // isMouseDown = true;
            }
            /*
			// Doesn't seem to work.
            private void onMouseMove(object Sender, MouseEventArgs e)
            {
                if (isMouseDown)
                {
                    Bitmap newshot = shot;
                    Size newsize = new Size(Cursor.Position.X - WindowPos.X, Cursor.Position.Y - WindowPos.Y);
                    using(Graphics g = Graphics.FromImage(newshot))
                    {
                        g.DrawRectangle(Pens.DarkGray, new Rectangle(SeekArea.Location, newsize));
                    }
                }
            }
            */
            private void onMouseUp(object Sender, MouseEventArgs e)
            {
                SeekArea.Width = Cursor.Position.X - SeekArea.X - WindowPos.X;
                SeekArea.Height = Cursor.Position.Y - SeekArea.Y - WindowPos.Y;
                this.Close();
            }

            public SlaveForm()
            {
                this.Load += onLoad;
            }
        }
    }
}
