using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.ViewArguments
{
    public class SubmitTransactionViewArgument : 
        IViewArgument<SubmitTransactionIntent>
    {
        public string SenderKey { get; set; }
        public string RecipientKey { get; set; }
        public decimal Sum { get; set; }

        public SubmitTransactionIntent Map(IRequestContext context)
        {
            return new SubmitTransactionIntent(SenderKey, RecipientKey, Sum);
        }
    }
}