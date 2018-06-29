using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface ICacheKeyBuilder<TIntent>
    {
        CacheKey GetCacheKey(TIntent intent);
    }
}
