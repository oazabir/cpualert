namespace CPUMonitor
{
    partial class KillProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KillProcessForm));
            this.IgnoreButton = new System.Windows.Forms.Button();
            this.TerminateButton = new System.Windows.Forms.Button();
            this.PostponeButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ProcessNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CPULabel = new System.Windows.Forms.Label();
            this.MemoryLabel = new System.Windows.Forms.Label();
            this.DummyTextBox = new System.Windows.Forms.TextBox();
            this.RestartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IgnoreButton
            // 
            this.IgnoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.IgnoreButton.Location = new System.Drawing.Point(234, 152);
            this.IgnoreButton.Name = "IgnoreButton";
            this.IgnoreButton.Size = new System.Drawing.Size(75, 23);
            this.IgnoreButton.TabIndex = 4;
            this.IgnoreButton.Text = "&Ignore";
            this.IgnoreButton.UseVisualStyleBackColor = true;
            this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
            // 
            // TerminateButton
            // 
            this.TerminateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TerminateButton.Location = new System.Drawing.Point(72, 152);
            this.TerminateButton.Name = "TerminateButton";
            this.TerminateButton.Size = new System.Drawing.Size(75, 23);
            this.TerminateButton.TabIndex = 2;
            this.TerminateButton.Text = "&Close";
            this.TerminateButton.UseVisualStyleBackColor = true;
            this.TerminateButton.Click += new System.EventHandler(this.TerminateButton_Click);
            // 
            // PostponeButton
            // 
            this.PostponeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.PostponeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PostponeButton.Location = new System.Drawing.Point(315, 152);
            this.PostponeButton.Name = "PostponeButton";
            this.PostponeButton.Size = new System.Drawing.Size(116, 23);
            this.PostponeButton.TabIndex = 1;
            this.PostponeButton.Text = "&Postpone {0} mins";
            this.PostponeButton.UseVisualStyleBackColor = true;
            this.PostponeButton.Click += new System.EventHandler(this.PostponeButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(291, 13);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "The following process taking high {0} and should be closed:";
            // 
            // ProcessNameLabel
            // 
            this.ProcessNameLabel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ProcessNameLabel.Location = new System.Drawing.Point(15, 26);
            this.ProcessNameLabel.Name = "ProcessNameLabel";
            this.ProcessNameLabel.Size = new System.Drawing.Size(415, 69);
            this.ProcessNameLabel.TabIndex = 4;
            this.ProcessNameLabel.Text = "ProcessName";
            this.ProcessNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(348, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "If you are running on battery, it is most likely reducing your battery life";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "CPU:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(192, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Memory:";
            // 
            // CPULabel
            // 
            this.CPULabel.AutoSize = true;
            this.CPULabel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPULabel.ForeColor = System.Drawing.Color.Red;
            this.CPULabel.Location = new System.Drawing.Point(72, 95);
            this.CPULabel.Name = "CPULabel";
            this.CPULabel.Size = new System.Drawing.Size(49, 23);
            this.CPULabel.TabIndex = 8;
            this.CPULabel.Text = "00%";
            // 
            // MemoryLabel
            // 
            this.MemoryLabel.AutoSize = true;
            this.MemoryLabel.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryLabel.ForeColor = System.Drawing.Color.Red;
            this.MemoryLabel.Location = new System.Drawing.Point(282, 95);
            this.MemoryLabel.Name = "MemoryLabel";
            this.MemoryLabel.Size = new System.Drawing.Size(72, 23);
            this.MemoryLabel.TabIndex = 9;
            this.MemoryLabel.Text = "000 MB";
            // 
            // DummyTextBox
            // 
            this.DummyTextBox.Location = new System.Drawing.Point(15, 152);
            this.DummyTextBox.Name = "DummyTextBox";
            this.DummyTextBox.Size = new System.Drawing.Size(51, 21);
            this.DummyTextBox.TabIndex = 0;
            // 
            // RestartButton
            // 
            this.RestartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RestartButton.Location = new System.Drawing.Point(153, 152);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(75, 23);
            this.RestartButton.TabIndex = 3;
            this.RestartButton.Text = "&Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // KillProcessForm
            // 
            this.AcceptButton = this.PostponeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.PostponeButton;
            this.ClientSize = new System.Drawing.Size(442, 187);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.DummyTextBox);
            this.Controls.Add(this.MemoryLabel);
            this.Controls.Add(this.CPULabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProcessNameLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.PostponeButton);
            this.Controls.Add(this.TerminateButton);
            this.Controls.Add(this.IgnoreButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KillProcessForm";
            this.Text = "Warning! Application taking high {0}!";
            this.Shown += new System.EventHandler(this.KillProcessForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KillProcessForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IgnoreButton;
        private System.Windows.Forms.Button TerminateButton;
        private System.Windows.Forms.Button PostponeButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label ProcessNameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CPULabel;
        private System.Windows.Forms.Label MemoryLabel;
        private System.Windows.Forms.TextBox DummyTextBox;
        private System.Windows.Forms.Button RestartButton;
    }
}