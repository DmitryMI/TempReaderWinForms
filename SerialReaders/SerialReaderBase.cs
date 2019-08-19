using System.IO.Ports;

namespace TempReaderWinForms.SerialReaders
{
    abstract class SerialReaderBase
    {
        public const int CmdPreciseSensor = 0x1;
        public const int CmdRoughSensor = 0x2;

        protected SerialPort SerialPort;
        public abstract bool HasData { get; }
        public abstract void RequestData();

        public abstract string GetData();

        protected SerialReaderBase(SerialPort serialPort)
        {
            SerialPort = serialPort;
        }

        protected string ReadSerialPort()
        {
            if (SerialPort != null)
            {
                string data = SerialPort.ReadLine();

                data = data.TrimEnd();

                return data;
            }

            return null;
        }

        protected void WriteCmd(int cmd)
        {
            byte[] sendBuffer = new byte[] { CmdPreciseSensor };
            SerialPort.Write(sendBuffer, 0, sendBuffer.Length);
        }
    }
}
