using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetAccountQuery : FirstStepQuery<GetAccountArgument, Account>
    {
        public GetAccountQuery(IFirstStepReadManager businessManager) : base(businessManager)
        {
        }

        protected override async Task<IResult<Account>> ProcessQuery(GetAccountArgument argument)
        {
            var result = await BusinessManager.GetAccount(argument.Key);
            return result.ToResult();
        }
    }
}
