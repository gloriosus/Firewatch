using System;
using System.Collections.Generic;
using System.Text;

namespace FirewatchDaemon
{
    public class MonthlyValues
    {
        public int Month;
        public float Min;
        public float Max;

        public MonthlyValues(int month, float min, float max)
        {
            Month = month;
            Min = min;
            Max = max;
        }
    }
}
