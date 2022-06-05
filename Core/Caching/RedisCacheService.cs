using Core.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        #region Fields

        public readonly IOptions<BitwiseConfig> _bitwiseConfig;
        private readonly RedisEndpoint conf = null;

        #endregion

        //config set requirepass fl@rp1$19C23
        public RedisCacheService(IOptions<BitwiseConfig> bitwiseConfig)
        {
            _bitwiseConfig = bitwiseConfig;
            conf = new RedisEndpoint { Host = _bitwiseConfig.Value.RedisEndPoint, Port = Convert.ToInt32(_bitwiseConfig.Value.RedisPort), Password = _bitwiseConfig.Value.RedisPassword };
        }
        public T Get<T>(string key, long db = 0)
        {
            try
            {
                conf.Db = db;
                using (IRedisClient client = new RedisClient(conf))
                {
                    return client.Get<T>(key);
                }
            }
            catch
            {
                throw new Exception("Redis Not Available");
                //return default;
            }
        }

        public IList<T> GetAll<T>(string key, long db = 0)
        {
            try
            {
                conf.Db = db;
                using (IRedisClient client = new RedisClient(conf))
                {
                    var keys = client.SearchKeys(key);
                    if (keys.Any())
                    {
                        IEnumerable<T> dataList = client.GetAll<T>(keys).Values;
                        return dataList.ToList();
                    }
                    return new List<T>();
                }
            }
            catch
            {

                throw new Exception("Redis Not Available");
            }
        }

        public void Set(string key, object data, long db = 0)
        {
            Set(key, data, DateTime.Now.AddMinutes(60), db);
        }

        public void Set(string key, object data, DateTime time, long db = 0)
        {
            try
            {
                conf.Db = db;
                using (IRedisClient client = new RedisClient(conf))
                {
                    var dataSerialize = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    client.Set(key, Encoding.UTF8.GetBytes(dataSerialize), time);
                }
            }
            catch
            {
                throw new Exception("Redis Not Available");
            }
        }

        public void SetAll<T>(IDictionary<string, T> values, long db = 0)
        {
            try
            {
                conf.Db = db;
                using (IRedisClient client = new RedisClient(conf))
                {
                    client.SetAll(values);
                }
            }
            catch
            {

                throw new Exception("Redis Not Available");
            }

        }


        public void Remove(string key, long db = 0)
        {
            try
            {
                conf.Db = db;
                using (IRedisClient client = new RedisClient(conf))
                {
                    client.Remove(key);
                }
            }
            catch
            {
                throw new Exception("Redis Not Available");
            }
        }

    }
}
