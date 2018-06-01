using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business
{
    public interface IFirstStepBusinessManager : IFirstStepReadManager
    {
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<Account> CreateAccount();
        Task<Transaction> UpdateTransactionStatus(string transactionId, Transaction.Status status);
        Task<Transaction> SubmitTransaction(string senderKey, string recipientKey, decimal sum);
    }
}
