using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CleanPc
{
    public class LogFile : IWriteToConsole
    {
        public static string LogFilePath { get; set; } = @"C:\DiskCleanUpLog";
        public static string LogFileName { get; set; } = "DiskCleanUpLogFile.txt";

        private static readonly string _LogPath = Path.Combine(LogFilePath, LogFileName);
        public static string LogPath => _LogPath;

        public void CreateFolderIfNotExist()
        {
            //Creates a new folder with a log file if the folder does not already exist
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
            //Writes to the log file
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

        public void WritesToLog(List<Temporary> tempText)
        {
            //Writes to the log file
            if (!File.Exists(LogPath))
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            
            if (tempText != null)
            {
                for (int i = 0; i < tempText.Count; i++)
                {
                    int count = 1;
                    sb.AppendLine(tempText[i].tempPath);
                    if ((tempText[i].DeletedList.Count == 0 | tempText[i].DeletedList == null) & tempText[i].notDeletedList.Count != 0)
                    {
                        sb.AppendLine("No files were deleted! ");
                    }
                    else
                    {
                        sb.AppendLine($"{tempText[i].numOfDeletedFiles} successful files were deleted!");
                        sb.AppendLine("Deleted files: ");
                        foreach (var deleted in tempText[i].DeletedList)
                        {
                            sb.AppendLine(count + " " + deleted);
                            count++;
                        }
                    }
                    
                    count = 1;
                    if ((tempText[i].notDeletedList.Count == 0 | tempText[i].notDeletedList == null) & tempText[i].DeletedList.Count != 0)
                    {

                        sb.AppendLine("All files were deleted!!");
                    }
                    else
                    {
                        sb.AppendLine("Undeleted files: ");
                        foreach (var notDeleted in tempText[i].notDeletedList)
                        {
                            sb.AppendLine(count + " " + notDeleted);
                            count++;
                        }
                        
                    }
                    sb.AppendLine("");
                    File.AppendAllText(LogPath, sb.ToString());
                    sb.Clear();
                }
            }
        }

        public void WriteToConsole(List<Temporary> data)
        {
            //Writes to the console window
            Console.WriteLine("-----------------------------Summary-------------------------------------");

            foreach (var item in data)
            {
                Console.WriteLine($"For path '{item.tempPath}' it was {item.numOfDeletedFiles} successfully deleted files!");
                Console.WriteLine($"{item.notDeletedList.Count} files was not deleted.");
                Console.WriteLine();
            }
            Console.WriteLine($"To get more information go to the log file at {LogFile.LogPath}");
            Console.WriteLine("-------------------------------------------------------------------------");
        }
    }
}
