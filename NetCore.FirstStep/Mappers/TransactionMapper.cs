using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using NetCore.FirstStep.ViewModels;

namespace NetCore.FirstStep.Mappers
{
    public class TransactionMapper :
        IResponseMapper<GetTransactionIntent, Transaction>,
        IResponseMapper<SubmitTransactionIntent, Transaction>
    {
        private TransactionViewModel MapInternal(Transaction transaction, IRequestContextReader context)
        {
            return new TransactionViewModel()
            {
                Id = transaction.Id,
                CreationTimestamp = transaction.CreationTimestamp,
                RecipientKey = transaction.RecipientKey,
                SenderKey = transaction.SenderKey,
                Status = transaction.TransactionStatus.ToString(),
                StatusTimestamp = transaction.StatusTimestamp,
                Sum = transaction.Sum
            };
        }

        public IViewModel<GetTransactionIntent> Map(Transaction transaction, IRequestContextReader context)
        {
            return MapInternal(transaction, context);
        }

        IViewModel<SubmitTransactionIntent> IMapper<Transaction, IRequestContextReader, IViewModel<SubmitTransactionIntent>>.Map(
            Transaction transaction,
            IRequestContextReader context)
        {
            return MapInternal(transaction, context);
        }
        
    }
}
