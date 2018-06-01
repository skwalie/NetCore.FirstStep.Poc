using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class SubmitTransactionCommand : FirstStepCommand<SubmitTransactionArgument, Transaction>
    {
        private readonly IQuery<GetAccountArgument, Account> _getAccountQuery;
        private readonly ICommand<UpdateTransactionStatusArgument, Transaction> _updateTransactionStatusQuery;

        public SubmitTransactionCommand(
            IFirstStepBusinessManager manager,
            IQuery<GetAccountArgument, Account> getAccountQuery,
            ICommand<UpdateTransactionStatusArgument, Transaction> updateTransactionStatusQuery) : base(manager)
        {
            _getAccountQuery = getAccountQuery;
            _updateTransactionStatusQuery = updateTransactionStatusQuery;
        }

        protected override async Task<IResult<Transaction>> PreProcessCommand(SubmitTransactionArgument argument)
        {
            var sender = await _getAccountQuery.Fetch(new GetAccountArgument() { Key = argument.SenderKey });
   
            if(!sender.IsSuccessful)
            {
                return sender.ExceptionDetails.ToErrorResult<Transaction>();
            }

            if(sender.Content == null)
            {
                return FailureReason.BadRequest.ToErrorResult<Transaction>("invalid sender key");
            }

            var recipient = await _getAccountQuery.Fetch(new GetAccountArgument() { Key = argument.RecipientKey });

            if (! recipient.IsSuccessful)
            {
                return sender.ExceptionDetails.ToErrorResult<Transaction>();
            }

            if (recipient.Content == null)
            {
                return FailureReason.BadRequest.ToErrorResult<Transaction>("invalid recipient key");
            }

            return default(Transaction).ToResult();
        }

        protected override async Task<IResult<Transaction>> ProcessCommand(SubmitTransactionArgument argument)
        {
            var transaction = await BusinessManager.SubmitTransaction(
                argument.SenderKey,
                argument.RecipientKey,
                argument.Sum);

            return transaction.ToResult();
        }
    }
}
