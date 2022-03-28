using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;

namespace Firewatch.Data
{
    public class UserTypeDAO : Singleton<UserTypeDAO>, IDataAccessObject<UserType>
    {
        public void Create(UserType entity)
        {
            throw new Exception("Invalid operation for this type");
        }

        public IEnumerable<UserType> Read()
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<UserType>("SELECT Id, Title FROM UserTypes");
            }
        }

        public void Update(UserType replaceable, UserType replacer)
        {
            throw new Exception("Invalid operation for this type");
        }

        public void Delete(UserType entity)
        {
            throw new Exception("Invalid operation for this type");
        }
    }
}
