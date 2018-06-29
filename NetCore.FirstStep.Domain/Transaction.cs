using System;

namespace NetCore.FirstStep.Domain
{
    public class Transaction
    {
        public enum Status
        {
            Initiated,
            Submitted,
            Pending,
            Accepted,
            Rejected,
            Error
        }

        public Transaction(string id, string senderyKey, string recipientKey, decimal sum, DateTime creationDate)
        {
            Id = id;
            Sum = Math.Abs(sum);
            CreationTimestamp = StatusTimestamp = creationDate;
            SenderKey = senderyKey;
            RecipientKey = recipientKey;
            TransactionStatus = Status.Initiated;
            IntegrityCheck = Guid.NewGuid();
        }

        public Transaction(string senderyKey, string recipientKey, decimal sum) : this(Guid.NewGuid().ToString(), senderyKey, recipientKey, sum, DateTime.UtcNow)
        {
        }

        public string Id { get; private set; }
        public DateTime CreationTimestamp { get; private set; }
        public string SenderKey { get; private set; }
        public string RecipientKey { get; private set; }
        public decimal Sum { get; private set; }

        public Status TransactionStatus { get; private set; }
        public DateTime StatusTimestamp { get; private set; }
        public Guid IntegrityCheck { get; private set; }

        public void SetTransactionStatus(Status status)
        {
            TransactionStatus = status;
            StatusTimestamp = DateTime.UtcNow;
            IntegrityCheck = Guid.NewGuid();
        }
    }
}
