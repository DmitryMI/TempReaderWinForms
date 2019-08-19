using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using TempReaderWinForms.DataSmoothing;
using TempReaderWinForms.SerialReaders;

namespace TempReaderWinForms
{
    public partial class MainForm : Form
    {
        public const int BaudRate = 9600;
        public const string LogFileName = "log.txt";

        private SerialPort _serialPort;
        private LineSeries _temperatureLine;
        private LineSeries _smoothTempLine;
        private LineSeries _humidityLine;
        private PlotModel _temperatureModel;
        private PlotModel _smoothTempModel;
        private PlotModel _humidityModel;
        private SerialReaderBase _serialReader;
        private FileStream _logStream;
        private IDataSmoother _dataSmoother;

        private int _counter = 0;

        public MainForm(string[] args)
        {
            InitializeComponent();

            ParseArgs(args);
        }

        private void ParseArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-t"))
                {
                    if (args.Length > i + 1)
                    {
                        int timerInterval = Int32.Parse(args[i + 1]);
                        MainTimer.Interval = timerInterval;
                        LogLabel.Text = "Timer interval set: " + timerInterval;
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitLogFile();
            InitPlot();
            InitListener();
        }

        private void InitListener()
        {
            try
            {
                
                 _serialPort = new SerialPort("COM3", BaudRate);
                _serialPort.Open();
                _serialPort.DiscardInBuffer();

                _serialReader = new AsyncSerialReader(_serialPort);
                

                //_serialReader = new FakeSerialReader(null);
                //_serialReader = new LogReader(null, _logStream);

                MainTimer.Start();
            }
            catch (IOException ex)
            {
                PrintMessage(ex.Message, true);
            }
        }

        private void PrintMessage(string text, bool isError)
        {
            MessageLabel.ForeColor = isError ? Color.Red : SystemColors.ControlText;
            MessageLabel.Text = text;
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            ProcessSensor();
        }

        private void ProcessSensor()
        {
            while (true)
            {
                string serialData = ReadSerialPort();

                if (serialData == null)
                    break;

                if (serialData.StartsWith("ERR"))
                {
                    LogLabel.Text = serialData;
                }
                else
                {
                    SensorData sensorData = ParseSensorData(serialData);
                    _counter++;

                    LogLabel.Text = "Measurements: " + _counter;

                    DateTime dateTime = DateTime.Now;
                    float time = DateTime.Now.ToBinary();
                    float temperature = sensorData.PreciseTemperature;
                    float humidity = sensorData.PreciseHumidity;

                    PlotNewData(_counter, temperature, _temperatureLine);
                    PlotSmoothing(_counter, temperature, _smoothTempLine);
                    PlotNewData(_counter, humidity, _humidityLine);

                    LogSerialData(dateTime, serialData);

                    if (_counter > 1)
                    {
                        RefreshPlotter();
                    }
                }
            }
        }

        private void InitLogFile()
        {
            FileInfo info = new FileInfo(LogFileName);

            if (!File.Exists(LogFileName))
            {
                FileStream createdStream = File.Create(LogFileName);
                createdStream.Close();
            }
            _logStream = new FileStream(LogFileName, FileMode.Append, FileAccess.Write);
        }

        private void LogSerialData(DateTime time, string serialData)
        {
            string line = time.ToLongTimeString() + "\t" + serialData + '\n';
            //SerialDataLogger.Text += line;
            SerialDataLogger.Text = line + SerialDataLogger.Text;

            if (_logStream.CanWrite)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(line);
                _logStream.Write(bytes, 0, bytes.Length);
                _logStream.Flush(true);
            }
        }

        private string ReadSerialPort()
        {
            if (_serialReader.HasData)
            {
                string tmp = _serialReader.GetData();
                return tmp;
            }
            else
            {
                _serialReader.RequestData();
            }

            return null;
        }

        private SensorData ParseSensorData(string serialData)
        {
            if (serialData.StartsWith("ERR"))
            {
                return null;
            }

            string[] dataSet = serialData.Split('|');

            float preciseHumi = Single.Parse(dataSet[0], CultureInfo.InvariantCulture);
            float preciseTemp = Single.Parse(dataSet[1], CultureInfo.InvariantCulture);
            float roughHumi = Single.Parse(dataSet[2], CultureInfo.InvariantCulture);
            float roughTemp = Single.Parse(dataSet[3], CultureInfo.InvariantCulture);

            return new SensorData()
            {
                PreciseTemperature = preciseTemp, PreciseHumidity = preciseHumi, RoughTemperature = roughTemp,
                RoughHumidity = roughHumi
            };
        }

        private void InitPlot()
        {
            //_dataSmoother = new SmartDataSmoother();
            //_dataSmoother = new AverageDataSmoother(3);
            _dataSmoother = new TempReaderWinForms.DataSmoothing.FullSmoother();
            //_dataSmoother = new ExpendableAvgSmoother();

            _temperatureModel = new PlotModel
            {
                Title = "Temperature status",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical
            };

            _smoothTempModel = new PlotModel
            {
                Title = "Temperature status (smooth)",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical
            };


            _humidityModel = new PlotModel
            {
                Title = "Humidity status",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical
            };

            _temperatureLine = new LineSeries();
            _humidityLine = new LineSeries();
            _smoothTempLine = new LineSeries();
            //_smoothTempLine.Smooth = true;

            _temperatureModel.Series.Add(_temperatureLine);
            _smoothTempModel.Series.Add(_smoothTempLine);
            _humidityModel.Series.Add(_humidityLine);

            TemperaturePlot.Model = _temperatureModel;
            TemperaturePlot.Visible = true;

            SmoothedTemperaturePlot.Model = _smoothTempModel;
            TemperaturePlot.Visible = true;

            HumidityPlot.Model = _humidityModel;
            HumidityPlot.Visible = true;
        }

        private void PlotNewData(float time, float value, DataPointSeries line)
        {
            line.Points.Add(new DataPoint(time, value));
        }

        private void PlotSmoothing(float time, float value, DataPointSeries line)
        {
            /*_dataSmoother.AddValue(value);
            float smoothed = _dataSmoother.GetLastValue();
            //float smoothed = value;
            PlotNewData(time, smoothed, line);*/
            line.Points.Add(new DataPoint(time, value));
            _dataSmoother.AddValue(value);
            int lastIndex = line.Points.Count - 1;

            for (int i = 1; i < _dataSmoother.MaxBackIndex; i++)
            {
                float backValue = _dataSmoother.GetValue(i);
                DataPoint point = line.Points[lastIndex - i];
                point = new DataPoint(point.X, backValue);
                line.Points[lastIndex - i] = point;
            }
        }

        private void RefreshPlotter()
        {
            _temperatureModel.InvalidatePlot(true);
            _smoothTempModel.InvalidatePlot(true);
            _humidityModel.InvalidatePlot(true);
        }
    }
}
