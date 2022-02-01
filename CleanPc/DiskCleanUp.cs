using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace CleanPc
{
    class DiskCleanUp
    {
        public void open()
        {
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = "cleanmgr.exe";
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();

        }
    }
}
