using System;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX.Interface
{
    public partial class ScriptForm : Form
    {
        private bool saved;

        public ScriptForm()
        {
            InitializeComponent();
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            foreach (var s in Script.Commands)
                comboBox1.Items.Add(s.Name);
            comboBox1.SelectedIndex = 0;

            if (UserSettings.Current.ContinueScript != null)
                foreach (var s in UserSettings.Current.ContinueScript)
                    ScriptDraft.Items.Add(s);

            saved = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(Script.Commands[comboBox1.SelectedIndex].Code);

            sb.Append(" " + Argument1.Value.ToString());

            if (ScriptDraft.SelectedIndex != -1)
                ScriptDraft.Items.Insert(ScriptDraft.SelectedIndex + 1, sb.ToString());
            else
                ScriptDraft.Items.Add(sb.ToString());
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
            UserSettings.Current.ContinueScript = sc;
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
