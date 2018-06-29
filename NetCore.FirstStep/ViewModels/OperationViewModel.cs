using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.ViewModels
{
    public class OperationViewModel :
        IViewModel<GetOperationIntent>
    {
        public Guid Id { get; set; }
        public string AccountKey { get; set; }
        public IViewModel<GetTransactionIntent> Transaction { get; set; }
        public decimal Amount { get; set; }
    }
}
