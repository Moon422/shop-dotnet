using System;
using System.Threading.Tasks;
using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Caching;

public interface ICacheService
{
    CacheKey PrepareCacheKey(CacheKey cacheKey, params string[] tokens);

    Task<T?> GetAsync<T>(CacheKey cacheKey, Func<Task<T>> dbCall);

    void Remove(CacheKey cacheKey);

    void RemoveByPrefix(string prefix);

    void ClearCache();
}
