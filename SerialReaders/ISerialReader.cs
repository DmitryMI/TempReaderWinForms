using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.SerialReaders
{
    interface ISerialReader
    {
        bool HasData { get; }
        void RequestData();

        void Release();
        string GetData();
    }
}
