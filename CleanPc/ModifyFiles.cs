using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CleanPc
{

    public class ModifyFiles
    {
        private string temp;
        private string TempP;
        private string prefetch;

        public ModifyFiles(string _temp, string _TempP, string _prefetch)
        {
            temp = _temp;
            TempP = _TempP;
            prefetch = _prefetch;
        }
        
        public static List<string> getFilesAndDirectory(string path)
        {
            List<string> FilesAndDirectories = new List<string>();
            
                if (Directory.Exists(path))
                {
                    FilesAndDirectories.AddRange(Directory.GetFiles(
                    path, "*", SearchOption.TopDirectoryOnly));

                    FilesAndDirectories.AddRange(
                        Directory.GetDirectories(
                        path, "*", SearchOption.TopDirectoryOnly)
                        );
                }
                else
                {
                    Console.WriteLine("The directory does not exist!");
                    return new List<string>();
                }
            return FilesAndDirectories;
        }
        public void delete(List<string> data)
        {
            List<string> exceptions = new List<string>();
            FileAttributes attr; 
            foreach (var item in data)
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
                    Console.WriteLine($"{item} was deleted!");
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex.Message);
                    continue;
                }
            }
            Console.WriteLine("\n Here is a list of files that could not be deleted: \n");
            foreach (var item in exceptions)
            {
                Console.WriteLine("* " + item +"\n");
            }
            
        }

        public void openFileAndFolder(string path, string action = "open")
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = action
            });
        }
        public void writeToConsole(List<string> data)
        {
            foreach (var item in data)
            {
                FileInfo info = new FileInfo(item);
                Console.WriteLine(info.Name);
            }
        }
    }
}
