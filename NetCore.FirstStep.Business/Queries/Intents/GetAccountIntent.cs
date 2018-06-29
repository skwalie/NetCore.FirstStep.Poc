using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetAccountIntent : GetFromIdentifierIntent<Account, string>
    {
        public GetAccountIntent(string key) : base(key)
        {
        }
    }
}
