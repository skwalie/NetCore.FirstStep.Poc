using System;

namespace NetCore.FirstStep.Core
{
    public class FailureDetail
    {
        public FailureDetail(FailureReason reason, Exception exception, params string[] messages)
        {
            Reason = reason;
            Exception = exception;
            Messages = messages;
        }

        public FailureDetail(FailureReason reason,  params string[] messages) : this(reason, null, messages)
        {
        }

        public FailureReason Reason { get; private set; }
        public Exception Exception { get; private set; }
        public string[] Messages { get; private set; }
    }
}