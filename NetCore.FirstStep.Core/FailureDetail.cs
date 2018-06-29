using System;
using System.Collections.Generic;

namespace NetCore.FirstStep.Core
{
    public class FailureDetail
    {
        public FailureDetail(FailureReason reason, Exception exception, params KeyValuePair<string, object>[] messages)
        {
            Reason = reason;
            Exception = exception;
            Messages = messages;
        }

        public FailureDetail(FailureReason reason,  params KeyValuePair<string, object>[] messages) : this(reason, null, messages)
        {
        }

        public FailureReason Reason { get; private set; }
        public Exception Exception { get; private set; }
        public KeyValuePair<string, object>[] Messages { get; private set; }
    }
}