using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CleanPc
{
    interface IWriteToConsole
    {
        void WriteToConsole(List<Temporary> data);
    }
}
