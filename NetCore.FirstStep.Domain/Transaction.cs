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
            InvalidAccount
        }

        public DateTime CreationTimestamp { get; set; }
        public string Id { get; set; }
        public string SenderKey { get; set; }
        public string RecipientKey { get; set; }
        public decimal Sum { get; set; }
        public DateTime? ExecutionTimestamp { get; set; }
        public Status TransactionStatus { get; set; }
    }
}
