using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Adapters
{
    public class Stub
    {
        public static List<Account> Accounts = new List<Account>(new[]
         {
           new Account() { Key = "A", Amount = 12000 },
           new Account() { Key = "B", Amount = 250 },
           new Account() { Key = "C", Amount = 100 }
        });
        
        
        public static List<Transaction> Transactions = new List<Transaction>(new[]
            {
                new Transaction() { Id = "id1", RecipientKey = "A", SenderKey = "B", Sum = 1000, ExecutionTimestamp = DateTime.UtcNow.AddDays(-2) },
                new Transaction() { Id = "id2", RecipientKey = "B", SenderKey = "C", Sum = 50, ExecutionTimestamp = DateTime.UtcNow.AddDays(-1) },
                new Transaction() { Id = "id3", RecipientKey = "C", SenderKey = "A", Sum = 30, ExecutionTimestamp = DateTime.UtcNow.AddMinutes(-5) }
            });
    }
}
