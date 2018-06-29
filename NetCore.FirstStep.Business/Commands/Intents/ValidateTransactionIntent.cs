using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Commands
{
    public class ValidateTransactionIntent : ICommandIntent
    {
        public ValidateTransactionIntent(string transactionId)
        {
            TransactionId = transactionId;
        }

        public string TransactionId { get; }
    }
}
