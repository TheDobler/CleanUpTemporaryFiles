using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace CleanPc
{

    class ModifyFiles
    {
        protected List<string> getFilesAndDirectory(string path)
        {
            //Get all files and folder that is not in use or does not need admin rights.
           List<string> FilesAndDirectories = new List<string>();
            
                if (Directory.Exists(path))
                {
                    try
                    {
                        FilesAndDirectories.AddRange(Directory.GetFiles(
                        path, "*", SearchOption.TopDirectoryOnly));

                        FilesAndDirectories.AddRange(
                            Directory.GetDirectories(
                            path, "*", SearchOption.TopDirectoryOnly)
                            );
                    }
                    catch (Exception e)
                    {
                        if (e is UnauthorizedAccessException)
                        {
                            throw new UnauthorizedAccessException();
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    return new List<string>();
                }
            return FilesAndDirectories;
        }
        
        public List <string> Delete(List<string> data)
        {
            //Delete all files in the list. Files that cannot be deleted are added to a new list that is returned.
            List<string> exceptions = new List<string>();
            FileAttributes attr; 
            foreach (var item in data)
            {
                try
                {
                    attr = File.GetAttributes(item);
                    Console.WriteLine(attr.ToString());
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(item, true);
                    }
                    else
                    {
                        File.Delete(item);
                    }
                    Console.WriteLine($"{item} was deleted!");
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex.Message);
                    continue;
                }

            }
            return exceptions;
        }

       
       
    }
}
