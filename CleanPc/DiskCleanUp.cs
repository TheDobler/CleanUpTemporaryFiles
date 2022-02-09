using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace CleanPc
{
    class DiskCleanUp
    {
        static String file = @".\Sageset.txt";

        public void AutoRun()
        {
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = "cleanmgr.exe";
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.StartInfo.Arguments = " /sagerun:1";
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();

        }
        public void ChangeFolderThenRun()
        {
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = "cleanmgr.exe";
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.StartInfo.Arguments = "cleanmgr /D C /sageset:1 & cleanmgr /sagerun:1";
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();

        }
        public bool Sageset()
        {
            
            bool SagsetIsSet = false;
            if (File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                string str = reader.ReadToEnd();
                str = str.Replace(Environment.NewLine, "");
                reader.Close();

                if (str == "1") { SagsetIsSet = true; }
                else
                {
                    SetSageset();
                    SagsetIsSet = true;
                }
            }
            else
            {
                FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write("0");
                sw.Close();
            }
            return SagsetIsSet;
        }
        public void SetSageset()
        {
            StreamWriter sw = new StreamWriter(file, false);
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = "cleanmgr.exe";
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.StartInfo.Arguments = " /sageset:1";
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();
            sw.Write("1");
            sw.Close();
        }
    }
}
