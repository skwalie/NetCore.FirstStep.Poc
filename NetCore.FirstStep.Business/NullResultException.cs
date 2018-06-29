using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.Business
{
    public class NullResultException : IntentException
    {
        public NullResultException(IIntent intent, object exceptionSource) : base(intent, exceptionSource)
        {
        }
    }
}
