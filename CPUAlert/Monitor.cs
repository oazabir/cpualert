namespace CPUMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Management;
    using System.Text;
    using System.Timers;

    using CPUMonitor.Properties;

    class Monitor
    {
        #region Fields

        private Dictionary<int, TimeSpan> _CPULimitExceeded;
        private Dictionary<int, TimeSpan> _MemoryLimitExceeded;
        private Dictionary<int, ProcessInfo> _ProcessInfoMap;
        private ManagementObjectSearcher _Searcher = 
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT IDProcess, Name, PercentProcessorTime, Description, WorkingSet FROM Win32_PerfFormattedData_PerfProc_Process");
        private Timer _Timer;

        #endregion Fields

        #region Delegates

        public delegate void ProcessNotification(ProcessInfo process);

        public delegate void ProcessUpdate(List<ProcessInfo> processes);

        #endregion Delegates

        #region Events

        public event ProcessUpdate Changed;

        public event ProcessNotification HighCPU;

        public event ProcessNotification HighMemory;

        public event ProcessNotification KillForHighCPU;

        public event ProcessNotification KillForHighMemory;

        public event ProcessNotification RecoveredCPU;

        public event ProcessNotification RecoveredMemory;

        public event ProcessNotification WarningForHighCPU;

        public event ProcessNotification WarningForHighMemory;

        #endregion Events

        #region Methods

        public void Start()
        {
            _CPULimitExceeded = new Dictionary<int, TimeSpan>();
            _MemoryLimitExceeded = new Dictionary<int, TimeSpan>();
            _ProcessInfoMap = new Dictionary<int, ProcessInfo>();

            _Timer = new Timer(Settings.Default.How_Frequently_To_Check.TotalMilliseconds);
            _Timer.Elapsed += new ElapsedEventHandler(_Timer_Elapsed);
            _Timer.Start();
        }

        public void Stop()
        {
            _Searcher.Dispose();
            _CPULimitExceeded.Clear();

            _Timer.Stop();
            _Timer.Dispose();
        }

        private void CheckCPU(List<ProcessInfo> processes)
        {
            Dictionary<int, TimeSpan>.Enumerator e = _CPULimitExceeded.GetEnumerator();

            while (e.MoveNext())
            {
                int processId = e.Current.Key;
                TimeSpan duration = e.Current.Value;

                if (duration > Settings.Default.How_Long_To_Wait_Before_Kill_Notice_For_CPU)
                    KillForHighCPU(processes.Find(p => p.Id == processId));
                else if (duration > Settings.Default.How_Long_To_Wait_Before_Warning_For_CPU)
                    WarningForHighCPU(processes.Find(p => p.Id == processId));

                if (_CPULimitExceeded.Count == 0)
                    break;
            }
        }

        private void CheckMemory(List<ProcessInfo> processes)
        {
            Dictionary<int, TimeSpan>.Enumerator e = _MemoryLimitExceeded.GetEnumerator();

            while (e.MoveNext())
            {
                int processId = e.Current.Key;
                TimeSpan duration = e.Current.Value;

                if (duration > Settings.Default.How_Long_To_Wait_Before_Kill_Notice_For_Memory)
                    KillForHighMemory(processes.Find(p => p.Id == processId));
                else if (duration > Settings.Default.How_Long_To_Wait_Before_Warning_For_Memory)
                    WarningForHighMemory(processes.Find(p => p.Id == processId));

                if (_MemoryLimitExceeded.Count == 0)
                    break;
            }
        }

        private List<ProcessInfo> GetUsage()
        {
            var processes = new List<ProcessInfo>();
            foreach (ManagementObject queryObj in _Searcher.Get())
            {
                var process = new ProcessInfo
                {
                    Id = Convert.ToInt32(queryObj["IDProcess"]),
                    Name = Convert.ToString(queryObj["Name"]),
                    CpuUsage = Convert.ToInt32(queryObj["PercentProcessorTime"]),
                    Description = Convert.ToString(queryObj["Description"]),
                    WorkingSet = Convert.ToInt64(queryObj["WorkingSet"]),
                };

                if (string.IsNullOrEmpty(process.Description))
                    process.Title = process.Name;
                else
                    process.Title = process.Description;

                if (process.Id > 0)
                    processes.Add(process);
            }

            return processes;
        }

        private void ProcessMemory(List<ProcessInfo> processes)
        {
            foreach (ProcessInfo process in processes)
            {
                if (process.WorkingSet > Settings.Default.Memory_Threshold_Bytes)
                {
                    if (!_MemoryLimitExceeded.ContainsKey(process.Id))
                        _MemoryLimitExceeded.Add(process.Id, TimeSpan.Zero);
                    else
                    {
                        TimeSpan oldDuration = _MemoryLimitExceeded[process.Id];
                        TimeSpan newDuration = oldDuration.Add(Settings.Default.How_Frequently_To_Check);
                        _MemoryLimitExceeded[process.Id] = newDuration;
                    }

                    HighMemory(process);
                }
                else
                {
                    if (_MemoryLimitExceeded.ContainsKey(process.Id))
                    {
                        _MemoryLimitExceeded.Remove(process.Id);
                        RecoveredMemory(process);
                    }
                }
            }
        }

        private void ProcessUsage(List<ProcessInfo> processes)
        {
            foreach (ProcessInfo process in processes)
            {
                if (process.CpuUsage > Settings.Default.CPU_Threshold)
                {
                    if (!_CPULimitExceeded.ContainsKey(process.Id))
                        _CPULimitExceeded.Add(process.Id, TimeSpan.Zero);
                    else
                    {
                        TimeSpan oldDuration = _CPULimitExceeded[process.Id];
                        TimeSpan newDuration = oldDuration.Add(Settings.Default.How_Frequently_To_Check);
                        _CPULimitExceeded[process.Id] = newDuration;
                    }

                    HighCPU(process);
                }
                else
                {
                    if (_CPULimitExceeded.ContainsKey(process.Id))
                    {
                        _CPULimitExceeded.Remove(process.Id);
                        RecoveredCPU(process);
                    }
                }
            }
        }

        void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var processes = GetUsage();

            ProcessUsage(processes);
            CheckCPU(processes);

            ProcessMemory(processes);
            CheckMemory(processes);

            if (Changed != default(ProcessUpdate))
                Changed(processes);
        }

        #endregion Methods
    }
}