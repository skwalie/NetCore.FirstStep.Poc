using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business
{
    public interface IFirstStepReadManager
    {
        Task<Account> GetAccount(string key);
        Task<Transaction> GetTransaction(string transactionId);

    }
}
