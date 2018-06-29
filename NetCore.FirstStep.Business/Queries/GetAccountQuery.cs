using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetAccountQuery : FirstStepQuery<GetAccountIntent, Account>
    {
        public GetAccountQuery(
            IQueryContext<GetAccountIntent> context,
            IFirstStepReadManager businessManager) : base(context, businessManager)
        {
        }

        public override async Task<IResult<Account>> Fetch(GetAccountIntent argument)
        {
            var account = await BusinessManager.GetAccount(argument.Key);

            if(account == null)
            {
                return FailureReason.ExpectationFailed.ToErrorResult<Account>("GetAccountIntent", "account not found");
            }

            return account.ToResult();
        }
    }
}
