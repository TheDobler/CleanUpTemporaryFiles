using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace CleanPc
{
    class Program : GetFilesAndDirectory, IWriteToConsole
    {
        static void Main(string[] args)
        {
            string userName = UserName;
            string Temp = @"C:\Windows\Temp"; //Temp
            string temp = $@"C:\Users\{userName}\AppData\Local\Temp"; //%temp%
            //string prefetch = @"C:\Windows\Prefetch"; //Prefetch

            
            Program program = new Program();

            List<string> _Temp = null;
            List<string> _temp = null;
            //List<string> _prefetch = null;
            
            _Temp = program.AddToList();
            _temp = program.AddToList();
            //_prefetch = program.getFilesAndDirectory(prefetch);

            IWriteToConsole writeToConsole = new Program();
            writeToConsole.WriteToConsole(_temp);
            Console.WriteLine();
            Console.WriteLine();
            writeToConsole.WriteToConsole(_Temp);
            //Console.WriteLine();
            //Console.WriteLine();
            //writeToConsole.WriteToConsole(_prefetch);
        }

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
