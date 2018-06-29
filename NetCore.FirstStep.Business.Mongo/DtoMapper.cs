using System;
using System.Linq;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;

namespace NetCore.FirstStep.Business.Mongo
{
    public class DtoMapper : 
        IMapper<Account, AccountDto>,
        IMapper<AccountDto, Account>,
        IMapper<Transaction, TransactionDto>,
        IMapper<TransactionDto, Transaction>,
        IMapper<Operation, OperationDto>
    {
        public OperationDto Map(Operation operation)
        {
            if (operation == null) return null;

            return new OperationDto()
            {
                AccountKey = operation.AccountKey,
                OperationId = operation.Id,
                TransactionId = operation.Transaction.Id,
            };
        }
        
        public Operation Map(OperationDto operation, string accountKey, Transaction transaction)
        {
            return operation == null  ? null : new Operation(operation.OperationId, accountKey, transaction);
        }

        public AccountDto Map(Account account)
        {
            if (account == null) return null;

            return new AccountDto()
            {
                Key = account.Key,
                CurrentSituation = Map(account.CurrentSituation)
            };
        }

        public SituationDto Map(Situation currentSituation)
        {
            if (currentSituation == null) return null;

            return new SituationDto()
            {
                StartDate = currentSituation.StartDate,
                InitialAmount = currentSituation.InitialAmount,
                Operations = currentSituation.Operations.Select(o => Map(o)),
                PreviousSituation = Map(currentSituation.PreviousSituation)
            };
        }

        public Account Map(AccountDto input)
        {
            if (input == null) return null;

            return new Account(input.Key, Map(input.CurrentSituation));
        }

        private Situation Map(SituationDto currentSituation)
        {
            return new Situation(new Operation[] { }, currentSituation.InitialAmount, currentSituation.StartDate);
        }

        public TransactionDto Map(Transaction transaction)
        {
            if (transaction == null) return null;

            return new TransactionDto()
            {
                CreationTimestamp = transaction.CreationTimestamp,
                IntegrityCheck = transaction.IntegrityCheck,
                TransactionId = transaction.Id,
                RecipientKey = transaction.RecipientKey,
                SenderKey = transaction.SenderKey,
                StatusTimestamp = transaction.StatusTimestamp,
                Sum = transaction.Sum,
                TransactionStatus = transaction.TransactionStatus.ToString()
            };
        }

        public Transaction Map(TransactionDto input)
        {
            if (input == null) return null;

            var transaction = new Transaction(
                input.TransactionId, 
                input.SenderKey,
                input.RecipientKey,
                input.Sum,
                input.CreationTimestamp);

            transaction.SetTransactionStatus(Enum.Parse<Transaction.Status>(input.TransactionStatus));

            return transaction;
        }

        private Situation Map(SituationDto situation, Operation operation)
        {
            return new Situation(
                new Operation[] { },
                situation.InitialAmount,
                situation.StartDate
                );
        }

    }
}
