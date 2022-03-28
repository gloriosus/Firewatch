using System;
using System.Collections.Generic;
using System.Text;

namespace FirewatchDaemon
{
    public class Polygon
    {
        public Point[] Border { get; private set; }
        public Point Center { get; private set; }
        public string Name { get; private set; }
        public string Map { get; private set; }

        public Polygon(Point[] border, Point center, string name, string map)
        {
            Border = border;
            Center = center;
            Name = name;
            Map = map;
        }

        public override string ToString()
        {
            string coordinates = string.Empty;

            for (int i = 0; i < Border.Length; i++)
            {
                coordinates += Border[i].ToString() + ", ";
            }

            coordinates = coordinates.Substring(0, coordinates.Length - 2);

            return $"var {Name} = L.polygon([{coordinates}]).addTo({Map});";
        }

        public string GetString()
        {
            string coordinates = string.Empty;

            for (int i = 0; i < Border.Length; i++)
            {
                coordinates += Border[i].ToString() + ", ";
            }

            coordinates = coordinates.Substring(0, coordinates.Length - 2);

            return coordinates;
        }
    }
}
