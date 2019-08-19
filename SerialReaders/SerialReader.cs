using System.IO.Ports;

namespace TempReaderWinForms.SerialReaders
{
    class SerialReader : SerialReaderBase
    {
        public SerialReader(SerialPort serialPort) : base(serialPort)
        {
        }

        public override bool HasData => SerialPort.BytesToRead > 0;

        public override void RequestData()
        {
            WriteCmd(CmdPreciseSensor);
        }

        public override string GetData()
        {
            return ReadSerialPort();
        }
    }
}
