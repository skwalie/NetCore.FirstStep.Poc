using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public class MemoryCacheService<TQueryArgument, TOutput> : CacheService<TQueryArgument, TOutput>
        where TQueryArgument : ICacheableQueryArgument
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(ICacheKeyBuilder<TQueryArgument> keyBuilder, IMemoryCache memoryCache) : base(keyBuilder)
        {
            _memoryCache = memoryCache;
        }
        
        public override TOutput Create(TQueryArgument argument, TOutput result)
        {
            var key = KeyBuilder.GetCacheKey(argument);

            return argument.RelativeExpiration.HasValue ?
                _memoryCache.Set(key, result, argument.RelativeExpiration.Value) :
                _memoryCache.Set(key, result);
        }

        public override TOutput Get(TQueryArgument argument)
        {
            return _memoryCache.Get<TOutput>(argument);
        }

        public override void Delete(TQueryArgument argument)
        {
            var key = KeyBuilder.GetCacheKey(argument);
            _memoryCache.Remove(key);
        }

        public override void Update(TQueryArgument argument, TOutput data)
        {
            var key = KeyBuilder.GetCacheKey(argument);
            _memoryCache.Set(key, data);
        }
    }
}
