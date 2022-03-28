using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;

namespace FirewatchDaemon
{
    public sealed class MapInfo
    {
        public string FilePath { get; set; }

        public MapInfo() { }

        public MapInfo(string filePath)
        {
            FilePath = filePath;
        }

        public IEnumerable<Polygon> Parse()
        {
            var lines = File.ReadAllLines(FilePath, Encoding.UTF8);

            int polygonNumber = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Region"))
                {
                    int count = 0;

                    bool isNumber = int.TryParse(lines[i + 1].Trim(), out count);

                    if (isNumber)
                    {
                        Point[] points = new Point[count - 1];

                        for (int v = i + 2; v < i + 2 + count - 1; v++)
                        {
                            string[] coordinates = lines[v].Split(' ');

                            double latitude = Convert.ToDouble(coordinates[1], CultureInfo.InvariantCulture);
                            double longitude = Convert.ToDouble(coordinates[0], CultureInfo.InvariantCulture);

                            points[v - (i + 2)] = new Point(latitude, longitude);
                        }

                        //var centerCoordinates = lines[i + 1 + count + 3].Trim().Split(' ');

                        //double centerLatitude = Convert.ToDouble(centerCoordinates[2], CultureInfo.InvariantCulture);
                        //double centerLongitude = Convert.ToDouble(centerCoordinates[1], CultureInfo.InvariantCulture);

                        double centerLatitude = (points.Select(point => point.Latitude).Min() + points.Select(point => point.Latitude).Max()) / 2;
                        double centerLongitude = (points.Select(point => point.Longitude).Min() + points.Select(point => point.Longitude).Max()) / 2;

                        Point center = new Point(centerLatitude, centerLongitude);

                        polygonNumber++;

                        yield return new Polygon(points, center, $"polygon{polygonNumber}", "mymap");
                    }
                }
            }
        }
    }
}
