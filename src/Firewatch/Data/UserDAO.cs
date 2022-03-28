using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;

namespace Firewatch.Data
{
    public class UserDAO : Singleton<UserDAO>, IDataAccessObject<User>
    {
        public void Create(User entity)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute("INSERT INTO Users (Username, Password, UserTypeId) VALUES (@Nickname, @Password, @UserTypeId)", new { Nickname = entity.Username, Password = entity.Password, UserTypeId = entity.UserTypeId });
            }
        }

        public IEnumerable<User> Read()
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                return dbConnection.Query<User>("SELECT Id, Username, Password, UserTypeId FROM Users");
            }
        }

        public void Update(User replaceable, User replacer)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute("UPDATE Users SET Username = @Username, Password = @Password, UserTypeId = @UserTypeId WHERE Id = @Id", new { Username = replacer.Username, Password = replacer.Password, UserTypeId = replacer.UserTypeId, Id = replaceable.Id });
            }
        }

        public void Delete(User entity)
        {
            using (var dbConnection = new MySqlConnection("Server = localhost; Database = firewatch; Uid = root;"))
            {
                dbConnection.Execute("DELETE FROM Users WHERE Id = @Id", new { Id = entity.Id });
            }
        }
    }
}
