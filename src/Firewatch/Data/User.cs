using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public class User : IEntity
    {
        public int Id { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public int UserTypeId { get; private set; }

        public User(int id, string username, string password, int userTypeId)
        {
            Id = id;
            Username = username;
            Password = password;
            UserTypeId = userTypeId;
        }
    }
}
