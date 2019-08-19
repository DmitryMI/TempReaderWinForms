using System.Collections.Generic;

namespace TempReaderWinForms.DataSmoothing
{
    class AverageDataSmoother : IDataSmoother
    {
        private int _queueLength;

        private Queue<float> _queue;

        public AverageDataSmoother(int count)
        {
            _queueLength = count;
            _queue = new Queue<float>(count);
        }

        public float GetLastValue()
        {
            float summ = 0;
            float[] arr = _queue.ToArray();
            for (int i = 0; i < _queue.Count; i++)
            {
                summ += arr[i];
            }

            return summ / _queue.Count;
        }

        public float GetValue(int backIndex)
        {
            float[] arr = _queue.ToArray();

            /*
            return arr[arr.Length - 1 - backIndex];*/

            /*float[] arr = _queue.ToArray();
            int index = arr.Length - 1 - backIndex;
            float summ = 0;
            for (int i = 0; i < index; i++)
            {
                summ += arr[i];
            }

            return summ / index;*/

            float summ = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                summ += arr[i];
            }

            return summ / arr.Length;
        }

        public int MaxBackIndex => _queue.Count;

        public void AddValue(float value)
        {
            _queue.Enqueue(value);
            if (_queue.Count > _queueLength)
            {
                _queue.Dequeue();
            }
        }
    }
}
