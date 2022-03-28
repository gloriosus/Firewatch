using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public interface IDataAccessObject<Entity> where Entity : IEntity
    {
        void Create(Entity entity);

        IEnumerable<Entity> Read();

        void Update(Entity replaceable, Entity replacer);

        void Delete(Entity entity);
    }
}
