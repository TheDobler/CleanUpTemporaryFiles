using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace CleanPc
{
    class Program : ModifyFiles, IWriteToConsole
    {
        static void Main(string[] args)
        {
            string userName = UserName;
            //string _temp = @"C:\Windows\Temp"; //Temp
            string _TEMP = $@"C:\Users\{userName}\AppData\Local\Temp"; //%temp%
            //string _prefetch = @"C:\Windows\Prefetch"; //Prefetch

            ModifyFiles mf = new ModifyFiles();
            Program program = new Program();

            //List<string> temp = null;
            List<string> TEMP = null;
            //List<string> prefetch = null;

            try
            {
                //temp = program.getFilesAndDirectory(_temp);
                TEMP = program.getFilesAndDirectory(_TEMP);
                //prefetch = program.getFilesAndDirectory(_prefetch);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
            }
           

            try
            {
                //if (temp.Count != 0)
                //{
                //    Console.WriteLine("temp folder: \n");
                //    mf.writeToConsole(temp);
                //}
                //else
                //{
                //    Console.WriteLine("The folder is empty!");
                //}
                   
                if (TEMP.Count != 0)
                {
                    Console.WriteLine("\n %temp% folder: \n");


                    writeToConsole(TEMP);
                    mf.Delete(TEMP);
                }
                else
                {
                    Console.WriteLine("The folder is empty!");
                }

                //if (prefetch.Count != 0)
                //{
                //    Console.WriteLine("\n prefetch folder: \n");
                //    mf.writeToConsole(prefetch);
                //}
                //else
                //{
                //    Console.WriteLine("The folder is empty!");
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("[IN PROGRAM.CS] - " + e + "\n");
            }
         }

        //Retrieves the username of the currently logged in user
        static string UserName
        {
            get
            {
                string UserName = WindowsIdentity.GetCurrent().Name;
                int index = UserName.IndexOf("\\");
                return UserName.Substring(index + 1);
            }
        }

        public void WriteToConsole(List<string> data)
        {
            foreach (var item in data)
            {
                FileInfo info = new FileInfo(item);
                Console.WriteLine(" " + info.Name);
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
