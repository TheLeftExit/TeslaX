using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TheLeftExit.TeslaX.Static;
using Message = TheLeftExit.TeslaX.Static.Message;

namespace TheLeftExit.TeslaX.Interface
{
    public partial class NewMainForm : Form
    {
        // ToolStripDropDownButton doesn't share ancestors with Controls that have Enabled property.
        // Therefore we can't enable/disable all of them in a single foreach.
        private void enableSettings(bool value)
        {
            propertyGrid.Enabled = value;
            topMenuStrip.Enabled = value;
            blockSelector.Enabled = value;
        }

        public NewMainForm()
        {
            InitializeComponent();

            // Loading Discord.
            Discord.Update(DiscordStatus.Idle);

            // Linking form to app logic.
            propertyGrid.SelectedObject = UserSettings.Current;
            App.StatusLabel = statusLabel;

            // Enabling block selector.
            blockSelector.Text = App.Sprites[0].Name;
            foreach (var item in App.Sprites)
            {
                if (item.Name != "Custom")
                    blockSelector.DropDownItems.Add(item.Name, null, (EventHandler)delegate
                    {
                        UserSettings.Current.SelectedBlock = App.Sprites.FindIndex(x => x.Name == item.Name);
                        blockSelector.Text = item.Name;
                    });
                else
                    blockSelector.DropDownItems.Add(item.Name, null, (EventHandler)delegate
                    {
                        using (var dlg = new OpenFileDialog())
                        {
                            dlg.Filter = "PNG files|*.png";
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                Bitmap newcustom = new Bitmap(dlg.FileName);
                                if (newcustom.Height == 32 && newcustom.Width % 32 == 0)
                                {
                                    App.CustomSprite.Dispose();
                                    App.CustomSprite = newcustom;
                                    UserSettings.Current.SelectedBlock = App.Sprites.Count - 1;
                                    blockSelector.Text = item.Name;
                                    return;
                                }
                                else
                                {
                                    newcustom.Dispose();
                                    Message.NoCustomSpritesheet();
                                }
                            }

                            UserSettings.Current.SelectedBlock = 0;
                            blockSelector.Text = App.Sprites[0].Name;
                        }
                    });
            }
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new ScriptForm().ShowDialog();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new TextureForm().ShowDialog();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!Workflow.Active)
            {
                startButton.Text = "Stop";
                enableSettings(false);
                new Thread(() =>
                {
                    while (Workflow.Start()) ; // This'll loop workflow/script if Continue is set.
                    Workflow.Active = false;
                    Discord.Update(DiscordStatus.Idle);
                    Invoke((MethodInvoker)delegate
                    {
                        startButton.Enabled = true;
                        startButton.Text = "Start";
                        enableSettings(true);
                    });
                }).Start();
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
                UserSettings.Current = new UserSettings();
                blockSelector.Text = App.Sprites[0].Name;
            }
        }
    }
}
