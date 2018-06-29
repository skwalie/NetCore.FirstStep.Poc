using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IQueryContext<TIntent> : IRequestContext
        //where TIntent : IQueryIntent
    {

    }
}
