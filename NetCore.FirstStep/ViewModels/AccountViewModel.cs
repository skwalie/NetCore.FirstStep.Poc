using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;

namespace NetCore.FirstStep.ViewModels
{
    public class AccountViewModel : 
        IViewModel<GetAccountIntent>,
        IViewModel<CreateAccountIntent>,
        IViewModel<ValidateTransactionIntent>,
        IViewModel<CreateOperationIntent>
    {
        public string Key { get; set; }
        public DateTime StartDate { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public IEnumerable<IViewModel<GetOperationIntent>> Operations { get; set; }

    }
}
