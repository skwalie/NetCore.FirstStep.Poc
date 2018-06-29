using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using System;


namespace NetCore.FirstStep.ViewModels
{
    public class TransactionViewModel : 
        IViewModel<GetTransactionIntent>,
        IViewModel<SubmitTransactionIntent>,
        IViewModel<UpdateTransactionStatusIntent>
    {
        public string Id { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public string SenderKey { get; set; }
        public string RecipientKey { get; set; }
        public decimal Sum { get; set; }

        public DateTime? StatusTimestamp { get; set; }
        public string Status { get; set; }
    }
}
