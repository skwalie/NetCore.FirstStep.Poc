using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business
{
    public interface ITransactionCustomerService
    {
        Task<Account> CreateAccount();
        Task<Account> GetAccount(string key);

        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<Transaction> GetTransaction(string transactionId);
        Task<Transaction> UpdateTransactionStatus(string transactionId, Transaction.Status status);
        Task<Transaction> ExecuteTransaction(string transactionId);
    }
}