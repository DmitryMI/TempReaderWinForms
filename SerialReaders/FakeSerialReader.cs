using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.SerialReaders
{
    class FakeSerialReader : SerialReaderBase
    {
        private int _fakeDataCounter = 0;

        private float[] _fakeData = new float[]
        {
            22.5f,  // 0
            22.5f,  // 1
            22.5f,  // 2
            22.6f,  // 3

            22.6f,  // 4
            22.5f,  // 5
            22.6f,  // 6
            22.5f,  // 7
            22.5f,  // 8

            22.5f,  // 9
            22.4f,  // 10
            22.5f,  // 11
            22.4f,  // 12
            22.4f,  // 13

            22.3f,  // 14
            22.4f,  // 15
            22.3f,  // 16
            22.4f,  // 17
            22.3f,  // 18
            22.4f,  // 19
            22.3f,  // 20
            22.2f,  // 21

            22.1f,  // 22
            22.1f,  // 23
            22.2f,  // 24
            22.2f,  // 25
            22.1f,  // 26
            22.1f,  // 27
            22.2f,  // 28
            22.3f,  // 29

            22.2f,  // 30
            22.2f,  // 31
            22.3f,  // 32
            22.3f,  // 33
            22.3f,  // 34
            22.2f,  // 35
            22.3f,  // 36
            22.3f,  // 37
            22.3f,  // 38
            22.3f,  // 39

            22.4f,  // 40
            22.4f,  // 4
        };

        private string GetFakeData(float temp)
        {
            return "0,00" + "|" + temp.ToString("0.00", CultureInfo.InvariantCulture) + "|" + "0,00" + "|" + "0,00";
        }

        public FakeSerialReader(SerialPort serialPort) : base(serialPort)
        {
        }

        public override bool HasData => _fakeDataCounter < _fakeData.Length;
        public override void RequestData()
        {
            _fakeDataCounter = 0;
        }

        public override string GetData()
        {
            float fake = _fakeData[_fakeDataCounter];
            _fakeDataCounter++;
            return GetFakeData(fake);
        }
    }
}
