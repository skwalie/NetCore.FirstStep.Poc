using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IResult
    {
        IList<FailureDetail> ExceptionDetails { get; }
        bool IsSuccessful { get; }
    }

    public interface IResult<T> : IResult
    {
        T Content { get; }
    }
}
