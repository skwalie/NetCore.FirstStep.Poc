using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{


    public class CacheKeyBuilder<TQueryArgument> : ICacheKeyBuilder<TQueryArgument>
    {
        private readonly Func<TQueryArgument, string> _getCacheKeyFunc;

        public CacheKeyBuilder(Func<TQueryArgument, string> getCacheKeyFunc)
        {
            _getCacheKeyFunc = getCacheKeyFunc;
        }
   
        public string GetCacheKey(TQueryArgument argument)
        {
            return _getCacheKeyFunc(argument);
        }
    }
}
