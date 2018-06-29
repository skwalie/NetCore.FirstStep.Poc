using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCore.FirstStep.Domain
{
    public class Situation
    {
        private readonly IList<Operation> _operations;

        public Situation() : this(new Operation[] { }, 0m, DateTime.UtcNow)
        {
        }

        public Situation(decimal initialSum) : this(new Operation[] { }, initialSum, DateTime.UtcNow)
        {
        }

        public Situation(IEnumerable<Operation> operations, decimal initialAmout, DateTime startDate) : base()
        {
            StartDate = startDate;
            InitialAmount = initialAmout;
            _operations = operations.ToList();
        }

        public Situation(Situation previousSituation) : this(
            new Operation[] { }, 
            previousSituation.CurrentAmount, 
            DateTime.Now)
        {
            PreviousSituation = previousSituation;
        }

        public Situation PreviousSituation { get; private set; }
        public DateTime StartDate  { get; private set; }
        public decimal InitialAmount { get; private set; }

        public IEnumerable<Operation> Operations => _operations;
        public decimal CurrentAmount => InitialAmount + Operations.Sum(op => op.Amount);

        public void AddOperation(Operation operation)
        {
            _operations.Add(operation);
        }
    }
}