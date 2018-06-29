using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class ValidateTransactionCommand : Command<ValidateTransactionIntent, Account>
    {
        private readonly IQuery<GetTransactionIntent, Transaction> _getTransactionQuery;
        private readonly IQuery<GetAccountIntent, Account> _getAccountQuery;
        private readonly ICommand<UpdateTransactionStatusIntent, Transaction> _updateStatusCommand;
        private readonly ICommand<CreateOperationIntent, Account> _createOperationCommand;

        public ValidateTransactionCommand(
            ICommandContext<ValidateTransactionIntent> context,
            IQuery<GetTransactionIntent, Transaction> getTransactionQuery,
            IQuery<GetAccountIntent, Account> getAccountQuery,
            ICommand<UpdateTransactionStatusIntent, Transaction> updateStatusCommand,
            ICommand<CreateOperationIntent, Account> createOperationCommand)  : base(context)
        {
            _createOperationCommand = createOperationCommand;
            _getTransactionQuery = getTransactionQuery;
            _getAccountQuery = getAccountQuery;
            _updateStatusCommand = updateStatusCommand;
        }

        public override async Task<IResult<Account>> Execute(ValidateTransactionIntent argument)
        {
            var transactionResult = await _getTransactionQuery.Fetch(new GetTransactionIntent(argument.TransactionId));
   
            if (!transactionResult.IsSuccessful)
            {
                return transactionResult.FailureDetails.ToErrorResult<Account>();
            }

            var transaction = transactionResult.Content;

            if (transaction == null)
            {
                return FailureReason.ExpectationFailed.ToErrorResult<Account>("ValidateTransactionIntent", "not found transaction");
            }

            if(transaction.TransactionStatus != Transaction.Status.Submitted &&
               transaction.TransactionStatus != Transaction.Status.Error)
            {
                return FailureReason.ExpectationFailed.ToErrorResult<Account>("ValidateTransactionIntent", $"invalid transaction status: {transaction.TransactionStatus.ToString()}");

            }

            var senderResult = await _getAccountQuery.Fetch(new GetAccountIntent(transaction.SenderKey));
            var recipientResult = await _getAccountQuery.Fetch(new GetAccountIntent(transaction.RecipientKey));

            if(! (senderResult.IsSuccessful && recipientResult.IsSuccessful))
            {
                return senderResult.FailureDetails
                    .Union(recipientResult.FailureDetails)
                    .ToErrorResult<Account>();
            }

            var updateStatusResult = await _updateStatusCommand.Execute(new UpdateTransactionStatusIntent(transaction.Id, Transaction.Status.Pending));

            if(! updateStatusResult.IsSuccessful)
            {
                return updateStatusResult.FailureDetails.ToErrorResult<Account>();
            }

            IResult<Account> accountResult = null;
            
            try
            {
                accountResult = await _createOperationCommand.Execute(new CreateOperationIntent(
                    transaction,
                    senderResult.Content,
                    recipientResult.Content));

                var status = accountResult?.IsSuccessful ?? false ?
                        Transaction.Status.Accepted :
                        Transaction.Status.Error;

                updateStatusResult = await _updateStatusCommand.Execute(new UpdateTransactionStatusIntent(
                    transaction.Id,
                    status));

                if (!updateStatusResult.IsSuccessful)
                {
                    accountResult.AddFailureDetails(updateStatusResult.FailureDetails.ToArray());
                }

                accountResult.Content.SetTransactionStatus(transaction.Id, status);
                return accountResult;
            }
            catch (Exception exception)
            {
                throw new IntentException(argument, _createOperationCommand, exception);
            }
        }
    }
}
