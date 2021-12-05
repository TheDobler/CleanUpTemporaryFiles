using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace CleanPc
{
    class Program : ModifyFiles
    {
        static void Main(string[] args)
        {
            string userName = GetUserName();
            string _temp = @"C:\Windows\Temp"; //Temp
            string _TEMP = $@"C:\Users\{userName}\AppData\Local\Temp"; //%temp%
            string _prefetch = @"C:\Windows\Prefetch"; //Prefetch

            ModifyFiles mf = new ModifyFiles();
            Program program = new Program();

            List<string> temp = null;
            List<string> TEMP = null;
            List<string> prefetch = null;

            try
            {
                temp = program.getFilesAndDirectory(_temp);
                TEMP = program.getFilesAndDirectory(_TEMP);
                prefetch = program.getFilesAndDirectory(_prefetch);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("It's working!!");
                Console.WriteLine(e); //.GetType().Name);
            }
           

            try
            {
                if (temp.Count != 0)
                {
                    Console.WriteLine("temp folder: \n");
                    mf.writeToConsole(temp);
                }
                else
                {
                    Console.WriteLine("The folder is empty!");
                }
                   
                if (TEMP.Count != 0)
                {
                    Console.WriteLine("\n %temp% folder: \n");
                    mf.writeToConsole(TEMP);
                }
                else
                {
                    Console.WriteLine("The folder is empty!");
                }

                if (prefetch.Count != 0)
                {
                    Console.WriteLine("\n prefetch folder: \n");
                    mf.writeToConsole(prefetch);
                }
                else
                {
                    Console.WriteLine("The folder is empty!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[IN PROGRAM.CS] - " + e + "\n");
            }

            //DiskCleanUp dc = new DiskCleanUp();
            //dc.open();

            Console.ReadKey(); 
         }

        static string GetUserName()
        {
            string UserName = WindowsIdentity.GetCurrent().Name;
            int index = UserName.IndexOf("\\");
            return UserName.Substring(index + 1);
        }
    }
}
