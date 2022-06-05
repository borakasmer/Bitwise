using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Caching
{
    public interface IRedisCacheService
    {
        T Get<T>(string key, long db = 0);
        IList<T> GetAll<T>(string key, long db = 0);
        void Set(string key, object data, long db = 0);
        void Set(string key, object data, DateTime time, long db = 0);
        void SetAll<T>(IDictionary<string, T> values, long db = 0);
        void Remove(string key, long db = 0);
    }
}
