using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace CleanPc
{
    class GetFilesAndDirectory
    {
        private string Path;
        public GetFilesAndDirectory(string path) => Path = path;
        public List<string> AddToList()
        {
            //Get all files and folder that is not in use or does not need admin rights.
            var FilesAndDirectories = new List<string>();

            if (!Directory.Exists(Path))
            {
                return new List<string>();
            }
            else
            {
                try
                {
                    FilesAndDirectories.AddRange(Directory.GetFiles(
                    Path, "*", SearchOption.TopDirectoryOnly));

                    FilesAndDirectories.AddRange(
                        Directory.GetDirectories(
                        Path, "*", SearchOption.TopDirectoryOnly)
                        );
                }
                catch (Exception)
                {
                    //Writing to Log file!
                }
            }
            return FilesAndDirectories;
        }
    }
}
