using NetCore.FirstStep.Core;
using System;

namespace NetCore.FirstStep.Core
{
    public class IntentException : Exception
    {
        private readonly IIntent _intent;
        private readonly object _exceptionSource;

        public IntentException(IIntent intent, object exceptionSource, Exception innerException) : base(exceptionSource.GetType().Name, innerException)
        {
            _intent = intent;
            _exceptionSource = exceptionSource;
        }

        public IntentException(IIntent intent, object exceptionSource) : this(intent, exceptionSource, null)
        {

        }

        public IIntent Intent => _intent;
        public object ExceptionSource => _exceptionSource;
    }
}
