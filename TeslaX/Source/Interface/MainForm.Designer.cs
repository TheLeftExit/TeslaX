using System.Windows.Forms;

namespace TeslaX
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.DisableInput = new System.Windows.Forms.CheckBox();
            this.Debug = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.discordGroupBox = new System.Windows.Forms.GroupBox();
            this.RichPresence = new System.Windows.Forms.CheckBox();
            this.movementGroupBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaxMove = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.MinStop = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.DistanceRight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.DistanceLeft = new System.Windows.Forms.NumericUpDown();
            this.basicOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.BlockSelector = new System.Windows.Forms.ComboBox();
            this.SkinColor = new System.Windows.Forms.NumericUpDown();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TextureButton = new System.Windows.Forms.Button();
            this.ScriptButton = new System.Windows.Forms.Button();
            this.rowsGroupBox = new System.Windows.Forms.GroupBox();
            this.MRSDelay = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.MRSButton = new System.Windows.Forms.CheckBox();
            this.otherGroupBox = new System.Windows.Forms.GroupBox();
            this.Windowed = new System.Windows.Forms.CheckBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.debugGroupBox.SuspendLayout();
            this.discordGroupBox.SuspendLayout();
            this.movementGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLeft)).BeginInit();
            this.basicOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkinColor)).BeginInit();
            this.settingsGroupBox.SuspendLayout();
            this.rowsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MRSDelay)).BeginInit();
            this.otherGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(213, 175);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(140, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.OnStartButtonClick);
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Controls.Add(this.DisableInput);
            this.debugGroupBox.Controls.Add(this.Debug);
            this.debugGroupBox.Location = new System.Drawing.Point(359, 12);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(131, 75);
            this.debugGroupBox.TabIndex = 2;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug options";
            // 
            // DisableInput
            // 
            this.DisableInput.AutoSize = true;
            this.DisableInput.Checked = global::TeslaX.Properties.Settings.Default.DebugMode;
            this.DisableInput.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "DebugMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DisableInput.Location = new System.Drawing.Point(6, 42);
            this.DisableInput.Name = "DisableInput";
            this.DisableInput.Size = new System.Drawing.Size(87, 17);
            this.DisableInput.TabIndex = 1;
            this.DisableInput.Text = "Debug mode";
            this.DisableInput.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.Debug.AutoSize = true;
            this.Debug.Checked = global::TeslaX.Properties.Settings.Default.DebugForm;
            this.Debug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Debug.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "DebugForm", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Debug.Location = new System.Drawing.Point(6, 19);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(112, 17);
            this.Debug.TabIndex = 0;
            this.Debug.Text = "Debug information";
            this.Debug.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select block:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Skin color:";
            // 
            // discordGroupBox
            // 
            this.discordGroupBox.Controls.Add(this.RichPresence);
            this.discordGroupBox.Location = new System.Drawing.Point(359, 93);
            this.discordGroupBox.Name = "discordGroupBox";
            this.discordGroupBox.Size = new System.Drawing.Size(131, 44);
            this.discordGroupBox.TabIndex = 7;
            this.discordGroupBox.TabStop = false;
            this.discordGroupBox.Text = "Discord Rich Presence";
            // 
            // RichPresence
            // 
            this.RichPresence.AutoSize = true;
            this.RichPresence.Checked = global::TeslaX.Properties.Settings.Default.RichPresence;
            this.RichPresence.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "RichPresence", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.RichPresence.Location = new System.Drawing.Point(6, 19);
            this.RichPresence.Name = "RichPresence";
            this.RichPresence.Size = new System.Drawing.Size(59, 17);
            this.RichPresence.TabIndex = 0;
            this.RichPresence.Text = "Enable";
            this.RichPresence.UseVisualStyleBackColor = true;
            // 
            // movementGroupBox
            // 
            this.movementGroupBox.Controls.Add(this.label7);
            this.movementGroupBox.Controls.Add(this.label6);
            this.movementGroupBox.Controls.Add(this.MaxMove);
            this.movementGroupBox.Controls.Add(this.label5);
            this.movementGroupBox.Controls.Add(this.MinStop);
            this.movementGroupBox.Controls.Add(this.label4);
            this.movementGroupBox.Controls.Add(this.DistanceRight);
            this.movementGroupBox.Controls.Add(this.label3);
            this.movementGroupBox.Controls.Add(this.DistanceLeft);
            this.movementGroupBox.Location = new System.Drawing.Point(12, 93);
            this.movementGroupBox.Name = "movementGroupBox";
            this.movementGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.movementGroupBox.Size = new System.Drawing.Size(195, 120);
            this.movementGroupBox.TabIndex = 8;
            this.movementGroupBox.TabStop = false;
            this.movementGroupBox.Text = "Movement tuning";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Target distance to block (pixels):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Max move duration (ms)";
            // 
            // MaxMove
            // 
            this.MaxMove.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TeslaX.Properties.Settings.Default, "MaxMove", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MaxMove.Location = new System.Drawing.Point(140, 45);
            this.MaxMove.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxMove.Name = "MaxMove";
            this.MaxMove.Size = new System.Drawing.Size(47, 20);
            this.MaxMove.TabIndex = 13;
            this.MaxMove.Value = global::TeslaX.Properties.Settings.Default.MaxMove;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Min stop duration (ms)";
            // 
            // MinStop
            // 
            this.MinStop.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TeslaX.Properties.Settings.Default, "MinStop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MinStop.Location = new System.Drawing.Point(140, 19);
            this.MinStop.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MinStop.Name = "MinStop";
            this.MinStop.Size = new System.Drawing.Size(47, 20);
            this.MinStop.TabIndex = 11;
            this.MinStop.Value = global::TeslaX.Properties.Settings.Default.MinStop;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Right";
            // 
            // DistanceRight
            // 
            this.DistanceRight.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TeslaX.Properties.Settings.Default, "DistanceRight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DistanceRight.Location = new System.Drawing.Point(140, 88);
            this.DistanceRight.Maximum = new decimal(new int[] {
            38,
            0,
            0,
            0});
            this.DistanceRight.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.DistanceRight.Name = "DistanceRight";
            this.DistanceRight.Size = new System.Drawing.Size(47, 20);
            this.DistanceRight.TabIndex = 10;
            this.DistanceRight.Value = global::TeslaX.Properties.Settings.Default.DistanceRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Left";
            // 
            // DistanceLeft
            // 
            this.DistanceLeft.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TeslaX.Properties.Settings.Default, "DistanceLeft", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DistanceLeft.Location = new System.Drawing.Point(37, 88);
            this.DistanceLeft.Maximum = new decimal(new int[] {
            58,
            0,
            0,
            0});
            this.DistanceLeft.Minimum = new decimal(new int[] {
            26,
            0,
            0,
            0});
            this.DistanceLeft.Name = "DistanceLeft";
            this.DistanceLeft.Size = new System.Drawing.Size(47, 20);
            this.DistanceLeft.TabIndex = 0;
            this.DistanceLeft.Value = global::TeslaX.Properties.Settings.Default.DistanceLeft;
            // 
            // basicOptionsGroupBox
            // 
            this.basicOptionsGroupBox.Controls.Add(this.label1);
            this.basicOptionsGroupBox.Controls.Add(this.BlockSelector);
            this.basicOptionsGroupBox.Controls.Add(this.SkinColor);
            this.basicOptionsGroupBox.Controls.Add(this.label2);
            this.basicOptionsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.basicOptionsGroupBox.Name = "basicOptionsGroupBox";
            this.basicOptionsGroupBox.Size = new System.Drawing.Size(195, 75);
            this.basicOptionsGroupBox.TabIndex = 9;
            this.basicOptionsGroupBox.TabStop = false;
            this.basicOptionsGroupBox.Text = "Basic options";
            // 
            // BlockSelector
            // 
            this.BlockSelector.DisplayMember = "Key";
            this.BlockSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlockSelector.FormattingEnabled = true;
            this.BlockSelector.Location = new System.Drawing.Point(81, 19);
            this.BlockSelector.Name = "BlockSelector";
            this.BlockSelector.Size = new System.Drawing.Size(108, 21);
            this.BlockSelector.TabIndex = 3;
            this.BlockSelector.ValueMember = "Key";
            this.BlockSelector.SelectedIndexChanged += new System.EventHandler(this.OnBlockSelectorChange);
            // 
            // SkinColor
            // 
            this.SkinColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(91)))), ((int)(((byte)(80)))));
            this.SkinColor.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TeslaX.Properties.Settings.Default, "SkinColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SkinColor.Location = new System.Drawing.Point(81, 46);
            this.SkinColor.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.SkinColor.Name = "SkinColor";
            this.SkinColor.ReadOnly = true;
            this.SkinColor.Size = new System.Drawing.Size(106, 20);
            this.SkinColor.TabIndex = 5;
            this.SkinColor.Value = global::TeslaX.Properties.Settings.Default.SkinColor;
            this.SkinColor.ValueChanged += new System.EventHandler(this.OnSkinColorChange);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.checkBox2);
            this.settingsGroupBox.Controls.Add(this.checkBox1);
            this.settingsGroupBox.Location = new System.Drawing.Point(213, 12);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(140, 75);
            this.settingsGroupBox.TabIndex = 13;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = global::TeslaX.Properties.Settings.Default.SimulatePunch;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "SimulatePunch", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox2.Location = new System.Drawing.Point(6, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(118, 17);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.Text = "Automate punching";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::TeslaX.Properties.Settings.Default.StopOnFull;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "StopOnFull", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(6, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Stop on full inventory";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TextureButton
            // 
            this.TextureButton.Location = new System.Drawing.Point(6, 41);
            this.TextureButton.Name = "TextureButton";
            this.TextureButton.Size = new System.Drawing.Size(119, 23);
            this.TextureButton.TabIndex = 14;
            this.TextureButton.Text = "Texture replacing";
            this.TextureButton.UseVisualStyleBackColor = true;
            this.TextureButton.Click += new System.EventHandler(this.OnTextureClick);
            // 
            // ScriptButton
            // 
            this.ScriptButton.Location = new System.Drawing.Point(68, 19);
            this.ScriptButton.Name = "ScriptButton";
            this.ScriptButton.Size = new System.Drawing.Size(66, 23);
            this.ScriptButton.TabIndex = 15;
            this.ScriptButton.Text = "Script";
            this.ScriptButton.UseVisualStyleBackColor = true;
            this.ScriptButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // rowsGroupBox
            // 
            this.rowsGroupBox.Controls.Add(this.MRSDelay);
            this.rowsGroupBox.Controls.Add(this.label8);
            this.rowsGroupBox.Controls.Add(this.MRSButton);
            this.rowsGroupBox.Controls.Add(this.ScriptButton);
            this.rowsGroupBox.Location = new System.Drawing.Point(213, 93);
            this.rowsGroupBox.Name = "rowsGroupBox";
            this.rowsGroupBox.Size = new System.Drawing.Size(140, 76);
            this.rowsGroupBox.TabIndex = 16;
            this.rowsGroupBox.TabStop = false;
            this.rowsGroupBox.Text = "Multiple rows support";
            // 
            // MRSDelay
            // 
            this.MRSDelay.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TeslaX.Properties.Settings.Default, "MRSDelay", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MRSDelay.Location = new System.Drawing.Point(68, 48);
            this.MRSDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.MRSDelay.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.MRSDelay.Name = "MRSDelay";
            this.MRSDelay.Size = new System.Drawing.Size(66, 20);
            this.MRSDelay.TabIndex = 18;
            this.MRSDelay.Value = global::TeslaX.Properties.Settings.Default.MRSDelay;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Delay (ms)";
            // 
            // MRSButton
            // 
            this.MRSButton.AutoSize = true;
            this.MRSButton.Checked = global::TeslaX.Properties.Settings.Default.Continue;
            this.MRSButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MRSButton.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "Continue", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MRSButton.Location = new System.Drawing.Point(6, 22);
            this.MRSButton.Name = "MRSButton";
            this.MRSButton.Size = new System.Drawing.Size(59, 17);
            this.MRSButton.TabIndex = 16;
            this.MRSButton.Text = "Enable";
            this.MRSButton.UseVisualStyleBackColor = true;
            this.MRSButton.CheckedChanged += new System.EventHandler(this.MRSButton_CheckedChanged);
            // 
            // otherGroupBox
            // 
            this.otherGroupBox.Controls.Add(this.Windowed);
            this.otherGroupBox.Controls.Add(this.TextureButton);
            this.otherGroupBox.Location = new System.Drawing.Point(359, 143);
            this.otherGroupBox.Name = "otherGroupBox";
            this.otherGroupBox.Size = new System.Drawing.Size(131, 70);
            this.otherGroupBox.TabIndex = 18;
            this.otherGroupBox.TabStop = false;
            this.otherGroupBox.Text = "Other";
            // 
            // Windowed
            // 
            this.Windowed.AutoSize = true;
            this.Windowed.Checked = global::TeslaX.Properties.Settings.Default.Windowed;
            this.Windowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Windowed.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "Windowed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Windowed.Location = new System.Drawing.Point(6, 19);
            this.Windowed.Name = "Windowed";
            this.Windowed.Size = new System.Drawing.Size(77, 17);
            this.Windowed.TabIndex = 1;
            this.Windowed.Text = "Windowed";
            this.Windowed.UseVisualStyleBackColor = true;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Location = new System.Drawing.Point(213, 201);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(140, 22);
            this.StatusLabel.TabIndex = 19;
            this.StatusLabel.Text = "Welcome.";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 222);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.otherGroupBox);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.debugGroupBox);
            this.Controls.Add(this.rowsGroupBox);
            this.Controls.Add(this.basicOptionsGroupBox);
            this.Controls.Add(this.movementGroupBox);
            this.Controls.Add(this.discordGroupBox);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::TeslaX.Properties.Resources.pickaxe;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TeslaX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.debugGroupBox.ResumeLayout(false);
            this.debugGroupBox.PerformLayout();
            this.discordGroupBox.ResumeLayout(false);
            this.discordGroupBox.PerformLayout();
            this.movementGroupBox.ResumeLayout(false);
            this.movementGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLeft)).EndInit();
            this.basicOptionsGroupBox.ResumeLayout(false);
            this.basicOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkinColor)).EndInit();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.rowsGroupBox.ResumeLayout(false);
            this.rowsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MRSDelay)).EndInit();
            this.otherGroupBox.ResumeLayout(false);
            this.otherGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox debugGroupBox;
        private System.Windows.Forms.CheckBox DisableInput;
        private System.Windows.Forms.CheckBox Debug;
        private System.Windows.Forms.ComboBox BlockSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown SkinColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox discordGroupBox;
        private System.Windows.Forms.CheckBox RichPresence;
        private System.Windows.Forms.GroupBox movementGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown DistanceRight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown DistanceLeft;
        private System.Windows.Forms.GroupBox basicOptionsGroupBox;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.CheckBox Windowed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown MaxMove;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown MinStop;
        private System.Windows.Forms.Button TextureButton;
        private Button ScriptButton;
        private GroupBox rowsGroupBox;
        private CheckBox MRSButton;
        private CheckBox checkBox2;
        private Button StartButton;
        private CheckBox checkBox1;
        private NumericUpDown MRSDelay;
        private Label label8;
        private GroupBox otherGroupBox;
        private Label StatusLabel;
    }
}