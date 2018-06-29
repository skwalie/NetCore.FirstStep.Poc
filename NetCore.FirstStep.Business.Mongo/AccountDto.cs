using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.FirstStep.Business.Mongo
{
    public class AccountDto
    {
        public string Key { get; set; }
        public SituationDto CurrentSituation { get; set; }
    
    }
}
