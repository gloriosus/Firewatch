using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FirewatchDaemon
{
    public class Point
    {
        // X
        public double Latitude { get; private set; }
        // Y
        public double Longitude { get; private set; }

        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"[{Latitude.ToString(CultureInfo.InvariantCulture)}, {Longitude.ToString(CultureInfo.InvariantCulture)}]";
        }
    }
}
