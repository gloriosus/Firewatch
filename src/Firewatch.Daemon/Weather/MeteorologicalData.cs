using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;
using System.Globalization;

namespace FirewatchDaemon
{
    class MeteorologicalData
    {
        public void DownloadActualData()
        {
            // Connecting to rp5.ru
        }

        public void GenerateRandomData(int month, int weatherStation)
        {
            MonthlyValues monthlyTemperature = AllowableValues.Temperature.First(monthlyValue => monthlyValue.Month.Equals(month));
            MonthlyValues monthlyDew = AllowableValues.Dew.First(monthlyValue => monthlyValue.Month.Equals(month));

            int days = DateTime.DaysInMonth(DateTime.Now.Year, month);

            using (var dbConnection = new MySqlConnection($"Server = localhost; Database = firewatch; Uid = root;"))
            {
                for (int i = 1; i <= days; i++)
                {
                    float temperature = GenerateFloat(monthlyTemperature.Min, monthlyTemperature.Max);
                    float dew = GenerateFloat(monthlyDew.Min, monthlyDew.Max);
                    float precipitation = -1.0F;

                    Random random = new Random();

                    bool isDrought = Convert.ToBoolean(random.Next(0, 3));

                    if (isDrought)
                    {
                        precipitation = 0;
                    }
                    else
                    {
                        precipitation = GenerateFloat(0.0F, 15.0F);
                    }

                    DateTime date = new DateTime(DateTime.Now.Year, month, i);

                    int userId = dbConnection.QueryFirst<int>("SELECT Id FROM Users WHERE Username = @Username", new { Username = "system" });

                    dbConnection.Execute($"INSERT INTO {date.ToString("MMMM", CultureInfo.InvariantCulture)} (WeatherStationId, Temperature, Dew, Precipitation, Date, UserId) VALUES (@WeatherStationId, @Temperature, @Dew, @Precipitation, @Date, @UserId)", new { WeatherStationId = weatherStation, Temperature = temperature, Dew = dew, Precipitation = precipitation, Date = date, UserId = userId });
                }
            }
        }

        private Random generator = new Random();

        private float GenerateFloat(float min, float max)
        {
            return (float)(generator.NextDouble() * (max - min) + min);
        }
    }
}
