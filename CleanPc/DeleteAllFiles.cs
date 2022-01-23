using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CleanPc
{
    class DeleteAllFiles
    {
        //Delete all files in the list. Files that cannot be deleted are added to a new list that is returned.
        LogFile log = new LogFile();
        List<string> files = new List<string>();

        public DeleteAllFiles(List<string> data)
        {
            files = data;
        }

        public void Delete()
        {
            FileAttributes attr;
            foreach (string item in files)
            {
                try
                {
                    attr = File.GetAttributes(item);
                    
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(item, true);
                    }
                    else
                    {
                        File.Delete(item);
                    }
                    //deletedList.Add(($"{item} was deleted!"));
                    log.WritesToLog(($"{item} was deleted!"));
                }
                catch (Exception ex)
                {
                    log.WritesToLog(ex.Message); //Writing to log file
                    continue;
                }

            }

        }
    }
}
