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
    public class Worker
    {
        /// <summary>
        /// Stores Growtopia window handle.
        /// </summary>
        public static HwndObject Window;

        /// <summary>
        /// Window location and size.
        /// </summary>
        public static Rectangle WindowPos;

        /// <summary>
        /// Starting seeking area, relative to window.
        /// </summary>
        public static Rectangle SeekArea;

        /// <summary>
        /// Form used to set SeekArea based on user input.
        /// </summary>
        class SlaveForm: Form
        {
            PictureBox pictureBox;

            Bitmap shot;
            bool isMouseDown;

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
                isMouseDown = false;
            }

            private void onMouseDown(object Sender, MouseEventArgs e)
            {
                SeekArea.X = Cursor.Position.X - WindowPos.X;
                SeekArea.Y = Cursor.Position.Y - WindowPos.Y;
                isMouseDown = true;
            }
            /*
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
                SeekArea.Width = Cursor.Position.X - SeekArea.X;
                SeekArea.Height = Cursor.Position.Y - SeekArea.Y;
                this.Close();
            }

            public SlaveForm()
            {
                this.Load += onLoad;
            }
        }

        /// <summary>
        /// Returns a Bitmap of part of the screen. Resource costly.
        /// </summary>
        public static Bitmap Screenshot(int x, int y, int w, int l)
        {
            Bitmap res = new Bitmap(w, l);
            using(Graphics g = Graphics.FromImage(res))
            {
                g.CopyFromScreen(x, y, 0, 0, res.Size);
            }
            return res;
        }

        public static void Init()
        {
            Window = HwndObject.GetWindowByTitle("Growtopia");
            WindowPos = new Rectangle(Window.Location, Window.Size);
            SlaveForm slaveForm = new SlaveForm();
            slaveForm.ShowDialog();
            MessageBox.Show(SeekArea.Location.ToString()+' '+SeekArea.Size.ToString());
            // at this point SeekArea exists, allowing to seek for LK with screenshots
            // we need predicates though
            /*
             * Known bug: if Growtopia is windowed, WindowPos misses it by a small offset.
             * Should not limit functionality, but is nasty and ought to be fixed at some point.
             */
        }
    }
}
