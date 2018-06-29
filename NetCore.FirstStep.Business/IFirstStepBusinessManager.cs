using NetCore.FirstStep.Domain;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business
{
    public interface IFirstStepBusinessManager : IFirstStepReadManager
    {
        Task<Account> CreateAccount();
        Task<Transaction> CreateTransaction(string senderKey, string recipientKey, decimal sum);
        Task<Transaction> UpdateTransactionStatus(string transactionId, Transaction.Status status);
        Task<Account> AddOperation(Operation operation);
    }
}
