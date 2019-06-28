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
        }

        public void UpdateDebugInfo(Point BottomLeft, string s)
        {
            if (Application.OpenForms[this.Text] != null)
                this.Invoke((MethodInvoker)delegate
                {
                    this.Location = BottomLeft.Add(0, -this.Size.Height);
                    DebugLabel.Text = s;
                    if (!Worker.Busy)
                        this.Close();
                });
        }
    }
}
