using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Domain
{
    public class Account
    {
        public string Key { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastModification { get; set; }
        public Guid IntegrityCheck { get; set; }
    }
}
