using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using NetCore.FirstStep.ViewModels;

namespace NetCore.FirstStep.Mappers
{
    public class OperationMapper : 
        IResponseMapper<GetOperationIntent, Operation>
    {
        private readonly IResponseMapper<GetTransactionIntent, Transaction> _transactionMapper;

        public OperationMapper(IResponseMapper<GetTransactionIntent, Transaction> transactionMapper)
        {
            _transactionMapper = transactionMapper;
        }
        
        IViewModel<GetOperationIntent> IMapper<Operation, IRequestContextReader, IViewModel<GetOperationIntent>>.Map(
            Operation operation, 
            IRequestContextReader context)
        {
            return MapInternal(operation, context);
        }

        private OperationViewModel MapInternal(Operation operation, IRequestContextReader context)
        {
            return new OperationViewModel()
            {
                AccountKey = operation.AccountKey,
                Amount = operation.Amount,
                Id = operation.Id,
                Transaction = _transactionMapper.Map(operation.Transaction, context)
            };
        }

    }
}
