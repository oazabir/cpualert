namespace CPUMonitor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    using CPUMonitor.Properties;

    public partial class KillProcessForm : Form
    {
        #region Fields

        private ProcessInfo _Process;

        #endregion Fields

        #region Constructors

        public KillProcessForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        public event Action<ProcessInfo> Ignore;

        public event Action<ProcessInfo> Postpone;

        public event Action<ProcessInfo> Restart;

        public event Action<ProcessInfo> Terminate;

        #endregion Events

        #region Methods

        public void Show(ProcessInfo process, string whatsHigh)
        {
            this._Process = process;

            this.Text = "Warning! Process taking high " + whatsHigh;
            this.TitleLabel.Text = "The following process is taking high " + whatsHigh + " and should be closed:";
            this.ProcessNameLabel.Text = process.Title;
            this.PostponeButton.Text = string.Format("Postpone {0} mins", Settings.Default.Default_Postpone_Time.TotalMinutes);
            this.CPULabel.Text = process.CpuUsage + "%";
            this.MemoryLabel.Text = (process.WorkingSet / 1048576) + " MB";
            try
            {
                Process realProcess = Process.GetProcessById(process.Id);
                this.ProcessNameLabel.Text = realProcess.MainWindowTitle;

            }
            catch
            {
            }

            this.Show();
            this.Refresh();
            this.TopMost = true;

            this.DummyTextBox.Focus();
        }

        private void IgnoreButton_Click(object sender, EventArgs e)
        {
            Ignore(this._Process);
        }

        private void KillProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Ignore(this._Process);
            }
        }

        private void PostponeButton_Click(object sender, EventArgs e)
        {
            Postpone(this._Process);
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Restart(this._Process);
        }

        private void TerminateButton_Click(object sender, EventArgs e)
        {
            Terminate(this._Process);
        }

        #endregion Methods

        private void KillProcessForm_Shown(object sender, EventArgs e)
        {
            this.DummyTextBox.Focus();
        }
    }
}