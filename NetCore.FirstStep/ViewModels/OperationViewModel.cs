using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using System;

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
