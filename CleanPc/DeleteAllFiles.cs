using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CleanPc
{
    class DeleteAllFiles
    {
        //Delete all files in the list. Files that cannot be deleted are added to a new list that is returned.

        List<string> files = new List<string>();
        List<string> exceptionList = new List<string>();

        public DeleteAllFiles(List<string> data)
        {
            files = data;
            
        }

        FileAttributes attr;

        private List<string> Delete()
        {
            foreach (string item in files)
            {
                try
                {
                    attr = File.GetAttributes(item);
                    //Console.WriteLine(attr.ToString());
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(item, true);
                    }
                    else
                    {
                        File.Delete(item);
                    }
                    Console.WriteLine($"{item} was deleted!"); //Writing to log file
                }
                catch (Exception ex)
                {
                    exceptionList.Add(ex.Message); //Writing to log file
                    continue;
                }

            }
            return exceptionList;
        }
    }
}
