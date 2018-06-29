using NetCore.FirstStep.Domain;
using System.Collections.Generic;

namespace NetCore.FirstStep.Business.Implementation
{
    public class Stub
    {
        public Stub()
        {
            var situation = new Situation(6176767600000);
            //
            Accounts.Add(new Account("BCE-0001", situation));
            Accounts.Add(new Account("BNB-0001", new Situation(167879870000)));
            Transactions.Add(new Transaction("BCE-0001", "BNB-0001", 10000000));
        }

        public List<Account> Accounts = new List<Account>();
        public List<Transaction> Transactions = new List<Transaction>();
    }
}
