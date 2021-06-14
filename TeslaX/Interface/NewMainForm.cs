using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Collections.Specialized;
using TheLeftExit.TeslaX.Properties;
using TheLeftExit.TeslaX.Static;

namespace TheLeftExit.TeslaX.Interface
{
    public partial class NewMainForm : Form
    {
        // ToolStripDropDownButton doesn't share ancestors with Controls that have Enabled property.
        // Therefore we can't enable/disable all of them in a single foreach.
        private void EnableSettings(bool value)
        {
            propertyGrid.Enabled = value;
            topMenuStrip.Enabled = value;
        }

        // Disabling ALT in our application (since we're using it as a shortcut for other things).
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) == Keys.Alt)
                return true;
            else
                return base.ProcessDialogKey(keyData);
        }

        bool ToStop = false;

        public NewMainForm()
        {
            InitializeComponent();

            // Setting icon.
            Icon = Resources.pickaxe;

            // Linking form to app logic.
            propertyGrid.SelectedObject = UserSettings.Current;
            App.StatusLabel = statusLabel;

            // Checking ContinueScript for version derivation.
            // Checking if there are unsupported commands.
            foreach (var s in UserSettings.Current.ContinueScript ?? new StringCollection())
            {
                bool supported = false;
                foreach (var c in Script.Commands)
                    if (s.Split(' ')[0] == c.Code)
                    {
                        supported = true;
                        break;
                    }
                if (!supported)
                {
                    MessageBox.Show("Your script contains unsupported instructions. It will be replaced with an empty one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UserSettings.Current.ContinueScript = null;
                }
            }

            // Starting listener.
            Thread thread = new Thread(() =>
            {
                while (!ToStop)
                {
                    if (Keyboard.IsKeyDown(Key.S))
                    {
                        if (Keyboard.IsKeyDown(Key.LeftAlt))
                        {
                            if (!Workflow.Active)
                            {
                                startButton.GetCurrentParent().Invoke((MethodInvoker)startButton.PerformClick);
                                while (Keyboard.IsKeyDown(Key.S))
                                    Thread.Sleep(50);
                            }
                        }
                        else
                        {
                            if (Workflow.Active)
                                startButton.GetCurrentParent().Invoke((MethodInvoker)startButton.PerformClick);
                        }
                    }
                    Thread.Sleep(50);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void NewMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ToStop = true;
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new ScriptForm().ShowDialog();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!Workflow.Active)
            {
                startButton.Text = "Stop";
                EnableSettings(false);
                Thread thread = new Thread(() =>
                {
                    Workflow.Active = true;
                    while (Workflow.Start()) ; // This'll loop workflow/script if Continue is set.
                    Workflow.Active = false;
                    Invoke((MethodInvoker)delegate
                    {
                        startButton.Enabled = true;
                        startButton.Text = "Start";
                        EnableSettings(true);
                    });
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                startButton.Enabled = false;
                Workflow.Active = false;
            }
        }

        private void ResetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("All settings will be set to defaults. Are you absolutely sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                UserSettings.Erase();
                propertyGrid.SelectedObject = UserSettings.Current;
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            Workflow.BlockAheadToStatus();
        }
    }
}
