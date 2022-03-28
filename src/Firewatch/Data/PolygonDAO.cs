using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;

namespace Firewatch.Data
{
    public class PolygonDAO : Singleton<PolygonDAO>, IDataAccessObject<Polygon>
    {
        public void Create(Polygon entity)
        {
            throw new Exception("Invalid operation for this type");
        }

        public IEnumerable<Polygon> Read()
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<Polygon>("SELECT Id, Border, Center FROM Polygons");
            }
        }

        public void Update(Polygon replaceable, Polygon replacer)
        {
            throw new Exception("Invalid operation for this type");
        }

        public void Delete(Polygon entity)
        {
            throw new Exception("Invalid operation for this type");
        }
    }
}
