using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CleanPc
{
    public class LogFile
    {
        public static string LogFilePath { get; set; } = @"C:\Users\Eirik Døble\Desktop\TestFolder\DiskCleanUpLog";
        public static string LogFileName { get; set; } = "DiskCleanUpLogFile.txt";

        private static readonly string _LogPath = Path.Combine(LogFilePath, LogFileName);
        public static string LogPath => _LogPath;

        //public LogFile() //Have to finde a better solution here!!
        //{
        //    CreateFolderIfNotExist();
        //}

        public void CreateFolderIfNotExist()
        {
            string[] list = LogFilePath.Split('\\');
            string _path = "";
            foreach (var item in list)
            {
                _path = _path + item + "\\";
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                    if (Directory.Exists(LogFilePath))
                    {
                        StreamWriter fs = File.CreateText(LogPath);
                        fs.Close();
                    }
                }
            }
        }
        public void WritesToLog(string text)
        {
            if (File.Exists(LogPath))
            {
                string textString = $"{text} \n";
                File.AppendAllText(LogPath, textString);
                return;
            }

            FileStream fs = new FileStream(LogPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(text);
            sw.Close();
            
        }
    }
}
