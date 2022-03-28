using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch
{
    public class HazardClass
    {
        public int Min { get; private set; }

        public int Max { get; private set; }

        public string Color { get; private set; }

        public HazardClass(int min, int max, string color)
        {
            Min = min;
            Max = max;
            Color = color;
        }
    }
}
