
namespace TheLeftExit.TeslaX
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.startbutton = new System.Windows.Forms.ToolStripButton();
            this.blockIDbutton = new System.Windows.Forms.Button();
            this.workerSpawner = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.doorIDbutton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.scriptbutton = new System.Windows.Forms.Button();
            this.startovercheckbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.detectbutton = new System.Windows.Forms.Button();
            this.debugbutton = new System.Windows.Forms.Button();
            this.adjustbutton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.initializebutton = new System.Windows.Forms.Button();
            this.workerWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.startbutton});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 168);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(408, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel1.Text = "Welcome!";
            // 
            // startbutton
            // 
            this.startbutton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.startbutton.Name = "startbutton";
            this.startbutton.Size = new System.Drawing.Size(35, 20);
            this.startbutton.Text = "Start";
            this.startbutton.Click += new System.EventHandler(this.startbutton_Click);
            // 
            // blockIDbutton
            // 
            this.blockIDbutton.Location = new System.Drawing.Point(54, 12);
            this.blockIDbutton.Name = "blockIDbutton";
            this.blockIDbutton.Size = new System.Drawing.Size(42, 42);
            this.blockIDbutton.TabIndex = 3;
            this.blockIDbutton.Text = "any";
            this.blockIDbutton.UseVisualStyleBackColor = true;
            this.blockIDbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // workerSpawner
            // 
            this.workerSpawner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerSpawner_DoWork);
            this.workerSpawner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerSpawner_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Break";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "until none found in range.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Once";
            // 
            // doorIDbutton
            // 
            this.doorIDbutton.Enabled = false;
            this.doorIDbutton.Location = new System.Drawing.Point(54, 60);
            this.doorIDbutton.Name = "doorIDbutton";
            this.doorIDbutton.Size = new System.Drawing.Size(42, 42);
            this.doorIDbutton.TabIndex = 3;
            this.doorIDbutton.Text = "none";
            this.doorIDbutton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "are detected, run a";
            // 
            // scriptbutton
            // 
            this.scriptbutton.Enabled = false;
            this.scriptbutton.Location = new System.Drawing.Point(211, 70);
            this.scriptbutton.Name = "scriptbutton";
            this.scriptbutton.Size = new System.Drawing.Size(49, 23);
            this.scriptbutton.TabIndex = 12;
            this.scriptbutton.Text = "script";
            this.scriptbutton.UseVisualStyleBackColor = true;
            // 
            // startovercheckbox
            // 
            this.startovercheckbox.AutoSize = true;
            this.startovercheckbox.Enabled = false;
            this.startovercheckbox.Location = new System.Drawing.Point(326, 73);
            this.startovercheckbox.Name = "startovercheckbox";
            this.startovercheckbox.Size = new System.Drawing.Size(78, 19);
            this.startovercheckbox.TabIndex = 13;
            this.startovercheckbox.Text = "start over.";
            this.startovercheckbox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "and then";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Or just";
            // 
            // detectbutton
            // 
            this.detectbutton.Location = new System.Drawing.Point(60, 108);
            this.detectbutton.Name = "detectbutton";
            this.detectbutton.Size = new System.Drawing.Size(75, 23);
            this.detectbutton.TabIndex = 16;
            this.detectbutton.Text = "Detect";
            this.detectbutton.UseVisualStyleBackColor = true;
            this.detectbutton.Click += new System.EventHandler(this.detectbutton_Click);
            // 
            // debugbutton
            // 
            this.debugbutton.Location = new System.Drawing.Point(141, 108);
            this.debugbutton.Name = "debugbutton";
            this.debugbutton.Size = new System.Drawing.Size(75, 23);
            this.debugbutton.TabIndex = 17;
            this.debugbutton.Text = "Debug";
            this.debugbutton.UseVisualStyleBackColor = true;
            this.debugbutton.Click += new System.EventHandler(this.debugbutton_Click);
            // 
            // adjustbutton
            // 
            this.adjustbutton.Location = new System.Drawing.Point(222, 108);
            this.adjustbutton.Name = "adjustbutton";
            this.adjustbutton.Size = new System.Drawing.Size(75, 23);
            this.adjustbutton.TabIndex = 18;
            this.adjustbutton.Text = "Adjust";
            this.adjustbutton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "But before any of that,";
            // 
            // initializebutton
            // 
            this.initializebutton.Location = new System.Drawing.Point(143, 137);
            this.initializebutton.Name = "initializebutton";
            this.initializebutton.Size = new System.Drawing.Size(75, 23);
            this.initializebutton.TabIndex = 20;
            this.initializebutton.Text = "Initialize";
            this.initializebutton.UseVisualStyleBackColor = true;
            this.initializebutton.Click += new System.EventHandler(this.button7_Click);
            // 
            // workerWorker
            // 
            this.workerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerWorker_DoWork);
            this.workerWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 190);
            this.Controls.Add(this.initializebutton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.adjustbutton);
            this.Controls.Add(this.debugbutton);
            this.Controls.Add(this.detectbutton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startovercheckbox);
            this.Controls.Add(this.scriptbutton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doorIDbutton);
            this.Controls.Add(this.blockIDbutton);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "TeslaX (ver. 2.0-pre1)";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button blockIDbutton;
        private System.ComponentModel.BackgroundWorker workerSpawner;
        private System.Windows.Forms.ToolStripButton startbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button doorIDbutton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button scriptbutton;
        private System.Windows.Forms.CheckBox startovercheckbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button detectbutton;
        private System.Windows.Forms.Button debugbutton;
        private System.Windows.Forms.Button adjustbutton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button initializebutton;
        private System.ComponentModel.BackgroundWorker workerWorker;
    }
}