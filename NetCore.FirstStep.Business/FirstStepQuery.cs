using NetCore.FirstStep.Business.Arguments;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Queries
{
    public abstract class FirstStepQuery<TInput, TOutput> : QueryBase<TInput, TOutput>
    {
        private readonly IFirstStepReadManager _businessManager;

        public FirstStepQuery(IFirstStepReadManager manager) : base()
        {
            _businessManager = manager;
        }

        public IFirstStepReadManager BusinessManager => _businessManager;
    }
}
