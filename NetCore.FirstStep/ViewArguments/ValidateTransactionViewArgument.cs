using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.ViewArguments
{
    public class ValidateTransactionViewArgument : IViewArgument<ValidateTransactionIntent>
    {
        public string TransactionId { get; set; }

        public ValidateTransactionIntent Map(IRequestContext input)
        {
            return new ValidateTransactionIntent(TransactionId);
        }
    }
}
