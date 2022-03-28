using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using Dapper;

namespace FirewatchDaemon
{
    public static class Actions
    {
        public static void ConvertToFile(string source, string destination)
        {
            MapInfo file = new MapInfo(source);

            var polygons = file.Parse();

            using (FileStream fileStream = new FileStream(destination, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    foreach (var polygon in polygons)
                    {
                        writer.WriteLine(polygon.ToString());
                    }
                }
            }
        }

        public static void ConvertToDatabase(string source, string server = "localhost", string database = "firewatch", string user = "root", string password = "")
        {
            using (var dbConnection = new MySqlConnection($"Server = {server}; Database = {database}; Uid = {user}; Pwd = {password};"))
            {
                MapInfo file = new MapInfo(source);

                var polygons = file.Parse();

                foreach (var polygon in polygons)
                {
                    dbConnection.Execute("INSERT INTO Polygons (Border, Center) VALUES (@Border, @Center)", new { Border = polygon.GetString(), Center = polygon.Center.ToString() });
                }
            }
        }
    }
}
