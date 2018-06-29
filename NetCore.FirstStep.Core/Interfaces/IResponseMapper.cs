using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IResponseMapper<TIntent, TDomain> : 
        IMapper<TDomain, IRequestContextReader, IViewModel<TIntent>>
        where TIntent : IIntent
    {
    }
}
