using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetTransactionQuery : FirstStepQuery<GetTransactionIntent, Transaction>
    {
        public GetTransactionQuery(
            IQueryContext<GetTransactionIntent> context,
            IFirstStepReadManager manager) : base(context, manager)
        {
        }

        public override async Task<IResult<Transaction>> Fetch(GetTransactionIntent argument)
        {
            var transaction = await BusinessManager.GetTransaction(argument.Key);

            if(transaction == null)
            {
                return FailureReason.ExpectationFailed.ToErrorResult<Transaction>("GetTransactionIntent", "transaction not found");
            }

            return transaction.ToResult();
        }
    }
}
