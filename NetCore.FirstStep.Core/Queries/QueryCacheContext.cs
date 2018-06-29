using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public class QueryCacheContext
    {
        public QueryCacheContext(bool useCache, TimeSpan relativeExpiration)
        {
            UseCache = useCache;
            RelativeExpiration = relativeExpiration;
        }

        public bool UseCache { get;  } 
        public TimeSpan RelativeExpiration { get;  }
    }
}
