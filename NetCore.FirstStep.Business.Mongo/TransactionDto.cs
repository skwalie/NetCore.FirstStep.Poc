using System;

namespace NetCore.FirstStep.Business.Mongo
{
    public class TransactionDto
    {
        public string TransactionId { get;  set; }
        public DateTime CreationTimestamp { get;  set; }
        public string SenderKey { get;  set; }
        public string RecipientKey { get;  set; }
        public decimal Sum { get;  set; }
        public string TransactionStatus { get;  set; }
        public DateTime StatusTimestamp { get;  set; }
        public Guid IntegrityCheck { get;  set; }
    }
}
