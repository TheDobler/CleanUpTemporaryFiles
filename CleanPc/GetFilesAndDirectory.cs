using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Security.Principal;

namespace CleanPc
{
    class GetFilesAndDirectory
    {
        private string _Temp = @"C:\Windows\Temp"; //Temp;
        public string Temp { get { return _Temp; } }

        private string _temp = $@"C:\Users\{GetUserName()}\AppData\Local\Temp"; //%temp%;
        public string temp { get { return _temp; } }

        private string _prefetch = @"C:\Windows\Prefetch"; //Prefetch
        public string prefetch { get { return _prefetch; } }

        //Can remove this property because it is an public property in program class
        private string Path { get; }
        public GetFilesAndDirectory(string path)
        {
            Path = path;
        }

        public List<string> AddToList()
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
            return FilesAndDirectories;
        }

        public static string GetUserName()
        {
            //Retrieves the username of the currently logged in user
            string UserName = WindowsIdentity.GetCurrent().Name;
            int index = UserName.IndexOf("\\");
            return UserName.Substring(index + 1);
        }
    }
}
