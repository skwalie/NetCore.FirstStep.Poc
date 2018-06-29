using System;

namespace NetCore.FirstStep.Business.Mongo
{
    public class AccountDto
    {
        public string Key { get; set; }
        public SituationDto CurrentSituation { get; set; }
    }
}
