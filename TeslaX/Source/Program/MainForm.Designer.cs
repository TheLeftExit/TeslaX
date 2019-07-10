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
            this.Windowed = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SimulateInput = new System.Windows.Forms.CheckBox();
            this.Debug = new System.Windows.Forms.CheckBox();
            this.BlockID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SkinColor = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RichPresence = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DistanceRight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.DistanceLeft = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.MinStop = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.MaxMove = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkinColor)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLeft)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMove)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(213, 190);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(131, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Windowed
            // 
            this.Windowed.AutoSize = true;
            this.Windowed.Checked = true;
            this.Windowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Windowed.Location = new System.Drawing.Point(6, 19);
            this.Windowed.Name = "Windowed";
            this.Windowed.Size = new System.Drawing.Size(77, 17);
            this.Windowed.TabIndex = 1;
            this.Windowed.Text = "Windowed";
            this.Windowed.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SimulateInput);
            this.groupBox1.Controls.Add(this.Debug);
            this.groupBox1.Location = new System.Drawing.Point(213, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug options";
            // 
            // SimulateInput
            // 
            this.SimulateInput.AutoSize = true;
            this.SimulateInput.Checked = true;
            this.SimulateInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SimulateInput.Location = new System.Drawing.Point(6, 42);
            this.SimulateInput.Name = "SimulateInput";
            this.SimulateInput.Size = new System.Drawing.Size(87, 17);
            this.SimulateInput.TabIndex = 1;
            this.SimulateInput.Text = "Disable input";
            this.SimulateInput.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.Debug.AutoSize = true;
            this.Debug.Checked = true;
            this.Debug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Debug.Location = new System.Drawing.Point(6, 19);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(112, 17);
            this.Debug.TabIndex = 0;
            this.Debug.Text = "Debug information";
            this.Debug.UseVisualStyleBackColor = true;
            // 
            // BlockID
            // 
            this.BlockID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlockID.FormattingEnabled = true;
            this.BlockID.Location = new System.Drawing.Point(81, 19);
            this.BlockID.Name = "BlockID";
            this.BlockID.Size = new System.Drawing.Size(108, 21);
            this.BlockID.TabIndex = 3;
            this.BlockID.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
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
            // SkinColor
            // 
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
            this.RichPresence.Location = new System.Drawing.Point(6, 19);
            this.RichPresence.Name = "RichPresence";
            this.RichPresence.Size = new System.Drawing.Size(59, 17);
            this.RichPresence.TabIndex = 0;
            this.RichPresence.Text = "Enable";
            this.RichPresence.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PNG file|*.png";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1_FileOk);
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
            this.DistanceRight.Value = new decimal(new int[] {
            38,
            0,
            0,
            0});
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
            this.DistanceLeft.Value = new decimal(new int[] {
            58,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.BlockID);
            this.groupBox3.Controls.Add(this.SkinColor);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 75);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Basic options";
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
            // MinStop
            // 
            this.MinStop.Location = new System.Drawing.Point(140, 19);
            this.MinStop.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MinStop.Name = "MinStop";
            this.MinStop.Size = new System.Drawing.Size(47, 20);
            this.MinStop.TabIndex = 11;
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
            // MaxMove
            // 
            this.MaxMove.Location = new System.Drawing.Point(140, 45);
            this.MaxMove.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxMove.Name = "MaxMove";
            this.MaxMove.Size = new System.Drawing.Size(47, 20);
            this.MaxMove.TabIndex = 13;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Target distance to block:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 222);
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
            ((System.ComponentModel.ISupportInitialize)(this.SkinColor)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLeft)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxMove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox SimulateInput;
        private System.Windows.Forms.CheckBox Debug;
        private System.Windows.Forms.ComboBox BlockID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown SkinColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox RichPresence;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
    }
}