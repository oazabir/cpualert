namespace CPUMonitor
{
    partial class MonitorCPUForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorCPUForm));
            this.MonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.TaskbarNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TaskbarContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TaskbarContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonitorTimer
            // 
            this.MonitorTimer.Interval = 1000;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(585, 243);
            this.PropertyGrid.TabIndex = 0;
            this.PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // TaskbarNotifyIcon
            // 
            this.TaskbarNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.TaskbarNotifyIcon.ContextMenuStrip = this.TaskbarContextMenu;
            this.TaskbarNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskbarNotifyIcon.Icon")));
            this.TaskbarNotifyIcon.Text = "CPU Monitor";
            this.TaskbarNotifyIcon.Visible = true;
            this.TaskbarNotifyIcon.DoubleClick += new System.EventHandler(this.TaskbarNotifyIcon_DoubleClick);
            // 
            // TaskbarContextMenu
            // 
            this.TaskbarContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.TaskbarContextMenu.Name = "TaskbarContextMenu";
            this.TaskbarContextMenu.Size = new System.Drawing.Size(117, 48);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DoubleClickEnabled = true;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MonitorCPUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 243);
            this.Controls.Add(this.PropertyGrid);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MonitorCPUForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CPU Monitor";
            this.Shown += new System.EventHandler(this.MonitorCPU_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MonitorCPU_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonitorCPUForm_FormClosing);
            this.TaskbarContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer MonitorTimer;
        private System.Windows.Forms.PropertyGrid PropertyGrid;
        private System.Windows.Forms.NotifyIcon TaskbarNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip TaskbarContextMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

