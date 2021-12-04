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

            List<string> temp = program.getFilesAndDirectory(_temp);
            List<string> TEMP = program.getFilesAndDirectory(_TEMP);
            List<string> prefetch = program.getFilesAndDirectory(_prefetch);

            try
            {
                if (temp.Count != 0)
                {
                    Console.WriteLine("temp folder: \n");
                    mf.writeToConsole(temp);
                }
                   
                if (TEMP.Count != 0)
                {
                    Console.WriteLine("\n %temp% folder: \n");
                    mf.writeToConsole(TEMP);
                }
                mf.Delete(TEMP);

                if (prefetch.Count != 0)
                {
                    Console.WriteLine("\n prefetch folder: \n");
                    mf.writeToConsole(prefetch);
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
