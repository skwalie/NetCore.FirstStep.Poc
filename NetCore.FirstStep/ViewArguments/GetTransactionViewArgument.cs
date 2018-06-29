using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.ViewArguments 
{
    public class GetTransactionViewArgument : IViewArgument<GetTransactionIntent>
    {
        public string TransactionId { get; set; }

        public GetTransactionIntent Map(IRequestContext context)
        {
            return new GetTransactionIntent(TransactionId);
        }
    }
}
