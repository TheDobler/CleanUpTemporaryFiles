using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CleanPc
{
    public class LogFile
    {
        public static string path { get; set; } = @"C:\Users\Eirik Døble\Desktop\TestFolder\DiskCleanUpLog";
        public static string fileName { get; set; } = "DiskCleanUpLogFile.txt";
        // private static string path = @"C:\Users\Eirik Døble\Desktop\TestFolder\DiskCleanUpLog\test";
        //private static string fileName = "DiskCleanUpLogFile.txt";
        private static readonly string _pathString = Path.Combine(path, fileName);

        public static string PathString => _pathString;


        public LogFile()
        {
            //Console.WriteLine("Path to my file: {0}\n", pathString);
            //Creates a new log file
            CreateFolderIfNotExist();

        }

        private void CreateFolderIfNotExist()
        {
            string[] list = path.Split('\\');
            string _path = "";
            foreach (var item in list)
            {
                _path = _path + item + "\\";
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                    if (Directory.Exists(path))
                    {
                        FileStream fs = File.Create(_pathString);
                        fs.Flush();
                        fs.Close();
                    }
                }
            }
        }
        public void WritesToLog(string text)
        {
            if (File.Exists(PathString))
            {
                //Console.WriteLine("File \"{0}\" already exists.", fileName);
                string textString = $"\n {text} \n";
                File.AppendAllText(PathString, textString);
                return;
            }
            byte[] txt = Encoding.ASCII.GetBytes("\n" + text + "\n");
            FileStream file = File.OpenWrite(PathString);
            file.Write(txt);
            file.Flush();
            file.Close();
            
        }
    }
}
