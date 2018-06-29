using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class SubmitTransactionCommand : FirstStepCommand<SubmitTransactionIntent, Transaction>
    {
        private readonly IQuery<GetAccountIntent, Account> _getAccountQuery;
        private readonly ICommand<UpdateTransactionStatusIntent, Transaction> _updateTransactionStatusQuery;

        public SubmitTransactionCommand(
            ICommandContext<SubmitTransactionIntent> context,
            IFirstStepBusinessManager manager,
            IQuery<GetAccountIntent, Account> getAccountQuery,
            ICommand<UpdateTransactionStatusIntent, Transaction> updateTransactionStatusCommand) : base(context, manager)
        {
            _getAccountQuery = getAccountQuery;
            _updateTransactionStatusQuery = updateTransactionStatusCommand;
        }

        public override async Task<IResult<Transaction>> Execute(SubmitTransactionIntent argument)
        {
            if (argument.SenderKey == argument.RecipientKey)
            {
                return FailureReason.ExpectationFailed.ToErrorResult<Transaction>("SubmitTransactionIntent", "identical keys");
            }

            var senderResult = await _getAccountQuery.Fetch(new GetAccountIntent(argument.SenderKey));
            var accountResult = await _getAccountQuery.Fetch(new GetAccountIntent(argument.RecipientKey));

            if (! (senderResult.IsSuccessful && accountResult.IsSuccessful))
            {
                return senderResult.FailureDetails
                    .Union(accountResult.FailureDetails)
                    .ToErrorResult<Transaction>();
            }

            var transaction = await HandleResult(
                argument,
                () => BusinessManager.CreateTransaction(argument.SenderKey, argument.RecipientKey, argument.Sum));

            if(transaction.IsSuccessful)
            {
                var setStatusResult = await _updateTransactionStatusQuery.Execute(new UpdateTransactionStatusIntent(
                    transaction.Content.Id,
                    Transaction.Status.Submitted));

                if (!setStatusResult.IsSuccessful)
                {
                    transaction.AddFailureDetails(setStatusResult.FailureDetails.ToArray());
                }

                return setStatusResult;
            }

            return transaction;
        }
    }
}
