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
    public partial class TextureForm : Form
    {
        public TextureForm()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            UserSettings.Current.TextureInfo = !UserSettings.Current.TextureInfo;
            TextureForm_Load(null, null);
        }

        private void TextureForm_Load(object sender, EventArgs e)
        {
            if (UserSettings.Current.TextureInfo)
                Size = new Size(428, 220);
            else
                Size = new Size(428, 86);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (Texture.Delete())
                Message.TextureDeleted();
            else
                Message.TextureAlreadyDeleted();
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            Texture.Replace();
            Message.TextureSwapped();
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            Texture.Restore();
            Message.TextureRestored();
        }
    }
}
