using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface ICacheableQueryArgument
    {
        bool UseCache { get; }
        TimeSpan? RelativeExpiration { get; }
    }
}
