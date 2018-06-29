using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IResult
    {
        IEnumerable<FailureDetail> FailureDetails { get; }
        bool IsSuccessful { get; }
        void AddFailureDetail(FailureReason reason, string code, string message);
        void AddFailureDetails(params FailureDetail[] failureDetails);
    }
    
    public interface IResult<T> : IResult
    {
        T Content { get; }
    }
}
