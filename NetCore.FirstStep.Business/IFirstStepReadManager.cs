using NetCore.FirstStep.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business
{
    public interface IFirstStepReadManager
    {
        Task<Account> GetAccount(string accountKey);
        Task<Transaction> GetTransaction(string transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsFromStatus(string accountKey, Transaction.Status status);
    }
}
