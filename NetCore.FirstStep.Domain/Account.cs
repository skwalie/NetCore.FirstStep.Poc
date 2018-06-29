using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.FirstStep.Domain
{
    public class Account
    {
        public Account(string key, Situation situation)
        {
            Key = key;
            CurrentSituation = situation;
        }

        public Account(string key) : this(key, new Situation())
        {
        }

        public Account() : this(Guid.NewGuid().ToString())
        {
        }

        public string Key { get; private set; }
        public Situation CurrentSituation { get; private set; }

        public void AddOperation(Operation operation)
        {
            CurrentSituation.AddOperation(operation);
        }

        public void SetTransactionStatus(string transactionId, Transaction.Status status)
        {
            var transaction = CurrentSituation.Operations
                .SingleOrDefault(op => op.Transaction.Id == transactionId)?
                .Transaction;

            if(transaction != null)
            {
                transaction.SetTransactionStatus(status);
            }
        }

    }
}
