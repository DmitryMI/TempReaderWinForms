using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.DataSmoothing
{
    class FullSmoother : IDataSmoother
    {
        private List<float> _queue = new List<float>();
        private List<float> _fixedQueue = new List<float>();
        private int _lastFlatter = 0;

        private int _intervalMaxLen = 10;
        private float _sensitivityUnit = 0.1f;

        public float GetLastValue()
        {
            return _queue.Last();
        }

        public float GetValue(int backIndex)
        {
            return _fixedQueue[_fixedQueue.Count - 1 - backIndex];
        }

        public int MaxBackIndex => _queue.Count;

        public void AddValue(float value)
        {
            _queue.Add(value);
            _fixedQueue.Add(value);

            FixData();
        }

        private void FixData()
        {
            if(_queue.Count < 4)
                return;

            for (int i = _lastFlatter; i < _queue.Count - 1;)
            {
                float currentValue = _queue[i];
                int regionStart = i;
                i++;
                while (i < _queue.Count - 1)
                {
                    float nextValue = _queue[i];
                    if (Math.Abs(nextValue - currentValue) > _sensitivityUnit * 1.5f)
                    {
                        _lastFlatter = i;
                        break;
                    }

                    i++;
                }

                Debug.WriteLine("Processing region: {0}, {1}", regionStart, i);
                ProcessFlatterRegion(regionStart, i);
            }
        }

        private void ProcessFlatterRegion(int start, int end)
        {
            float startValue = _queue[start];
            float endValue = _queue[end];
            int count = end - start;

            if (Math.Abs(endValue - startValue) > 0.001f)
            {

                for (int i = start + 1; i < end; i++)
                {
                    _fixedQueue[i] = Lerp(startValue, endValue, i - start, count);
                }
            }
            else
            {
                //return;
                // TODO Fix middle interpolation

                int middleIndex = (int)Math.Round((end - start) / 2f + start);
                float middleValue = _queue[middleIndex];

                int halfStart = start + 1;
                int halfCount = middleIndex - start;
                for (int i = halfStart; i < middleIndex; i++)
                {
                    _fixedQueue[i] = Lerp(startValue, middleValue, i - halfStart, halfCount);
                }

                halfStart = middleIndex;
                halfCount = end - middleIndex;
                for (int i = halfStart; i < end; i++)
                {
                    _fixedQueue[i] = Lerp(middleValue, endValue, i - halfStart, halfCount);
                }
            }
        }

        private float Lerp(float min, float max, int index, int count)
        {
            float part = (float)index / count;
            float delta = max - min;
            return delta * part + min;
        }

        private int GetDelta(int a, int b)
        {
            return Math.Sign(_queue[b] - _queue[a]);
        }

    }
}
