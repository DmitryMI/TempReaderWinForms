using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.SerialReaders
{
    class LogReader : SerialReaderBase
    {
        private FileStream _stream;

        public LogReader(SerialPort serialPort, FileStream logFileStream) : base(serialPort)
        {
            if (!logFileStream.CanRead)
            {
                logFileStream.Close();
                _stream = new FileStream("log.txt", FileMode.Open, FileAccess.Read);
            }
            else
            {
                _stream = logFileStream;
            }
        }

        public override bool HasData => _stream.Position < _stream.Length;
        public override void RequestData()
        {
            
        }

        public override string GetData()
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                int b = _stream.ReadByte();
                if (b < 0 || (char) b == '\n')
                {
                    break;
                }
                else
                {
                    builder.Append((char) b);
                }
            }

            string logRecord = builder.ToString();

            string data = logRecord.Split('\t')[1];

            return data;
        }
    }
}
