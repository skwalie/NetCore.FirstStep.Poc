using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetAccountIntent : GetFromIdentifierIntent<Account, string>
    {
        public GetAccountIntent(string key) : base(key)
        {
        }
    }
}
