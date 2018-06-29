using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core.Interfaces
{
    public interface ICacheableIntent : IIntent
    {
        bool UseCache { get; }
        TimeSpan RelativeExpiration { get; }
    }
}
