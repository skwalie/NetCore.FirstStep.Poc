using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface ICacheKeyBuilder<TQueryArgument>
    {
        string GetCacheKey(TQueryArgument domainObject);
    }
}
