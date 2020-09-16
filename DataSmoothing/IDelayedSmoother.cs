using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempReaderWinForms.DataSmoothing
{
    interface IDelayedSmoother : IDataSmoother
    {
        void MakeSmooth();
    }
}
