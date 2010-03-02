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


    /// <summary>
    /// The form that loads and remains running the app. It hosts the Settings view.
    /// </summary>
    public partial class MonitorCPUForm : Form
    {
        #region Fields

        private readonly Dictionary<int, ProcessInfo> _IgnoreProcess = new Dictionary<int, ProcessInfo>();
        private readonly KillProcessForm _KillProcessForm = new KillProcessForm();
        private readonly Dictionary<int, DateTime> _PostponeProcess = new Dictionary<int, DateTime>();
        private readonly List<ProcessInfo> _ProcessesToKill = new List<ProcessInfo>();

        private Monitor _Monitor;
        private Timer _ProcessKillTimer;

        #endregion Fields

        #region Constructors

        public MonitorCPUForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Checks the after A while if a process is still running. If running, then kill it.
        /// </summary>
        /// <param name="processToKill">The process to kill.</param>
        private void CheckAfterAWhileIfItsStillRunning(ProcessInfo processToKill)
        {
            if (_ProcessKillTimer == default(Timer))
            {
                _ProcessKillTimer = new Timer();
                _ProcessKillTimer.Tick += new EventHandler(_ProcessKillTimer_Tick);
            }

            if (!_ProcessesToKill.Contains(processToKill))
                _ProcessesToKill.Add(processToKill);

            _ProcessKillTimer.Interval = Convert.ToInt32(Settings.Default.How_Long_To_Wait_Before_Killing_Process_If_Normal_Close_Does_Not_Work.TotalMilliseconds);
            _ProcessKillTimer.Start();
        }


        /// <summary>
        /// Closes the process by sending Shutdown message to the main window. Then it starts 
        /// a timer to check if the process is really closed. 
        /// </summary>
        /// <param name="processToKill">The process to kill.</param>
        private void CloseProcess(ProcessInfo processToKill)
        {
            try
            {
                Process process = Process.GetProcessById(processToKill.Id);
                processToKill.Path = process.MainModule.FileName;
                process.CloseMainWindow();
            }
            catch (Exception closeException)
            {
                if (MessageBox.Show(this, "Unable to close process: " + (processToKill.Title) + Environment.NewLine
                                          + closeException.Message + Environment.NewLine
                                          + "Do you want to kill it?", "Unable to close process", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    KillProcess(processToKill);
                }
            }

            // Check back after a while if the process is still running. If it's running kill it
            CheckAfterAWhileIfItsStillRunning(processToKill);
        }

        /// <summary>
        /// Decides if process can be killed by checking if user has chosen this process to be
        /// postponed or ignored.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="whatsHigh">The whats high.</param>
        private void DecideIfProcessCanBeKilled(ProcessInfo process, string whatsHigh)
        {
            if (!_IgnoreProcess.ContainsKey(process.Id))
            {
                if (_PostponeProcess.ContainsKey(process.Id))
                {
                    DateTime postponeUntil = _PostponeProcess[process.Id];
                    if (postponeUntil > DateTime.Now)
                    {
                        // We need to wait
                        Debug.WriteLine(process.Title + ": user has asked us to wait till " + postponeUntil.ToLocalTime());
                    }
                    else
                    {
                        // It's time to ask user again
                        _PostponeProcess.Remove(process.Id);
                        ShowProcessToKill(process, whatsHigh);
                    }
                }
                else
                {
                    // There's no previous request by user to postpone it, so ask user
                    ShowProcessToKill(process, whatsHigh);
                }
            }
            else
            {
                // User has asked us to ignore this process, too bad
                Debug.WriteLine(process.Title + ": user has asked to ignore this.");
            }
        }

        /// <summary>
        /// Exits this application.
        /// </summary>
        private void Exit()
        {
            if (_KillProcessForm != default(KillProcessForm))
                _KillProcessForm.Close();

            this.Close();
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void HideForm()
        {
            this.Hide();
            this.TaskbarNotifyIcon.ShowBalloonTip(5000, "CPU Monitor", "CPU Monitor is running...", ToolTipIcon.Info);
            MemoryHelper.ClearMemory();
        }

        /// <summary>
        /// Hides the form that shows alert for a process.
        /// </summary>
        private void HideProcessToKill()
        {
            this.Invoke(new MethodInvoker(delegate
                {
                    _KillProcessForm.Hide();
                    _Monitor.Start();
                    MemoryHelper.ClearMemory();
                }));
        }

        /// <summary>
        /// Kill a process if it still running. After killing, if it is requested
        /// to be restarted, then restart it.
        /// </summary>
        /// <param name="processToKill">The process to kill.</param>
        private void KillProcess(ProcessInfo processToKill)
        {
            try
            {
                foreach (Process process in Process.GetProcessesByName(processToKill.Name))
                {
                    // Check if the process is still running
                    if (process.Id == processToKill.Id)
                    {
                        if (!process.HasExited)
                        {
                            process.Kill();
                            process.Close();
                        }
                    }
                }

                // If user has requested the process to be restarted, then restart it.
                if (processToKill.CanRestart)
                    Process.Start(processToKill.Path);

            }
            catch (Exception killException)
            {
                MessageBox.Show(this, "Unable to kill process: " + (processToKill.Title)
                                      + Environment.NewLine + "You should restart your computer", "Unable to kill process",
                                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void MonitorCPUForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
        }

        private void MonitorCPU_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Monitor != default(Monitor))
                _Monitor.Stop();

            _KillProcessForm.Dispose();
        }

        private void MonitorCPU_Shown(object sender, EventArgs e)
        {
            this.PropertyGrid.SelectedObject = Settings.Default;
            if (_Monitor == default(Monitor))
            {
                _Monitor = new Monitor();
                _Monitor.Start();
                _Monitor.Changed += new Monitor.ProcessUpdate(_Monitor_Changed);

                _Monitor.WarningForHighCPU += new Monitor.ProcessNotification(_Monitor_WarningForHighCPU);
                _Monitor.KillForHighCPU += new Monitor.ProcessNotification(_Monitor_KillForHighCPU);
                _Monitor.HighCPU += new Monitor.ProcessNotification(_Monitor_HighCPU);
                _Monitor.RecoveredCPU += new Monitor.ProcessNotification(_Monitor_RecoveredCPU);

                _Monitor.HighMemory += new Monitor.ProcessNotification(_Monitor_HighMemory);
                _Monitor.KillForHighMemory += new Monitor.ProcessNotification(_Monitor_KillForHighMemory);
                _Monitor.RecoveredMemory += new Monitor.ProcessNotification(_Monitor_RecoveredMemory);
                _Monitor.WarningForHighMemory += new Monitor.ProcessNotification(_Monitor_WarningForHighMemory);

                _KillProcessForm.Terminate += new Action<ProcessInfo>(_KillProcessForm_Terminate);
                _KillProcessForm.Postpone += new Action<ProcessInfo>(_KillProcessForm_Postpone);
                _KillProcessForm.Ignore += new Action<ProcessInfo>(_KillProcessForm_Ignore);
                _KillProcessForm.Restart += new Action<ProcessInfo>(_KillProcessForm_Restart);

                HideForm();
            }
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void ShowProcessToKill(ProcessInfo process, string whatsHigh)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                _Monitor.Stop();
                _KillProcessForm.Show(process, whatsHigh);
            }));
        }

        private void TaskbarNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        /// <summary>
        /// Terminates the process.
        /// </summary>
        /// <param name="process">The process.</param>
        private void TerminateProcess(ProcessInfo process)
        {
            _PostponeProcess.Remove(process.Id);
            _IgnoreProcess.Remove(process.Id);

            CloseProcess(process);

            HideProcessToKill();
        }

        void _KillProcessForm_Ignore(ProcessInfo process)
        {
            if (!_IgnoreProcess.ContainsKey(process.Id))
                _IgnoreProcess.Add(process.Id, process);

            HideProcessToKill();
        }

        void _KillProcessForm_Postpone(ProcessInfo process)
        {
            if (!_PostponeProcess.ContainsKey(process.Id))
                _PostponeProcess.Add(process.Id, DateTime.Now.Add(Settings.Default.Default_Postpone_Time));

            HideProcessToKill();
        }

        void _KillProcessForm_Restart(ProcessInfo process)
        {
            process.CanRestart = true;
            TerminateProcess(process);
        }

        void _KillProcessForm_Terminate(ProcessInfo process)
        {
            TerminateProcess(process);
        }

        void _Monitor_Changed(List<ProcessInfo> processes)
        {
            foreach (ProcessInfo process in processes)
            {
                if (process.CpuUsage > 0)
                    Debug.WriteLine(process.Title + '\t' + process.CpuUsage);
            }
        }

        void _Monitor_HighCPU(ProcessInfo process)
        {
            Debug.WriteLine(string.Format("High CPU: {0}, {1}%", process.Title, process.CpuUsage));
        }

        void _Monitor_HighMemory(ProcessInfo process)
        {
            Debug.WriteLine(process.Title + ": High Memory");
        }

        void _Monitor_KillForHighCPU(ProcessInfo process)
        {
            Debug.WriteLine(process.Title + ": Exceeded limit for kill. Let's kill the process");

            DecideIfProcessCanBeKilled(process, "CPU");
        }

        void _Monitor_KillForHighMemory(ProcessInfo process)
        {
            Debug.WriteLine(process.Title + ": Exceeded Memory limit for kill. Let's kill the process");

            DecideIfProcessCanBeKilled(process, "Memory");
        }

        void _Monitor_RecoveredCPU(ProcessInfo process)
        {
            Debug.WriteLine(string.Format("Recovered CPU: {0}, {1}%", process.Title, process.CpuUsage));
        }

        void _Monitor_RecoveredMemory(ProcessInfo process)
        {
            Debug.WriteLine(process.Title + ": Recovered memory");
        }

        void _Monitor_WarningForHighCPU(ProcessInfo process)
        {
            this.TaskbarNotifyIcon.ShowBalloonTip(5000, "CPU Monitor", "Warning! Process causing high CPU: " + process.Title, ToolTipIcon.Warning);
            Debug.WriteLine(process.Title + " High CPU Warning");
        }

        void _Monitor_WarningForHighMemory(ProcessInfo process)
        {
            this.TaskbarNotifyIcon.ShowBalloonTip(5000, "CPU Monitor", "Warning! Process taking high memory: " + process.Title, ToolTipIcon.Warning);
            Debug.WriteLine(process.Title + " High Memory Warning");
        }

        void _ProcessKillTimer_Tick(object sender, EventArgs e)
        {
            _ProcessKillTimer.Stop();

            this.Invoke(new MethodInvoker(delegate
                {
                    _ProcessesToKill.ForEach(new Action<ProcessInfo>(delegate(ProcessInfo process)
                        {
                            KillProcess(process);
                        }));
                }));
        }

        #endregion Methods

        private void CPUAlertLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://code.google.com/p/cpualert/");
        }

        private void MeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://msmvps.com/blogs/omar/");
        }
    }
}