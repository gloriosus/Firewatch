using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public class Polygon : IEntity
    {
        public int Id { get; private set; }

        public string Border { get; private set; }

        public string Center { get; private set; }

        public Polygon(int id, string border, string center)
        {
            Id = id;
            Border = border;
            Center = center;
        }

        public Point ExtractPoint()
        {
            var coordinates = Center.Substring(1, Center.Length - 2).Split(new string[] { ", " }, StringSplitOptions.None);

            double latitude = Convert.ToDouble(coordinates[0], CultureInfo.InvariantCulture);
            double longitude = Convert.ToDouble(coordinates[1], CultureInfo.InvariantCulture);

            return new Point(latitude, longitude);
        }
    }
}
