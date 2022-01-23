using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;



/// <summary>
/// 1. Skrive ut stien til loggfilen om hvor filen ligger - Console.WriteLine("Path to my file: {0}\n", pathString);
/// 
/// </summary>



namespace CleanPc
{
    class Program : IWriteToConsole
    {
        
        

        

        static void Main(string[] args)
        {
            if (File.Exists(LogFile.LogPath)) File.Delete(LogFile.LogPath);
            LogFile log = new LogFile();
            Program p = new Program();

            log.CreateFolderIfNotExist();
            log.WritesToLog("CleanPC Solution!" + "\nDate run " + DateTime.Now.ToString() + "\n");
            openFileAndFolder(p.Temp);

            //var GetTemp = new GetFilesAndDirectory("");
            var GetTemp = new GetFilesAndDirectory(p.Temp); //Temp
            //var _GetTemp = new GetFilesAndDirectory(p.temp); //%temp%
            //var GetPrefetch = new GetFilesAndDirectory(p.prefetch); //Preftech


            Console.WriteLine("We're done, you've fired!");
        }

        public void Delete(List<string> files)
        {
            LogFile log = new LogFile();
            FileAttributes attr;
            foreach (string item in files)
            {
                try
                {
                    attr = File.GetAttributes(item);

                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(item, true);
                    }
                    else
                    {
                        File.Delete(item);
                    }
                    //deletedList.Add(($"{item} was deleted!"));
                    log.WritesToLog(($"{item} was deleted!"));
                }
                catch (Exception ex)
                {
                    log.WritesToLog(ex.Message); //Writing to log file
                    continue;
                }

            }

        }

        void IWriteToConsole.WriteToConsole(List<string> data)
        {
            int i = 0;
            foreach (var item in data)
            {
                FileInfo info = new FileInfo(item);
                Console.WriteLine( ++i + " " + info.Name);
            }
        }

        public static void openFileAndFolder(string path, string action = "open")
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = action
            });
        }
    }
}
