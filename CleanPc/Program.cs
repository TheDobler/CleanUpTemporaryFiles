using System;
using System.Collections.Generic;

namespace CleanPc
{
    class Program
    {
        static void Main(string[] args)
        {
            //string userName = "Eirik Døble";
            //string _temp = @"C:\Windows\Temp";
            //string _TEMP = $@"C:\Users\{userName}\AppData\Local\Temp";
            //string _prefetch = @"C:\Windows\Prefetch";

            //ModifyFiles mf = new ModifyFiles(_temp, _TEMP, _prefetch);

            //List<string> temp = ModifyFiles.getFilesAndDirectory(_temp);
            //List<string> TEMP = ModifyFiles.getFilesAndDirectory(_TEMP);
            //List<string> prefetch = ModifyFiles.getFilesAndDirectory(_prefetch);

            //try
            //{
            //    //Console.WriteLine("temp folder: \n");
            //    //mf.writeToConsole(temp);
            //    Console.WriteLine("\n %temp% folder: \n");
            //    mf.writeToConsole(TEMP);
            //    //Console.WriteLine("\n prefetch folder: \n");
            //    //mf.writeToConsole(prefetch);
            //    mf.delete(TEMP);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e + "IN PROGRAM.CS \n");
            //}

            DiskCleanUp dc = new DiskCleanUp();
            dc.open();
            
            Console.ReadKey(); 
        }
    }
}
