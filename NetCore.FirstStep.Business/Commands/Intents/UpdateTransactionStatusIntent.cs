using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Commands
{
    public class UpdateTransactionStatusIntent : ICommandIntent
    {
        public UpdateTransactionStatusIntent(string transactionId, Transaction.Status status)
        {
            TransactionId = transactionId;
            Status = status;
        }
        public string TransactionId { get; set; }
        public Transaction.Status Status { get; set; }
    }
}
