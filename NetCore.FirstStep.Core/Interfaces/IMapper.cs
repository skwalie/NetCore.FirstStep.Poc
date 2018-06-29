using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IMapper<TOutput>
    {
        TOutput Map();
    }

    public interface IMapper<TInput, TOutput>
    {
        TOutput Map(TInput input);
    }

    public interface IMapper<TInput1, TInput2, TOutput>
    {
        TOutput Map(TInput1 input1, TInput2 input);
    }

    public interface IMapper<TInput1, TInput2, TInput3, TOutput>
    {
        TOutput Map(TInput1 input1, TInput2 input2, TInput3 input3);
    }
}
