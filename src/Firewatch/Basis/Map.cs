using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firewatch.Data;

namespace Firewatch
{
    public static class Map
    {
        private static HazardClass[] NesterovScale = new HazardClass[]
        {
            new HazardClass(0, 300, "rgb(135,206,235)"),    // Blue
            new HazardClass(301, 1000, "rgb(154,205,50)"),  // Green
            new HazardClass(1001, 4000, "rgb(255,215,0)"),  // Yellow
            new HazardClass(4001, 12000, "rgb(255,140,0)"), // Orange
            new HazardClass(12001, 99999, "rgb(139,0,0)")   // Red
        };

        private static ResetValue[] ComplexIndicatorResetValues = new ResetValue[]
        {
            new ResetValue(0, 300, 2),
            new ResetValue(301, 1000, 3),
            new ResetValue(1001, 2000, 4),
            new ResetValue(2001, 3000, 5),
            new ResetValue(3001, 4000, 6),
            new ResetValue(4001, 5000, 7),
            new ResetValue(5001, 6000, 8),
            new ResetValue(6001, 7000, 9),
            new ResetValue(7001, 8000, 10),
            new ResetValue(8001, 9000, 11),
            new ResetValue(9001, 10000, 12),
            new ResetValue(10001, 11000, 13),
            new ResetValue(11001, 12000, 14),
            new ResetValue(12001, 99999, 15)
        };

        private static List<ComplexIndicator> ComplexIndicatorTable = new List<ComplexIndicator>();

        private static double CalculateDistance(Point polygon, Point station)
        {
            return Math.Sqrt(Math.Pow(station.Latitude - polygon.Latitude, 2) + Math.Pow(station.Longitude - polygon.Longitude, 2));
        }

        private static double CalculateComplexIndicator(IEnumerable<Day> month)
        {
            double result = 0;

            double rainfall = 0;

            foreach (var day in month)
            {
                int comparisonValue = Convert.ToInt32(result);
                int resetValue = ComplexIndicatorResetValues.First(reset => comparisonValue >= reset.Min & comparisonValue <= reset.Max).Precipitation;

                if (day.Precipitation + rainfall <= resetValue)
                {
                    result += day.Temperature * (day.Temperature - day.Dew);
                    rainfall = day.Precipitation;
                }
                else
                {
                    result = day.Temperature * (day.Temperature - day.Dew);
                    rainfall = 0;
                }

                if (result < 0)
                {
                    result = 0;
                }
            }

            return result;
        }

        private static double Interpolate(Polygon polygon, IEnumerable<WeatherStation> stations)
        {
            double top = 0;
            double bottom = 0;

            double power = 3;

            foreach (var station in stations)
            {
                top += ComplexIndicatorTable.First(ci => ci.WeatherStationId == station.Id).Value / Math.Pow(CalculateDistance(polygon.ExtractPoint(), station.ExtractPoint()), power);
                bottom += 1 / Math.Pow(CalculateDistance(polygon.ExtractPoint(), station.ExtractPoint()), power);
            }

            return top / bottom;
        }

        public static void FillComplexIndicatorTable(string month, DateTime date)
        {
            ComplexIndicatorTable.Clear();

            foreach (var station in WeatherStationDAO.Instance.Read(1))
            {
                double value = CalculateComplexIndicator(MonthDAO.Instance.Read(month, station.Id, 1, date));

                ComplexIndicatorTable.Add(new ComplexIndicator(station.Id, value));
            }
        }

        public static string Generate(Polygon polygon)
        {
            var stations = WeatherStationDAO.Instance.Read(1);

            int result = Convert.ToInt32(Interpolate(polygon, stations));

            string color = NesterovScale.First(hazard => result >= hazard.Min & result <= hazard.Max).Color;

            return $"var polygon{polygon.Id} = L.polygon([" + polygon.Border + "], { fillColor: '" + color + "', fillOpacity: 0.5, weight: 0.5 }).addTo(mymap);";
        }
    }
}
