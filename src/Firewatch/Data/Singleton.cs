using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firewatch.Data
{
    public abstract class Singleton<DataAccessObject> where DataAccessObject : class, new()
    {
        private static DataAccessObject uniqueInstance;
        private static DataAccessObject GetInstance()
        {
            if (uniqueInstance is null)
            {
                uniqueInstance = new DataAccessObject();
            }

            return uniqueInstance;
        }

        public static DataAccessObject Instance
        {
            get
            {
                return GetInstance();
            }
        }
    }
}
