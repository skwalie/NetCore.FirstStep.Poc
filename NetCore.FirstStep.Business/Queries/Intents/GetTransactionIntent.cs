using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetTransactionIntent : GetFromIdentifierIntent<Transaction, string>
    {
        public GetTransactionIntent(string key) : base(key)
        {
        }
    }
}
