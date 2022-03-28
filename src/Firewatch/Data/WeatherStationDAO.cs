using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;

namespace Firewatch.Data
{
    public class WeatherStationDAO : Singleton<WeatherStationDAO>, IDataAccessObject<WeatherStation>
    {
        public void Create(WeatherStation entity)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute("INSERT INTO WeatherStations (Title, Coordinates, Url, UserId) VALUES (@Title, @Coordinates, @Url, @UserId)", new { Title = entity.Title, Coordinates = entity.Coordinates, Url = entity.Url, UserId = entity.UserId });
            }
        }

        public IEnumerable<WeatherStation> Read()
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<WeatherStation>("SELECT Id, Title, Coordinates, Url, UserId FROM WeatherStations");
            }
        }

        public IEnumerable<WeatherStation> Read(int userId)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<WeatherStation>("SELECT Id, Title, Coordinates, Url, UserId FROM WeatherStations WHERE UserId = @UserId", new { UserId = userId });
            }
        }

        public string GetName(int weatherStationId)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<WeatherStation>("SELECT Id, Title, Coordinates, Url, UserId FROM WeatherStations WHERE Id = @Id", new { Id = weatherStationId }).First().Title;
            }
        }

        public int GetId(string title)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<WeatherStation>("SELECT Id, Title, Coordinates, Url, UserId FROM WeatherStations WHERE Title = @Title", new { Title = title }).First().Id;
            }
        }

        public void Update(WeatherStation replaceable, WeatherStation replacer)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute("UPDATE WeatherStations SET Title = @Title, Coordinates = @Coordinates, Url = @Url, UserId = @UserId WHERE Id = @Id", new { Title = replacer.Title, Coordinates = replacer.Coordinates, Url = replacer.Url, UserId = replacer.UserId, Id = replaceable.Id });
            }
        }

        public void Delete(WeatherStation entity)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute("DELETE FROM WeatherStations WHERE Id = @Id", new { Id = entity.Id });
            }
        }
    }
}
