using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RTPackConverter;
using System.Diagnostics;

namespace TheLeftExit.TeslaX
{
    public partial class ItemSelector : Form
    {
        public DecodedItem Result;

        private List<DecodedItem> items;
        private string halfpath = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"\Growtopia";

        private Dictionary<string, Bitmap> DecodedTextures;
        private Bitmap DecodeTexture(string fname)
        {
            if (!DecodedTextures.ContainsKey(fname))
            DecodedTextures.Add(fname, TextureDecoder.ConvertRTPACKFile(halfpath + @"\game\" + fname));
            return DecodedTextures[fname];
        }

        public ItemSelector(List<DecodedItem> itemSelection)
        {
            InitializeComponent();

            items = itemSelection;

            DecodedTextures = new Dictionary<string, Bitmap>();

            listView1.BeginUpdate();

            var il = new ImageList();
            il.ImageSize = new Size(32, 32);
            var lvis = new ListViewItem[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                var sourceImage = DecodeTexture(item.Texture);
                il.Images.Add(item.Name, sourceImage.Clone(
                    new Rectangle(item.TextureRealX * 32, item.TextureRealY * 32, 32, 32),
                    sourceImage.PixelFormat)
                    );

                lvis[i] = new ListViewItem(item.Name, item.Name);
            }
            listView1.LargeImageList = il;
            listView1.Items.AddRange(lvis);

            foreach (var img in DecodedTextures)
                img.Value.Dispose();

            listView1.EndUpdate();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            Result = items.Find(x => x.Name == listView1.SelectedItems[0].Text);
            Close();
        }
    }
}
