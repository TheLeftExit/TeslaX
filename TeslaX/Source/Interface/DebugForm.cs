using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using TeslaX.Properties;

namespace TeslaX
{
    [System.ComponentModel.DesignerCategory("")]
    public class DebugForm : Form
    {
        public Label DebugLabel;
        public Button DebugBlockButton;
        public Button DebugPlayerButton;

        public static readonly int bh = 10;
        public static readonly int ph = 6;
        public static readonly int fh = 90;

        public DebugForm()
        {
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            Size = new Size((Settings.Default.BlocksAhead + Settings.Default.BlocksBehind + 1) * 32, fh);

            DebugLabel = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font(FontFamily.GenericMonospace, 7.5f),
                //ForeColor = Color.White

            };
            Controls.Add(DebugLabel);

            DebugBlockButton = new Button
            {
                Enabled = false,
                Size = new Size(32, bh),
                Location = new Point(0, this.Size.Height - bh)
            };
            Controls.Add(DebugBlockButton);
            DebugBlockButton.BringToFront();

            DebugPlayerButton = new Button
            {
                Enabled = false,
                Size = new Size(32, ph),
                Location = new Point(0, this.Size.Height - ph)
            };
            Controls.Add(DebugPlayerButton);
            DebugPlayerButton.BringToFront();

            
            //BackColor = Color.Black;
            //TransparencyKey = BackColor;
        }

        public void Done()
        {
            Invoke((MethodInvoker)delegate
            {
                Close();
            });
        }
    }
}