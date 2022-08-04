using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Bussines.Concrete
{
    public class CacheExample:ICacheExample
    {
        private IDistributedCache _distributedCache;
        private IMemoryCache _memoryCache;

        public CacheExample(IDistributedCache distCache,IMemoryCache memCache)
        {
            _distributedCache = distCache;
            _memoryCache = memCache;
        }

        public void DistSetString(string key, string value)
        {
            _distributedCache.SetString(key,value);
        }

        public void DistSetList(string key)
        {
            var arrayNumber = new int[] { 1, 2, 4, 6 };
            var strArrayNumber = System.Text.Json.JsonSerializer.Serialize(arrayNumber);
            _distributedCache.SetString(key,strArrayNumber);
        }

        public string DistGetValue(string key)
        {
            return _distributedCache.GetString(key);
        }

        public void InMemSetString(string key, string value)
        {
            _memoryCache.Set(key, value);
        }

        public void InMemSetObject(string key, object value)
        {
            _memoryCache.Set(key, value);
        }

        public T InMemGetValue<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
    }
}
