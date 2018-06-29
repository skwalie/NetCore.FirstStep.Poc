using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetCore.FirstStep.Domain;

namespace NetCore.FirstStep.Business.Implementation
{
    public class FirstStepBusinessManager : IFirstStepBusinessManager
    {
        private static Stub _stub;

        public FirstStepBusinessManager(Stub stub)
        {
            _stub = _stub ?? stub;
        }

        public FirstStepBusinessManager() : this(new Stub())
        {
        }

        public Task<Account> CreateAccount()
        {
            var account = new Account();
            _stub.Accounts.Add(account);
            return Task.FromResult(account);
        }

        public async Task<Account> AddOperation(Operation operation)
        {
            var account = await GetAccount(operation.AccountKey);
            account.CurrentSituation.AddOperation(operation);
            
            return account;
        }

        public Task<Account> GetAccount(string key)
        {
            var account = _stub.Accounts.SingleOrDefault(acc => acc.Key == key);

#if DEBUG
            Thread.Sleep(25);
#endif

            return Task.FromResult(account);
        }

        public Task<Transaction> GetTransaction(string transactionId)
        {
            var transaction = _stub.Transactions.SingleOrDefault(t => t.Id == transactionId);

#if DEBUG
            Thread.Sleep(25);
#endif
            return Task.FromResult(transaction);
        }

        public Task<Transaction> CreateTransaction(string senderKey, string recipientKey, decimal sum)
        {
            var transaction = new Transaction(senderKey, recipientKey, sum);
            _stub.Transactions.Add(transaction);
            return Task.FromResult(transaction);
        }

        public async Task<Transaction> UpdateTransactionStatus(string transactionId, Transaction.Status status)
        {
            var transaction = await GetTransaction(transactionId);
            transaction.SetTransactionStatus(status);
            return transaction;
        }

        public Task<IEnumerable<Transaction>> GetTransactionsFromStatus(string accountKey, Transaction.Status status)
        {
            var result = _stub.Transactions
                .Where(t =>
                    t.TransactionStatus == status &&
                    (t.SenderKey == accountKey || t.RecipientKey == accountKey));

#if DEBUG
            Thread.Sleep(25);
#endif

            return Task.FromResult(result);
        }
    }
}
