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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.basicOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.BlockSelector = new System.Windows.Forms.ComboBox();
            this.TextureButton = new System.Windows.Forms.Button();
            this.ScriptButton = new System.Windows.Forms.Button();
            this.rowsGroupBox = new System.Windows.Forms.GroupBox();
            this.MRSButton = new System.Windows.Forms.CheckBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.basicOptionsGroupBox.SuspendLayout();
            this.rowsGroupBox.SuspendLayout();
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
            // basicOptionsGroupBox
            // 
            this.basicOptionsGroupBox.Controls.Add(this.label1);
            this.basicOptionsGroupBox.Controls.Add(this.BlockSelector);
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
            // TextureButton
            // 
            this.TextureButton.Location = new System.Drawing.Point(47, 143);
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
            this.rowsGroupBox.Controls.Add(this.MRSButton);
            this.rowsGroupBox.Controls.Add(this.ScriptButton);
            this.rowsGroupBox.Location = new System.Drawing.Point(213, 93);
            this.rowsGroupBox.Name = "rowsGroupBox";
            this.rowsGroupBox.Size = new System.Drawing.Size(140, 76);
            this.rowsGroupBox.TabIndex = 16;
            this.rowsGroupBox.TabStop = false;
            this.rowsGroupBox.Text = "Multiple rows support";
            // 
            // MRSButton
            // 
            this.MRSButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MRSButton.Location = new System.Drawing.Point(6, 22);
            this.MRSButton.Name = "MRSButton";
            this.MRSButton.Size = new System.Drawing.Size(59, 17);
            this.MRSButton.TabIndex = 16;
            this.MRSButton.Text = "Enable";
            this.MRSButton.UseVisualStyleBackColor = true;
            this.MRSButton.CheckedChanged += new System.EventHandler(this.MRSButton_CheckedChanged);
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
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(437, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(243, 201);
            this.propertyGrid1.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 221);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.TextureButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.rowsGroupBox);
            this.Controls.Add(this.basicOptionsGroupBox);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::TeslaX.Properties.Resources.pickaxe;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TeslaX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.basicOptionsGroupBox.ResumeLayout(false);
            this.basicOptionsGroupBox.PerformLayout();
            this.rowsGroupBox.ResumeLayout(false);
            this.rowsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox BlockSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox basicOptionsGroupBox;
        private System.Windows.Forms.Button TextureButton;
        private Button ScriptButton;
        private GroupBox rowsGroupBox;
        private CheckBox MRSButton;
        private Button StartButton;
        private Label StatusLabel;
        private PropertyGrid propertyGrid1;
    }
}