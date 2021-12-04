using System;
using Microsoft.Extensions.Caching.Memory;

namespace StudyDemo3_Dapper
{
    public class MemoryHelper
    {
        private static IMemoryCache _memoryCache = null;
        static MemoryHelper()
        {
            if(_memoryCache == null)
            {
                _memoryCache = new MemoryCache(new MemoryCacheOptions());
            }
        }

        public static void Set(string key, object value)
        {
            _memoryCache.Set(key, value, TimeSpan.FromSeconds(60));
        }

        public static object Get(string key)
        {
            if(!_memoryCache.TryGetValue(key, out object value))
            {
                value = new DapperExtHelper<Post>().GetAll();
                Set(key, value);
            }
            return value;
        }
    }
}
