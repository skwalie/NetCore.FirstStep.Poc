using Microsoft.Extensions.Caching.Memory;
using System;

namespace NetCore.FirstStep.Core
{
    public class MemoryCacheService<TIntent, TOutput> : CacheService<TIntent, TOutput>
        where TIntent : IIntent
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(ICacheKeyBuilder<TIntent> keyBuilder, IMemoryCache memoryCache) : base(keyBuilder)
        {
            _memoryCache = memoryCache;
        }

        public override TOutput Create(TIntent intent, TOutput result, TimeSpan relativeExpiration)
        {
            var key = KeyBuilder.GetCacheKey(intent);
            return _memoryCache.Set(key, result, relativeExpiration);
        }

        public override TOutput Get(TIntent intent)
        {
            var key = KeyBuilder.GetCacheKey(intent);
            return key == null ? default(TOutput) : _memoryCache.Get<TOutput>(key);
        }

        public override void Invalidate(TIntent intent)
        {
            var key = KeyBuilder.GetCacheKey(intent);

            if (key != null)
            {
                Invalidate(key);
            }
        }

        private void Invalidate(CacheKey cacheKey)
        {
            foreach (var dependency in cacheKey.Dependencies)
            {
                Invalidate(dependency);
            }

            _memoryCache.Remove(cacheKey);
        }
    }
}
