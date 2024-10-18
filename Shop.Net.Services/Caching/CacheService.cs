using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Caching;

public class CacheService : ICacheService
{
    private readonly Dictionary<string, CancellationTokenSource> cachePrefixCancellationTokens;
    private readonly IMemoryCache memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
        cachePrefixCancellationTokens = new Dictionary<string, CancellationTokenSource>();
    }

    public CacheKey PrepareCacheKey(CacheKey cacheKey, params object[] tokens)
    {
        var key = string.Format(cacheKey.Key, tokens);
        return new CacheKey(key, cacheKey.Prefix);
    }

    public async Task<IList<T>> GetAllAsync<T>(CacheKey cacheKey, Func<Task<IList<T>>> dbCall)
    {
        if (memoryCache.TryGetValue(cacheKey.Key, out IList<T>? value))
        {
            return value ?? [];
        }

        value = await dbCall();

        if (!cachePrefixCancellationTokens.TryGetValue(cacheKey.Prefix, out var cts))
        {
            cts = new CancellationTokenSource();
            cachePrefixCancellationTokens.Add(cacheKey.Prefix, cts);
        }
        else if (cts is null)
        {
            cts = new CancellationTokenSource();
            cachePrefixCancellationTokens[cacheKey.Prefix] = cts;
        }

        var cacheEntryOption = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetAbsoluteExpiration(TimeSpan.FromHours(6))
            .AddExpirationToken(new CancellationChangeToken(cts.Token));

        return memoryCache.Set(cacheKey.Key, value, cacheEntryOption);
    }

    public async Task<T?> GetAsync<T>(CacheKey cacheKey, Func<Task<T?>> dbCall)
    {
        if (memoryCache.TryGetValue(cacheKey.Key, out T? value))
        {
            return value;
        }

        value = await dbCall();

        if (!cachePrefixCancellationTokens.TryGetValue(cacheKey.Prefix, out var cts))
        {
            cts = new CancellationTokenSource();
            cachePrefixCancellationTokens.Add(cacheKey.Prefix, cts);
        }
        else if (cts is null)
        {
            cts = new CancellationTokenSource();
            cachePrefixCancellationTokens[cacheKey.Prefix] = cts;
        }

        var cacheEntryOption = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetAbsoluteExpiration(TimeSpan.FromHours(6))
            .AddExpirationToken(new CancellationChangeToken(cts.Token));

        return memoryCache.Set(cacheKey.Key, value, cacheEntryOption);
    }

    public void Remove(CacheKey cacheKey)
    {
        memoryCache.Remove(cacheKey.Key);
    }

    public void RemoveByPrefix(string prefix)
    {
        if (cachePrefixCancellationTokens.TryGetValue(prefix, out var cts) && cts is not null)
        {
            cts.Cancel();
            cachePrefixCancellationTokens.Remove(prefix);
        }
    }

    public void ClearCache()
    {
        foreach (var cts in cachePrefixCancellationTokens.Values)
        {
            cts.Cancel();
        }

        cachePrefixCancellationTokens.Clear();
    }
}