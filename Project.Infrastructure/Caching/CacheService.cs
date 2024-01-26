using Microsoft.Extensions.Caching.Memory;
using Project.Application.Common.Interfaces.Caching;

namespace Project.Infrastructure.Caching;

internal sealed class CacheService : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }


    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {

        T? result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
                return factory(cancellationToken);
            });
        return result!;
    }

    public async Task RemoveAsync(string key)
    {
        await Task.CompletedTask;
        var ok = _memoryCache.TryGetValue(key, out var result);
        if (ok)
            _memoryCache.Remove(key);
    }
}
