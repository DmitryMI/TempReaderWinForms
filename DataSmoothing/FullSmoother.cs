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
        private int _lastFlatter = 3;

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

            for (int i = _lastFlatter; i < _queue.Count;)
            {
                int delta0 = GetDelta(i - 2, i - 3);
                int delta1 = GetDelta(i - 1, i - 2);
                int delta2 = GetDelta(i, i - 1);
                bool noZeros = delta0 != 0 && delta1 != 0 && delta2 != 0;
                bool isFlatter = noZeros && delta0 != delta1 && delta1 != delta2;
                if (isFlatter)
                {
                    int flatterStart = i - 3;
                    int flatterEnd = i;
                    if (i < _queue.Count)
                    {
                        int delta = delta2;
                        while (i < _queue.Count - 1)
                        {
                            i++;
                            var lastDelta = delta;
                            delta = GetDelta(i, i - 1);
                            if (delta == 0 || lastDelta == delta)
                            {
                                _lastFlatter = i + 4;
                                break;
                            }
                        }

                        //i--;
                        flatterEnd = i;
                    }
                    
                    ProcessFlatterRegion(flatterStart, flatterEnd);
                    Debug.WriteLine("Resolving flatter on {0}, {1}", flatterStart, flatterEnd);
                    i += 4;
                }
                else
                {
                    i += 1;
                }
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
                return;
                // TODO Fix middle interpolation

                int middleIndex = (int)Math.Round((end - start) / 2f + start);
                float middleValue = _queue[middleIndex];

                int halfStart = start + 1;
                int halfCount = middleIndex - start;
                for (int i = halfStart; i < middleIndex; i++)
                {
                    _fixedQueue[i] = Lerp(startValue, middleValue, i - halfStart, halfCount);
                }

                halfStart = middleIndex + 1;
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
