using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using NetCore.FirstStep.ViewModels;
using System.Linq;

namespace NetCore.FirstStep.Mappers
{
    public class AccountMapper : 
        IResponseMapper<GetAccountIntent, Account>,
        IResponseMapper<CreateAccountIntent, Account>,
        IResponseMapper<ValidateTransactionIntent, Account>,
        IResponseMapper<CreateOperationIntent, Account>
    {
        private readonly IResponseMapper<GetOperationIntent, Operation> _operationMapper;

        public AccountMapper(IResponseMapper<GetOperationIntent, Operation> operationMapper)
        {
            _operationMapper = operationMapper;
        }

        public IViewModel<GetAccountIntent> Map(Account account, IRequestContextReader context)
        {
            return MapInternal(account, context);
        }

        IViewModel<CreateAccountIntent> IMapper<Account, IRequestContextReader, IViewModel<CreateAccountIntent>>.Map(
            Account account, 
            IRequestContextReader context)
        {
            return MapInternal(account, context);
        }

        IViewModel<ValidateTransactionIntent> IMapper<Account, IRequestContextReader, IViewModel<ValidateTransactionIntent>>.Map(
            Account account, 
            IRequestContextReader context)
        {
            return MapInternal(account, context);
        }

        IViewModel<CreateOperationIntent> IMapper<Account, IRequestContextReader, IViewModel<CreateOperationIntent>>.Map(
            Account account, IRequestContextReader context)
        {
            return MapInternal(account, context);
        }

        private AccountViewModel MapInternal(Account account, IRequestContextReader context)
        {
            return new AccountViewModel()
            {
                CurrentAmount = account.CurrentSituation.CurrentAmount,
                InitialAmount = account.CurrentSituation.InitialAmount,
                Key = account.Key,
                StartDate = account.CurrentSituation.StartDate,
                Operations = account.CurrentSituation.Operations.Select(o => _operationMapper.Map(o, context))
            };
        }


    }
}
