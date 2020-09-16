using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.SerialReaders
{
    class LogReader : SerialReaderBase, ISerialReaderTimed
    {
        private FileStream _stream;
        private DateTime _lastDateTime;
        private bool _wasReleased = false;

        public override void Release()
        {
            base.Release();

            _stream?.Close();
            _stream?.Dispose();

            _wasReleased = true;
        }

        public LogReader(SerialPort serialPort, FileStream logFileStream) : base(serialPort)
        {
            if (!logFileStream.CanRead)
            {
                throw new ArgumentException("Stream must be readable");
            }
            else
            {
                _stream = logFileStream;
            }
        }

        public override bool HasData => !_wasReleased && _stream.Position < _stream.Length;
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

            var split = logRecord.Split('\t');
            string date = split[0];
            
            bool dateParsed = DateTime.TryParse(date, out _lastDateTime);
            //DateTime.
            /*if(!dateParsed)
                _lastDateTime = new DateTime();*/

            string data = split[1];

            return data;
        }

        public DateTime GetLastResultTime()
        {
            return _lastDateTime;
        }
    }
}
