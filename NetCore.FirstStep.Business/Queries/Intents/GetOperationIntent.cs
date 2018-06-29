using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;

namespace NetCore.FirstStep.Business.Queries
{
    public class GetOperationIntent : GetFromIdentifierIntent<Operation, Guid>
    {
        public GetOperationIntent(Guid key) : base(key)
        {
        }
    }
}
