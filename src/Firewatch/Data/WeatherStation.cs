using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public class WeatherStation : IEntity
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Coordinates { get; private set; }

        public string Url { get; private set; }

        public int UserId { get; private set; }

        public WeatherStation(int id, string title, string coordinates, string url, int userId)
        {
            Id = id;
            Title = title;
            Coordinates = coordinates;
            Url = url;
            UserId = userId;
        }

        public Point ExtractPoint()
        {
            var coordinates = Coordinates.Substring(1, Coordinates.Length - 2).Split(new string[] { ", " }, StringSplitOptions.None);

            double latitude = Convert.ToDouble(coordinates[0], CultureInfo.InvariantCulture);
            double longitude = Convert.ToDouble(coordinates[1], CultureInfo.InvariantCulture);

            return new Point(latitude, longitude);
        }
    }
}
