using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class UpdateTransactionStatusCommand : FirstStepCommand<UpdateTransactionStatusIntent, Transaction>
    {
        public UpdateTransactionStatusCommand(
            ICommandContext<UpdateTransactionStatusIntent> context,
            IFirstStepBusinessManager manager) : base(context, manager)
        {
        }

        public override async Task<IResult<Transaction>> Execute(UpdateTransactionStatusIntent argument)
        {
            return await HandleResult(
                argument,
                () => BusinessManager.UpdateTransactionStatus(argument.TransactionId, argument.Status));
        }
    }
}
