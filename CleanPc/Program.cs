using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace CleanPc
{
    class Program : IWriteToConsole
    {
        //Retrieves the username of the currently logged in user
        private static string UserName
        {
            get
            {
                string UserName = WindowsIdentity.GetCurrent().Name;
                int index = UserName.IndexOf("\\");
                return UserName.Substring(index + 1);
            }
        }

        private string _Temp = @"C:\Windows\Temp"; //Temp;
        public string Temp{get { return _Temp; }}

        private string _temp = $@"C:\Users\{UserName}\AppData\Local\Temp"; //%temp%;
        public string temp{ get{return _temp;}}

        private string _prefetch = @"C:\Windows\Prefetch"; //Prefetch
        public string prefetch { get { return _prefetch; } }


        static void Main(string[] args)
        {
            if (File.Exists(LogFile.PathString))
            {
                File.Delete(LogFile.PathString);
            }
            LogFile log = new LogFile();
            Program p = new Program();
            //string userName = UserName;
            //string Temp = @"C:\Windows\Temp"; //Temp
            //string temp = $@"C:\Users\{userName}\AppData\Local\Temp"; //%temp%
            //string prefetch = @"C:\Windows\Prefetch"; //Prefetch

            //var GetTemp = new GetFilesAndDirectory(p.Temp); //Temp
            var GetTemp = new GetFilesAndDirectory(""); //Temp

            //var _GetTemp = new GetFilesAndDirectory(p.temp); //%temp%
            //var GetPrefetch = new GetFilesAndDirectory(p.prefetch); //Preftech

            new DeleteAllFiles(GetTemp.AddToList()).Delete(); //Add to list and delete files and directory inside Temp folder. 
            //new DeleteAllFiles(_GetTemp.AddToList()).Delete(); //Add to list and delete files and directory inside %temp% folder.
            //new DeleteAllFiles(GetPrefetch.AddToList()).Delete(); //Add to list and delete files and directory inside Preftech folder.


            Console.WriteLine("We're done, you've fired!");
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

        public void openFileAndFolder(string path, string action = "open")
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
