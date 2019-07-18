namespace TeslaX
{
    partial class ScriptForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ScriptDraft = new System.Windows.Forms.ListBox();
            this.Info1 = new System.Windows.Forms.Label();
            this.Argument1 = new System.Windows.Forms.NumericUpDown();
            this.Info2 = new System.Windows.Forms.Label();
            this.Argument2 = new System.Windows.Forms.NumericUpDown();
            this.Info3 = new System.Windows.Forms.Label();
            this.Argument3 = new System.Windows.Forms.NumericUpDown();
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Argument1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Argument2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Argument3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // ScriptDraft
            // 
            this.ScriptDraft.FormattingEnabled = true;
            this.ScriptDraft.Location = new System.Drawing.Point(6, 48);
            this.ScriptDraft.Name = "ScriptDraft";
            this.ScriptDraft.Size = new System.Drawing.Size(156, 95);
            this.ScriptDraft.TabIndex = 1;
            this.ScriptDraft.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListBox1_KeyPress);
            // 
            // Info1
            // 
            this.Info1.AutoSize = true;
            this.Info1.Location = new System.Drawing.Point(6, 56);
            this.Info1.Name = "Info1";
            this.Info1.Size = new System.Drawing.Size(64, 13);
            this.Info1.TabIndex = 3;
            this.Info1.Text = "Argument 1:";
            // 
            // Argument1
            // 
            this.Argument1.Location = new System.Drawing.Point(6, 72);
            this.Argument1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Argument1.Name = "Argument1";
            this.Argument1.Size = new System.Drawing.Size(101, 20);
            this.Argument1.TabIndex = 4;
            this.Argument1.ThousandsSeparator = true;
            // 
            // Info2
            // 
            this.Info2.AutoSize = true;
            this.Info2.Location = new System.Drawing.Point(6, 95);
            this.Info2.Name = "Info2";
            this.Info2.Size = new System.Drawing.Size(64, 13);
            this.Info2.TabIndex = 3;
            this.Info2.Text = "Argument 2:";
            // 
            // Argument2
            // 
            this.Argument2.Location = new System.Drawing.Point(6, 111);
            this.Argument2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Argument2.Name = "Argument2";
            this.Argument2.Size = new System.Drawing.Size(101, 20);
            this.Argument2.TabIndex = 4;
            this.Argument2.ThousandsSeparator = true;
            // 
            // Info3
            // 
            this.Info3.AutoSize = true;
            this.Info3.Location = new System.Drawing.Point(6, 134);
            this.Info3.Name = "Info3";
            this.Info3.Size = new System.Drawing.Size(64, 13);
            this.Info3.TabIndex = 3;
            this.Info3.Text = "Argument 3:";
            // 
            // Argument3
            // 
            this.Argument3.Location = new System.Drawing.Point(6, 150);
            this.Argument3.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Argument3.Name = "Argument3";
            this.Argument3.Size = new System.Drawing.Size(101, 20);
            this.Argument3.TabIndex = 4;
            this.Argument3.ThousandsSeparator = true;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(6, 19);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(87, 19);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 6;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(87, 149);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.Button3_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(6, 149);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.Button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Argument3);
            this.groupBox1.Controls.Add(this.Argument2);
            this.groupBox1.Controls.Add(this.Argument1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Info1);
            this.groupBox1.Controls.Add(this.Info2);
            this.groupBox1.Controls.Add(this.Info3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 182);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select command";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AddButton);
            this.groupBox2.Controls.Add(this.ScriptDraft);
            this.groupBox2.Controls.Add(this.CancelButton);
            this.groupBox2.Controls.Add(this.RemoveButton);
            this.groupBox2.Controls.Add(this.SaveButton);
            this.groupBox2.Location = new System.Drawing.Point(141, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 182);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Script";
            // 
            // ScriptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 201);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScriptForm";
            this.Text = "Script builder";
            this.Load += new System.EventHandler(this.ScriptForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Argument1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Argument2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Argument3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListBox ScriptDraft;
        private System.Windows.Forms.Label Info1;
        private System.Windows.Forms.NumericUpDown Argument1;
        private System.Windows.Forms.Label Info2;
        private System.Windows.Forms.NumericUpDown Argument2;
        private System.Windows.Forms.Label Info3;
        private System.Windows.Forms.NumericUpDown Argument3;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}