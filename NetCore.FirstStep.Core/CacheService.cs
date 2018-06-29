using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public abstract class CacheService<TIntent, TData> : 
        ICacheService<TIntent, TData>
        where TIntent : IIntent
    {
        private readonly ICacheKeyBuilder<TIntent> _keyBuilder;

        public CacheService(ICacheKeyBuilder<TIntent> keyBuilder)
        {
            _keyBuilder = keyBuilder;
        }

        protected ICacheKeyBuilder<TIntent> KeyBuilder => _keyBuilder;

        public abstract TData Create(TIntent intent, TData data, TimeSpan relativeExpiration);
        public abstract TData Get(TIntent intent);
        public abstract void Invalidate(TIntent intent);
    }
}
