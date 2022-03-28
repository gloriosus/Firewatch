using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public class Day : IEntity
    {
        public int Id { get; private set; }

        public int WeatherStationId { get; private set; }

        public float Temperature { get; private set; }

        public float Dew { get; private set; }

        public float Precipitation { get; private set; }

        public DateTime Date { get; private set; }

        public int UserId { get; private set; }

        public Day(int id, int weatherStationId, float temperature, float dew, float precipitation, DateTime date, int userId)
        {
            Id = id;
            WeatherStationId = weatherStationId;
            Temperature = temperature;
            Dew = dew;
            Precipitation = precipitation;
            Date = date;
            UserId = userId;
        }
    }
}
