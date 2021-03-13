using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

using Petroteks.MvcUi.StringInfos;

namespace Petroteks.MvcUi.Services
{
    public class CacheManager : ICacheService
    {
        private readonly IMemoryCache memoryCache;

        public CacheManager(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public T UpdateGet<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false)
        {
            Remove(key);
            return Get(key,func,cacheItemPriority,isLoop);
        }
        public async Task<T> UpdateGetAsync<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false)
        {
            Remove(key);
            return await GetAsync(key, func, cacheItemPriority, isLoop);
        }
        public T Get<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false)
        {
            var data = memoryCache.GetOrCreate(key, entry =>
           {
               entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(CacheInfo.DefaultCacheDay);
               entry.SetPriority(cacheItemPriority);
               return func();
           });


            if (data == null && !isLoop)
            {
                Remove(key);
                return Get(key, func, cacheItemPriority, !isLoop);
            }
            return data;
        }

        public async Task<T> GetAsync<T>(string key, Func<T> func, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal, bool isLoop = false)
        {
            var data = await memoryCache.GetOrCreateAsync(key, entry =>
             {
                 entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(CacheInfo.DefaultCacheDay);
                 entry.SetPriority(cacheItemPriority);
                 return Task.Run(() => func());
             });

            if (data == null && !isLoop)
            {
                Remove(key);
                return await GetAsync(key, func, cacheItemPriority, !isLoop);
            }
            return data;
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }

        public void Save<T>(string key, T value, CacheItemPriority cacheItemPriority = CacheItemPriority.Normal)
        {
            memoryCache.Set(key, value, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(CacheInfo.DefaultCacheDay),
                Priority= cacheItemPriority
            });
        }

        public bool Get(string key)
        {
            return memoryCache.Get(key)!=null;
        }
    }
}
