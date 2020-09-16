using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.SerialReaders
{
    class LoggedSerialReader : ISerialReader, ISerialReaderTimed
    {
        private ISerialReaderTimed _logReader;
        private ISerialReader _actualReader;
        private DateTime _lastDateTime;
        private bool _logReleased = false;

        public LoggedSerialReader(ISerialReader actualReader, ISerialReaderTimed logReader)
        {
            _logReader = logReader;
            _actualReader = actualReader;
        }

        public bool HasData => (_logReader != null && _logReader.HasData) || _actualReader.HasData;
        public void RequestData()
        {
            if(_logReader.HasData)
                return;
            
            _actualReader.RequestData();
        }

        public void Release()
        {
            _logReader.Release();
            _actualReader.Release();
        }

        public string GetData()
        {
            string tmp;
            if (_logReader.HasData)
            {
                tmp = _logReader.GetData();
                _lastDateTime = _logReader.GetLastResultTime();
                return _logReader.GetData();
            }
            else
            {
                if (!_logReleased)
                {
                    _logReader?.Release();
                    _logReleased = true;
                }

                //_logReader = null;
            }

            tmp = _actualReader.GetData();

            if (_actualReader is ISerialReaderTimed timedReader)
            {
                _lastDateTime = timedReader.GetLastResultTime();
            }
            else
            {
                _lastDateTime = DateTime.Now;
            }

            return tmp;
        }

        public bool LogFileReleased => _logReleased;

        public DateTime GetLastResultTime()
        {
            return _lastDateTime;
        }
    }
}
