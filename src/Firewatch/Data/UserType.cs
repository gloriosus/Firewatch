using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public class UserType : IEntity
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public UserType(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
