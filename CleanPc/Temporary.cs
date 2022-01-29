using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;


namespace CleanPc
{
    public class Temporary
    {
        private string _tempPath;  //Temp;
        private List<string> _tempList = new List<string>(); //List of all files and folder in the temporary folder.
        private List<string> _notDeletedList = new List<string>();
        private int _numOfDeletedFiles = 0;

        public string tempPath { get { return _tempPath; } }
        public List<string> tempList { get { return _tempList; } }
        public List<string> notDeletedList { get { return _notDeletedList; } set { _notDeletedList = value; } }
        public int numOfDeletedFiles { get { return _numOfDeletedFiles;} set { _numOfDeletedFiles = value; } }

        public Temporary(string TempPath, List<string> temporarList)
        {
            _tempPath = TempPath;
            _tempList = temporarList;

        }

    }
}
