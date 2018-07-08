using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.FirstStep.Core;
using System.Linq;

namespace NetCore.FirstStep.Testing
{
    public class TestResult<T> : IResult<T>
    {
        private List<FailureDetail> _failureDetails;

        public TestResult()
        {
            _failureDetails = new List<FailureDetail>();
        }

        public T Content { get; set; }
        public IEnumerable<FailureDetail> FailureDetails
        {
            get { return _failureDetails; }
            set { _failureDetails = value?.ToList(); }
        }

        public bool IsSuccessful => !FailureDetails.Any();

        public void AddFailureDetail(FailureReason reason, string code, string message)
        {
            _failureDetails.Add(new FailureDetail(reason, new KeyValuePair<string, object>(code, message)));
        }

        public void AddFailureDetails(params FailureDetail[] failureDetails)
        {
            _failureDetails.AddRange(failureDetails);
        }
    }
}