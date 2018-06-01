using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetTransactionQuery : FirstStepQuery<GetTransactionArgument, Transaction>
    {
        public GetTransactionQuery(IFirstStepReadManager manager) : base(manager)
        {
        }

        protected override async Task<IResult<Transaction>> ProcessQuery(GetTransactionArgument argument)
        {
            var transaction = await BusinessManager.GetTransaction(argument.TransactionId);
            return transaction.ToResult();
        }
    }
}
