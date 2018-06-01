using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Arguments
{
    public class SubmitTransactionArgument
    {
        public string SenderKey { get; set; }
        public string RecipientKey { get; set; }
        public decimal Sum { get; set; }
    }
}
