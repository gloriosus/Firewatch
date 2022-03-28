using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
using Dapper;
using System.Globalization;

namespace FirewatchDaemon
{
    class Program
    {
        static void Main(string[] args)
        {
            string src = @"C:\Users\vecryd\Desktop\5k.mif";
            Actions.ConvertToDatabase(src);

            Console.WriteLine("Completed");

            MeteorologicalData meteo = new MeteorologicalData();

            meteo.GenerateRandomData(6, 1);
            meteo.GenerateRandomData(6, 2);
            meteo.GenerateRandomData(6, 3);
            meteo.GenerateRandomData(6, 4);
            meteo.GenerateRandomData(6, 5);
            meteo.GenerateRandomData(6, 6);
            meteo.GenerateRandomData(6, 7);
            meteo.GenerateRandomData(6, 8);
            meteo.GenerateRandomData(6, 9);

            Console.WriteLine("Completed");

            /*
            bool cycled = true;

            while (cycled)
            {
                string action = Console.ReadLine();

                switch (action)
                {
                    case ".convert":
                        string source = Console.ReadLine();
                        string destination = Console.ReadLine();
                        Actions.ConvertToFile(source, destination);
                        break;

                    case ".db":
                        string src = Console.ReadLine();
                        Actions.ConvertToDatabase(src);
                        break;

                    case ".exit":
                        cycled = false;
                        break;
                }

                Console.WriteLine("Completed ...");
            }
            */

            Console.ReadKey();
        }
    }
}
