using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Domain
{
    public class Operation
    {
        public Operation(Guid id, string accountKey, Transaction transaction)
        {
            Id = id;
            AccountKey = accountKey;
            Transaction = transaction;
        }

        public Operation(string accountKey, Transaction transaction) : 
            this(Guid.NewGuid(), accountKey, transaction) 
        {
        }

        public Guid Id { get; private set; }
        public string AccountKey { get; private set; }
        public Transaction Transaction { get; private set; }
        public bool IsAccpeted => Transaction.TransactionStatus == Transaction.Status.Accepted;
        public bool IsDebit => Transaction.RecipientKey == AccountKey;
        public decimal Debit => IsAccpeted && IsDebit ? Transaction.Sum : 0;
        public decimal Credit => !IsAccpeted && IsDebit  ? 0 : Transaction.Sum;
        public decimal Amount => IsAccpeted ? IsDebit ? Transaction.Sum : -Transaction.Sum : 0;
    }
}
