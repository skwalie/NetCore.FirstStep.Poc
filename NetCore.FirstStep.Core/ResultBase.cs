using System;
using System.Collections.Generic;
using System.Linq;

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

        public ResultBase(Exception exception, FailureReason reason) : 
            this(new FailureDetail(reason, exception, new KeyValuePair<string, object>("error", exception.Message)))
        {
        }

        public IEnumerable<FailureDetail> FailureDetails => _exceptionDetails;
        public bool IsSuccessful => !_exceptionDetails.Any();
        public T Content => _content;

        public void AddFailureDetail(FailureReason reason, string code, string message)
        {
            _exceptionDetails.Add(new FailureDetail(reason, new KeyValuePair<string, object>(code, message)));
        }

        public void AddFailureDetails(params FailureDetail[] failureDetails)
        {
            failureDetails?.ToList().ForEach(_exceptionDetails.Add);
        }
    }
}
