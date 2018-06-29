using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class CreateOperationCommand : FirstStepCommand<CreateOperationIntent, Account>
    {
        public CreateOperationCommand(
            ICommandContext<CreateOperationIntent> context,
            IFirstStepBusinessManager manager) : base(context, manager)
        {
        }

        public override async Task<IResult<Account>> Execute(CreateOperationIntent createOperationArgument)
        {
            var transaction = createOperationArgument.Transaction;

            var senderAccount = HandleResult(
                createOperationArgument,
                () => BusinessManager.AddOperation(new Operation(transaction.SenderKey, transaction)));
            
            await HandleResult(
                createOperationArgument,
                () => BusinessManager.AddOperation(new Operation(transaction.RecipientKey, transaction)));

            return await senderAccount;
        }
    }
}
