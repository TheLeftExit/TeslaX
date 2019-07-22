using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class ScriptForm : Form
    {
        private Label[] labels;
        private NumericUpDown[] arguments;
        private int parameters;
        private bool saved;

        public ScriptForm()
        {
            InitializeComponent();
            labels = new Label[] { Info1, Info2, Info3 };
            arguments = new NumericUpDown[] { Argument1, Argument2, Argument3 };
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            parameters = Script.Commands[comboBox1.SelectedIndex].Parameters.Length;

            for(int i = 0; i < 3; i++)
            {
                if(parameters > i)
                {
                    labels[i].Visible = true;
                    labels[i].Text = Script.Commands[comboBox1.SelectedIndex].Parameters[i];
                    arguments[i].Visible = true;
                    arguments[i].Value = 0;
                }
                else
                {
                    labels[i].Visible = false;
                    arguments[i].Visible = false;
                }
            }
        }

        private void ListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(Script.Commands[comboBox1.SelectedIndex].Code);

            for (int i = 0; i < parameters; i++)
                sb.Append(" " + arguments[i].Value.ToString());

            if (ScriptDraft.SelectedIndex != -1)
                ScriptDraft.Items.Insert(ScriptDraft.SelectedIndex + 1, sb.ToString());
            else
                ScriptDraft.Items.Add(sb.ToString());
            saved = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int selected = ScriptDraft.SelectedIndex;
            if(selected != -1)
                ScriptDraft.Items.RemoveAt(selected);
            if(ScriptDraft.Items.Count > 0)
                ScriptDraft.SelectedIndex = Math.Min(ScriptDraft.Items.Count - 1, selected);
            saved = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            StringCollection sc = new StringCollection();
            foreach(string s in ScriptDraft.Items)
            {
                sc.Add(s);
            }
            UserSettings.Current.ContinueScript = sc;
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
