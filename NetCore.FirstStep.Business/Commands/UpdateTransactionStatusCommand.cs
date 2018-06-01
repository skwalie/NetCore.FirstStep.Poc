using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class UpdateTransactionStatusCommand : FirstStepCommand<UpdateTransactionStatusArgument, Transaction>
    {
        public UpdateTransactionStatusCommand(IFirstStepBusinessManager manager) : base(manager)
        {
        }

        protected override async Task<IResult<Transaction>> ProcessCommand(UpdateTransactionStatusArgument argument)
        {
            var transaction = await BusinessManager.UpdateTransactionStatus(argument.TransactionId, argument.Status);
            return transaction.ToResult();
        }
    }
}
