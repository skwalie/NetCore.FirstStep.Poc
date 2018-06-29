using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCore.FirstStep.Business.Mongo
{
    public class SituationDto
    {
        public SituationDto PreviousSituation { get;  set; }
        public DateTime StartDate  { get;  set; }
        public decimal InitialAmount { get;  set; }
        public IEnumerable<OperationDto> Operations { get; set; }
    }
}