using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.DataSmoothing
{
    class SmartDataSmoother : IDataSmoother
    {
        public int Capacity = 4;

        private Queue<float> _queue;

        private Queue<float> _smoothQueue;

        public SmartDataSmoother()
        {
            _queue = new Queue<float>(Capacity);
            _smoothQueue = new Queue<float>(Capacity);
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
            float[] arr = _smoothQueue.ToArray();
            return arr[arr.Length - 1 - backIndex];
        }

        public int MaxBackIndex => Math.Min(Capacity, _queue.Count);

        public void AddValue(float value)
        {
            AddValueInternal(value);
        }

        private void AddValueInternal(float value)
        {
            _queue.Enqueue(value);
            _smoothQueue.Enqueue(value);
            if (_queue.Count > Capacity)
            {
                _queue.Dequeue();
                _smoothQueue.Dequeue();
            }

            if(_queue.Count == Capacity)
                CleanPikes();
        }

        private void CleanPikes()
        {
            float[] arr = _smoothQueue.ToArray();
          

            int delta01 = Math.Sign(arr[1] - arr[0]);
            int delta12 = Math.Sign(arr[2] - arr[1]);
            int delta23 = Math.Sign(arr[3] - arr[2]);

            bool noZeros = delta01 * delta12 * delta23 != 0;
            bool hasDifference = delta01 != delta12 && delta12 != delta23;
            bool hasPike = noZeros && hasDifference;

            if (hasPike)
            {
                arr[1] = Lerp(arr[0], arr[3], 1);
                arr[2] = Lerp(arr[0], arr[3], 2);
            }

            _smoothQueue = new Queue<float>(arr);
        }

        private float Lerp(float min, float max, int index)
        {
            float part = (float)index / (Capacity - 1);
            float delta = max - min;
            return delta * part + min;
        }
    }
}
