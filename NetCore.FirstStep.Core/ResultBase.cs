using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public class ResultBase<T> : IResult<T>
    {
        private readonly T _content;
        private readonly IList<FailureDetail> _exceptionDetails;

        public ResultBase(params FailureDetail[] details)
        {
            _exceptionDetails = details?.ToList() ?? new List<FailureDetail>();
        }

        public ResultBase(T content) : this(null)
        {
            _content = content;
        }

        public ResultBase(Exception exception, FailureReason reason) : this(new FailureDetail(reason, exception, exception.Message))
        {
        }

        public IList<FailureDetail> ExceptionDetails => _exceptionDetails;
        public bool IsSuccessful => !_exceptionDetails.Any();
        public T Content => _content;


    }
}
