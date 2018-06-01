using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public abstract class CacheService<TQueryArgument, TData> : ICacheService<TQueryArgument, TData>
        where TQueryArgument : ICacheableQueryArgument
    {
        private readonly ICacheKeyBuilder<TQueryArgument> _keyBuilder;

        public CacheService(ICacheKeyBuilder<TQueryArgument> keyBuilder)
        {
            _keyBuilder = keyBuilder;
        }

        protected ICacheKeyBuilder<TQueryArgument> KeyBuilder => _keyBuilder;

        public abstract TData Create(TQueryArgument argument, TData data);
        public abstract TData Get(TQueryArgument argument);
        public abstract void Delete(TQueryArgument argument);
        public abstract void Update(TQueryArgument argument, TData data);
    }
}
