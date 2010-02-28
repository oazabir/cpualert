namespace CPUMonitor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    [RunInstaller(true)]
    public class CPUMonitorInstaller : System.Configuration.Install.Installer
    {
        #region Constructors

        public CPUMonitorInstaller()
            : base()
        {
            this.Committed += new InstallEventHandler(MyInstaller_Committed);
            this.Committing += new InstallEventHandler(MyInstaller_Committing);
        }

        #endregion Constructors

        #region Methods

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            foreach (Process process in Process.GetProcessesByName("CPUMonitor"))
            {
                process.CloseMainWindow();
                Thread.Sleep(10000);
                try
                {
                    if (!process.HasExited)
                        process.Kill();

                    process.Close();
                }
                catch
                {
                }
            }
        }

        private void MyInstaller_Committed(object sender, InstallEventArgs e)
        {
            try
            {
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Process.Start(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) + "\\CPUMonitor.exe");
            }
            catch
            {
                MessageBox.Show("There was a problem launching the application. Please restart your computer to run CPU Monitor",
                    "Problem launching CPU Monitor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MyInstaller_Committing(object sender, InstallEventArgs e)
        {
        }

        #endregion Methods
    }
}