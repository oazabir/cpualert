using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CPUMonitor
{
    static class MemoryHelper
    {
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        public static void ClearMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
               
                EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
            catch
            {
            }
        }
    }
}
