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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DisableInput = new System.Windows.Forms.CheckBox();
            this.Debug = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RichPresence = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaxMove = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.MinStop = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.DistanceRight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.DistanceLeft = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BlockSelector = new System.Windows.Forms.ComboBox();
            this.SkinColor = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Windowed = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLeft)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkinColor)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(280, 190);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(64, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DisableInput);
            this.groupBox1.Controls.Add(this.Debug);
            this.groupBox1.Location = new System.Drawing.Point(213, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug options";
            // 
            // DisableInput
            // 
            this.DisableInput.AutoSize = true;
            this.DisableInput.Checked = global::TeslaX.Properties.Settings.Default.SimulateInput;
            this.DisableInput.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "SimulateInput", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DisableInput.Location = new System.Drawing.Point(6, 42);
            this.DisableInput.Name = "DisableInput";
            this.DisableInput.Size = new System.Drawing.Size(87, 17);
            this.DisableInput.TabIndex = 1;
            this.DisableInput.Text = "Disable input";
            this.DisableInput.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.Debug.AutoSize = true;
            this.Debug.Checked = global::TeslaX.Properties.Settings.Default.Debug;
            this.Debug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Debug.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TeslaX.Properties.Settings.Default, "Debug", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RichPresence);
            this.groupBox4.Location = new System.Drawing.Point(213, 88);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(131, 44);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Discord Rich Presence";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.MaxMove);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.MinStop);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.DistanceRight);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.DistanceLeft);
            this.groupBox2.Location = new System.Drawing.Point(12, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(195, 120);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Movement tuning";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Target distance to block:";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.BlockSelector);
            this.groupBox3.Controls.Add(this.SkinColor);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 75);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Basic options";
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
            this.BlockSelector.SelectedIndexChanged += new System.EventHandler(this.BlockSelector_SelectedIndexChanged);
            // 
            // SkinColor
            // 
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
            this.SkinColor.BackColor = global::TeslaX.Game.SkinColors[(int)global::TeslaX.Properties.Settings.Default.SkinColor];
            this.SkinColor.ValueChanged += new System.EventHandler(this.SkinColor_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Windowed);
            this.groupBox6.Location = new System.Drawing.Point(213, 138);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(131, 46);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Other settings";
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Textures";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Texture_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 222);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::TeslaX.Properties.Resources.pickaxe;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "TeslaX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLeft)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkinColor)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox DisableInput;
        private System.Windows.Forms.CheckBox Debug;
        private System.Windows.Forms.ComboBox BlockSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown SkinColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox RichPresence;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown DistanceRight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown DistanceLeft;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox Windowed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown MaxMove;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown MinStop;
        private System.Windows.Forms.Button button1;
    }
}