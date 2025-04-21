using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Caches
{
    public interface ICacheService
    {
        T GetData<T>(string key);

        bool SetData<T> (string key, T value, DateTimeOffset expirationTime);

        Object Delete(string key);
    }
}
