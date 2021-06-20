using System;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using TheLeftExit.TeslaX;

namespace TheLeftExit.TeslaX.Interface
{
    public partial class ScriptForm : Form
    {
        private bool saved;

        public StringCollection Script;

        public ScriptForm(StringCollection s = null)
        {
            InitializeComponent();
            Script = s;
            saved = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string cmd = comboBox1.SelectedText + " " + Argument1.Value;
            if (ScriptDraft.SelectedIndex != -1)
                ScriptDraft.Items.Insert(ScriptDraft.SelectedIndex + 1, cmd);
            else
                ScriptDraft.Items.Add(cmd);
            saved = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int selected = ScriptDraft.SelectedIndex;
            if (selected != -1)
                ScriptDraft.Items.RemoveAt(selected);
            if (ScriptDraft.Items.Count > 0)
                ScriptDraft.SelectedIndex = Math.Min(ScriptDraft.Items.Count - 1, selected);
            saved = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            StringCollection sc = new StringCollection();
            foreach (string s in ScriptDraft.Items)
            {
                sc.Add(s);
            }
            Script = sc;
            MessageBox.Show("Script saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            saved = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                Close();
                return;
            }
            var res = MessageBox.Show("You have unsaved changes. Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
                Close();
        }
    }
}
