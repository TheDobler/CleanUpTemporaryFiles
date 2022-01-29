using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;


/// <summary>
/// 
/// 
/// </summary>



namespace CleanPc
{
    class Program
    {
        
        static void Main(string[] args)
        {

            LogFile log = new LogFile();
            string Temp = @"C:\Windows\Temp";
            string PercentTemp = $@"C:\Users\{GetUserName()}\AppData\Local\Temp";
            string prefetch = @"C:\Windows\Prefetch";
            List<Temporary> temporaries; //= new List<Temporary>()

            //string[] paths = {Temp, PercentTemp, prefetch };
            string[] paths = { PercentTemp};

            if (File.Exists(LogFile.LogPath)) File.Delete(LogFile.LogPath);

            log.CreateFolderIfNotExist();
            log.WritesToLog("CleanPC Solution!" + "\nDate run " + DateTime.Now.ToString() + "\n");

            temporaries = GetTemporary(paths);
            foreach (var item in temporaries)
            {
                Delete(item);
            }

            log.WriteToConsole(temporaries);

            Console.WriteLine("We're done, you've fired!");
        }

        static List<Temporary> GetTemporary(string [] pathToFiles) 
        {
            Dictionary<int, List<string>> file_item = new Dictionary<int, List<string>>();
            List<Temporary> tempList = new List<Temporary>();

            for (int i = 0; i < pathToFiles.Length; i++)
            {
                file_item.Add(i, GetTempFiles(pathToFiles[i]));
            }

            for (int i = 0; i < pathToFiles.Length; i++)
            {
                Temporary Temp = new Temporary(pathToFiles[i], file_item[i]);
                tempList.Add(Temp);
            }

            return new List<Temporary>(tempList);
        }

        static void Delete(Temporary files)
        {
            LogFile log = new LogFile();
            FileAttributes attr;
            int countDeletedFiles = 0;
            List<string> notDeleteFilesList = new List<string>();
            foreach (string item in files.tempList)
            {
                try
                {
                    attr = File.GetAttributes(item);

                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(item, true);
                        countDeletedFiles++;
                    }
                    else
                    {
                        File.Delete(item);
                        countDeletedFiles++;
                    }
                    //deletedList.Add(($"{item} was deleted!"));
                    log.WritesToLog(($"{item} was successfully deleted!"));
                }
                catch (Exception ex)
                {
                    //log.WritesToLog(ex.Message); //Writing to log file
                    //notDeleteFilesList.Add(item);
                    notDeleteFilesList.Add(ex.Message);
                }
            }
            files.numOfDeletedFiles = countDeletedFiles;
            files.notDeletedList = notDeleteFilesList;

            log.WritesToLog($"It was {files.numOfDeletedFiles} successfully deleted files!");
            log.WritesToLog("Files that was not deleted: ");
            foreach (var item in notDeleteFilesList)
            {
                log.WritesToLog(item);
            }
            log.WritesToLog("");
        }

        static public List<string> GetTempFiles(string Path)
        {
            //Get all files and folder that is not in use or does not need admin rights.
            var FilesAndDirectories = new List<string>();
            LogFile log = new LogFile();

            if (!Directory.Exists(Path))
            {
                if (string.IsNullOrEmpty(Path))
                {
                    log.WritesToLog(" The path cannot be null or empty!");
                    return new List<string>(); ;
                }
                log.WritesToLog("The folder does not exist!");
                return new List<string>();
            }
            else
            {
                try
                {
                    FilesAndDirectories.AddRange(Directory.GetFiles(
                    Path, "*", SearchOption.TopDirectoryOnly));

                    FilesAndDirectories.AddRange(
                        Directory.GetDirectories(
                        Path, "*", SearchOption.TopDirectoryOnly)
                        );
                }
                catch (Exception e)
                {
                    log.WritesToLog(e.Message);
                }
            }
            //return a new instance off FilesAndDirectories.
            return new List<string>(FilesAndDirectories);
        }

        static void Delete1(List<string> files)
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

        public static string GetUserName()
        {
            //Retrieves the username of the currently logged in user
            string UserName = WindowsIdentity.GetCurrent().Name;
            int index = UserName.IndexOf("\\");
            return UserName.Substring(index + 1);
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
