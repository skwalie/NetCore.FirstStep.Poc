using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;

namespace NetCore.FirstStep.Business.Commands
{
    public class SubmitTransactionIntent : ICommandIntent
    {
        public SubmitTransactionIntent(string senderKey, string recipientKey, decimal sum)
        {
            SenderKey = senderKey;
            RecipientKey = recipientKey;
            Sum = sum;
        }

        public string SenderKey { get; private set; }
        public string RecipientKey { get; private set; }
        public decimal Sum { get; private set; }
    }
}
