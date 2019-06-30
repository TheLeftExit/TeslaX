using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TeslaX
{
    [System.ComponentModel.DesignerCategory("")]
    public class DebugForm: Form
    {
        public Label DebugLabel;
        public Button DebugBlockButton;
        public Button DebugPlayerButton;

        public static readonly int bh = 10;
        public static readonly int ph = 6;

        public DebugForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.Size = new Size((Settings.BlocksAhead + Settings.BlocksBehind + 1) * 32, 80);

            DebugLabel = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font(FontFamily.GenericMonospace, 7)
            };
            this.Controls.Add(DebugLabel);

            DebugBlockButton = new Button
            {
                Enabled = false,
                Size = new Size(32, bh),
                Location = new Point(0, this.Size.Height - bh)
            };
            this.Controls.Add(DebugBlockButton);
            DebugBlockButton.BringToFront();

            DebugPlayerButton = new Button
            {
                Enabled = false,
                Size = new Size(32, ph),
                Location = new Point(0, this.Size.Height - ph)
            };
            this.Controls.Add(DebugPlayerButton);
            DebugPlayerButton.BringToFront();
        }

        public void UpdateDebugInfo(Point BottomLeft, string s)
        {
            if (Application.OpenForms[this.Text] != null)
                this.Invoke((MethodInvoker)delegate
                {
                    this.Location = BottomLeft.Add(0, -this.Size.Height);
                    DebugLabel.Text = s;
                    DebugPlayerButton.Location = new Point(Worker.LastKnown.Value.X - Worker.shot.X, this.Size.Height - ph - 1);
                    DebugBlockButton.Location = new Point(Worker.Distance.Value != -1 ? Worker.LastKnown.Value.X + (Worker.Right ? 1 : -1) * (Worker.Distance.Value + 32) + Window.X - this.Location.X : -33, this.Size.Height - bh - 1);
                    if (!Worker.Busy)
                        this.Close();
                });
        }
    }
}
