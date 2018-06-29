using System;

namespace NetCore.FirstStep.Business.Mongo
{
    public class OperationDto
    {
        public Guid OperationId { get;  set; }
        public string AccountKey { get;  set; }
        public string TransactionId { get;  set; }
    }
}
