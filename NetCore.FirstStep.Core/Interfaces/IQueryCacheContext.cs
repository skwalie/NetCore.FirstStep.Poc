﻿using System;

namespace NetCore.FirstStep.Core
{
    public interface IQueryCacheContext<TIntent> : IQueryContext<TIntent>
        where TIntent : IQueryIntent
    {
        bool UseCache { get; set; }
        TimeSpan RelativeExpiration { get; set; }
    }
}
