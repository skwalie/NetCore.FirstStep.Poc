using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput Map(TInput input);
    }
}
