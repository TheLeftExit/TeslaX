using System.Drawing;
using System.Windows.Forms;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX.Interface
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
            Size = new Size((UserSettings.Current.BlocksAhead + UserSettings.Current.BlocksBehind + 1) * 32, fh);

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
                Location = new Point(0, Size.Height - bh)
            };
            Controls.Add(DebugBlockButton);
            DebugBlockButton.BringToFront();

            DebugPlayerButton = new Button
            {
                Enabled = false,
                Size = new Size(32, ph),
                Location = new Point(0, Size.Height - ph)
            };
            Controls.Add(DebugPlayerButton);
            DebugPlayerButton.BringToFront();


            //BackColor = Color.Black;
            //TransparencyKey = BackColor;
        }

        public void Done() =>
            Invoke((MethodInvoker)Close);
    }
}