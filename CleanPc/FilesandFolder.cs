using System;
using System.Collections.Generic;
using System.Text;

namespace CleanPc
{
    class FilesandFolder
    {
        private string _Temp; //= @"C:\Windows\Temp"; //Temp folder
        public string Temp { get { return _Temp; } }

        private string _temp; // = $@"C:\Users\{UserName}\AppData\Local\Temp"; //%temp% folder
        public string temp { get { return _temp; } }

        private string _prefetch; // = @"C:\Windows\Prefetch"; //Prefetch folder
        public string prefetch { get { return _prefetch; } }

        public FilesandFolder(string TempFolderPath, string PercentTempFolderPath, string PrefetchFolderPath)
        {
            _Temp = TempFolderPath;
            _temp = PercentTempFolderPath;
            _prefetch = PrefetchFolderPath;
        }

    }
}
