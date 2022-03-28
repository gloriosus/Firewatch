using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;
using System.Globalization;

namespace Firewatch.Data
{
    public class MonthDAO : Singleton<MonthDAO>, IDataAccessObject<Day>
    {
        public void Create(Day entity)
        {
            throw new Exception("Invalid operation for this type");
        }

        public void Create(Day entity, string month)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute($"INSERT INTO {month} (WeatherStationId, Temperature, Dew, Precipitation, Date, UserId) VALUES (@WeatherStationId, @Temperature, @Dew, @Precipitation, @Date, @UserId)", new { WeatherStationId = entity.WeatherStationId, Temperature = entity.Temperature, Dew = entity.Dew, Precipitation = entity.Precipitation, Date = entity.Date, UserId = entity.UserId });
            }
        }

        public IEnumerable<Day> Read()
        {
            throw new Exception("Invalid operation for this type");
        }

        public IEnumerable<Day> Read(string month)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<Day>($"SELECT Id, WeatherStationId, Temperature, Dew, Precipitation, Date, UserId FROM {month}");
            }
        }

        public IEnumerable<Day> Read(string month, int weatherStationId)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<Day>($"SELECT Id, WeatherStationId, Temperature, Dew, Precipitation, Date, UserId FROM {month} WHERE WeatherStationId = @WeatherStationId", new { WeatherStationId = weatherStationId });
            }
        }

        public IEnumerable<Day> ReadByUser(string month, int userId)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<Day>($"SELECT Id, WeatherStationId, Temperature, Dew, Precipitation, Date, UserId FROM {month} WHERE UserId = @UserId", new { UserId = userId });
            }
        }

        public IEnumerable<Day> Read(string month, int weatherStationId, int userId)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<Day>($"SELECT Id, WeatherStationId, Temperature, Dew, Precipitation, Date, UserId FROM {month} WHERE WeatherStationId = @WeatherStationId AND UserId = @UserId", new { WeatherStationId = weatherStationId, UserId = userId });
            }
        }

        public IEnumerable<Day> Read(string month, int weatherStationId, int userId, DateTime date)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                DateTime dateFrom = new DateTime(date.Year, date.Month, 1);

                return dbConnection.Query<Day>($"SELECT Id, WeatherStationId, Temperature, Dew, Precipitation, Date, UserId FROM {month} WHERE WeatherStationId = @WeatherStationId AND UserId = @UserId AND Date BETWEEN @DateFrom AND @DateTo", new { WeatherStationId = weatherStationId, UserId = userId, DateFrom = dateFrom, DateTo = date });
            }
        }

        public void Update(Day replaceable, Day replacer)
        {
            throw new Exception("Invalid operation for this type");
        }

        public void Update(Day replaceable, Day replacer, string month)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute($"UPDATE {month} SET WeatherStationId = @WeatherStationId, Temperature = @Temperature, Dew = @Dew, Precipitation = @Precipitation, Date = @Date, UserId = @UserId WHERE Id = @Id", new { WeatherStationId = replacer.WeatherStationId, Temperature = replacer.Temperature, Dew = replacer.Dew, Precipitation = replacer.Precipitation, Date = replacer.Date, UserId = replacer.UserId, Id = replaceable.Id });
            }
        }

        public void Delete(Day entity)
        {
            throw new Exception("Invalid operation for this type");
        }

        public void Delete(Day entity, string month)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute($"DELETE FROM {month} WHERE Id = @Id", new { Id = entity.Id });
            }
        }
    }
}
