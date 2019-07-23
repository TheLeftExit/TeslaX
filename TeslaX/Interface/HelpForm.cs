using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TheLeftExit.TeslaX.Properties;
using System.IO;

namespace TheLeftExit.TeslaX.Interface
{
    public partial class HelpForm : Form
    {
        private class Article
        {
            public string Name;
            public string Text;
        }

        private List<Article> Articles;

        public HelpForm()
        {
            InitializeComponent();

            
            string serialized = Resources.articles;
            Articles = JsonConvert.DeserializeObject<List<Article>>(serialized);
            foreach (var a in Articles)
                listBox1.Items.Add(a.Name);
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                richTextBox1.Text = Articles[listBox1.SelectedIndex].Text;
        }
    }
}
