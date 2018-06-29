using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.ViewArguments
{
    public class ValidateTransactionViewArgument : IViewArgument<ValidateTransactionIntent>
    {
        public string TransactionId { get; set; }

        public ValidateTransactionIntent Map(IRequestContext input)
        {
            return new ValidateTransactionIntent(TransactionId);
        }
    }
}
