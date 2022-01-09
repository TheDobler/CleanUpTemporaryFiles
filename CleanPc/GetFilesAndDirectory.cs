using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace CleanPc
{
    class GetFilesAndDirectory : LogFile
    {
        private string Path { get; }
        public GetFilesAndDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                WritesToLog(" 1. cannot be null or empty1.");
                Console.WriteLine($"1 . '{nameof(path)}' cannot be null or empty.");
                WritesToLog("1");
                Console.WriteLine("1");
                WritesToLog("2");
                Console.WriteLine("2");
                WritesToLog("3");
                Console.WriteLine("3");
                WritesToLog("4");
                Console.WriteLine("4");

            }

            Path = path;
        }

        public List<string> AddToList()
        {
            //Get all files and folder that is not in use or does not need admin rights.
            var FilesAndDirectories = new List<string>();

            if (!Directory.Exists(Path))
            {
                WritesToLog("The folder does not exist23!");
                Console.WriteLine("The folder does not exist23!");

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
                    WritesToLog("2. " + e.Message);
                    Console.WriteLine("2. " + e.Message);
                }
            }
            return FilesAndDirectories;
        }
    }
}
