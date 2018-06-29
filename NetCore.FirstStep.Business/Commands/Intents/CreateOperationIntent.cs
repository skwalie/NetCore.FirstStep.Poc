using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Commands
{
    public class CreateOperationIntent : ICommandIntent
    {
        public CreateOperationIntent(Transaction transaction, 
            Account sender,
            Account recipient)
        {
            Transaction = transaction;
            Sender = sender;
            Recipient = recipient;
        }

        public Transaction Transaction { get; private set; }
        public Account Sender { get; private set; }
        public Account Recipient { get; private set; }
    }
}
