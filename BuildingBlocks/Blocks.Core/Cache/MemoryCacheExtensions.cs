namespace Blocks.Core.Cache
{
    using Microsoft.Extensions.Caching.Memory;

    public static class MemoryCacheExtensions
    {
        public static T GetOrCreateByType<T>(this IMemoryCache memoryCache, Func<ICacheEntry, T> factory) => memoryCache.GetOrCreate(typeof(T).FullName!, factory)!;
    }
}
