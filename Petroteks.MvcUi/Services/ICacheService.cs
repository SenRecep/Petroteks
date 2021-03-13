using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

namespace Petroteks.MvcUi.Services
{
    public interface ICacheService
    {
        void Remove(string key);

        T UpdateGet<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false);
        Task<T> UpdateGetAsync<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false);
        Task<T> GetAsync<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false);
        T Get<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false);
        bool Get(string key);
        void Save<T>(string key, T value, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal);
    }
}
