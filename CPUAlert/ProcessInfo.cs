namespace CPUMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProcessInfo
    {
        #region Fields

        public bool CanRestart;
        public int CpuUsage;
        public string Description;
        public int Id;
        public string Name;
        public string Path;
        public string Title;
        public long WorkingSet;

        #endregion Fields
    }
}