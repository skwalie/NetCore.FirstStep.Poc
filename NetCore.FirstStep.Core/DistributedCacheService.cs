using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public class DistributedCacheService<TQueryArgument, TOutput> : CacheService<TQueryArgument, TOutput>
        where TQueryArgument : ICacheableQueryArgument
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheService(ICacheKeyBuilder<TQueryArgument> keyBuilder, IDistributedCache distributedCache) : base(keyBuilder)
        {
            _distributedCache = distributedCache;
        }


        public override TOutput Create(TQueryArgument argument, TOutput output)
        {
            var key = KeyBuilder.GetCacheKey(argument);

            _distributedCache.Set(
                key,
                Serialize(output),
                new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = argument.RelativeExpiration });
            
            return output;
        }

        private byte[] Serialize(TOutput entity)
        {
            var formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, entity);
                return stream.ToArray();
            }
        }

        private TOutput Deserialize(byte[] bytes)
        {
            var formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                return (TOutput)formatter.Deserialize(stream);
            }
        }

        public override void Delete(TQueryArgument entity)
        {
            var key = KeyBuilder.GetCacheKey(entity);
            _distributedCache.Remove(key);
        }

        public override TOutput Get(TQueryArgument argument)
        {
            var key = KeyBuilder.GetCacheKey(argument);
            var bytes = _distributedCache.Get(key);
            return Deserialize(bytes);
        }

        public override void Update(TQueryArgument argument, TOutput output)
        {
            var key = KeyBuilder.GetCacheKey(argument);

            _distributedCache.Set(
                key,
                Serialize(output),
                new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = argument.RelativeExpiration });
        }
    }
}
