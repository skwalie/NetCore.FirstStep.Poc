using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Arguments
{
    public class UpdateTransactionStatusArgument
    {
        public string TransactionId { get; set; }
        public Transaction.Status Status { get; set; }
    }
}
