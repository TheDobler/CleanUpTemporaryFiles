using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;


/// <summary>
/// 1. Skrive ut stien til loggfilen om hvor filen ligger - Console.WriteLine("Path to my file: {0}\n", pathString);
/// 
/// </summary>



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
