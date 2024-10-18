using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Caching;

public interface ICacheService
{
    CacheKey PrepareCacheKey(CacheKey cacheKey, params object[] tokens);

    Task<IList<T>> GetAllAsync<T>(CacheKey cacheKey, Func<Task<IList<T>>> dbCall);

    Task<T?> GetAsync<T>(CacheKey cacheKey, Func<Task<T?>> dbCall);

    void Remove(CacheKey cacheKey);

    void RemoveByPrefix(string prefix);

    void ClearCache();
}
