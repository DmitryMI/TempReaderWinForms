using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace TempReaderWinForms.SerialReaders
{
    class AsyncSerialReader : SerialReaderBase
    {
        private Task _workerReader;
        private Task _workerWriter;
        private object _lock = new object();
        private string _buffer;

        public AsyncSerialReader(SerialPort serialPort) : base(serialPort)
        {
        }

        public override bool HasData
        {
            get
            {
                bool taskFinished = _workerReader == null || _workerReader.IsCompleted;
                if (!taskFinished)
                    return false;

                bool bufferHasData;
                lock (_lock)
                {
                    bufferHasData = !String.IsNullOrEmpty(_buffer);
                }

                return bufferHasData;
            }
        }

        public override void RequestData()
        {
            if (_workerReader == null || _workerReader.Status != TaskStatus.Running)
            {
                _workerReader = new Task(ReadingMethod);
                _workerReader.Start();

                _workerWriter = new Task(WritingMethod);
                _workerWriter.Start();
            }
        }

        public override string GetData()
        {
            if (!HasData)
                return null;

            string dataCopy;
            lock (_lock)
            {
                dataCopy = (string)_buffer.Clone();
                _buffer = null;
            }

            RequestData();

            return dataCopy;
        }

        private void ReadingMethod()
        {
            string data = ReadSerialPort();
            lock (_lock)
            {
                _buffer = data;
            }
        }

        private void WritingMethod()
        {
            WriteCmd(CmdPreciseSensor);
        }
    }
}
