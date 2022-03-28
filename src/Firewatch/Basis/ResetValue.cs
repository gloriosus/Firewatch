using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch
{
    public class ResetValue
    {
        public int Min { get; private set; }

        public int Max { get; private set; }

        public int Precipitation { get; private set; }

        public ResetValue(int min, int max, int precipitation)
        {
            Min = min;
            Max = max;
            Precipitation = precipitation;
        }
    }
}
