using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Queries
{
    public abstract class FirstStepQuery<TIntent, TOutput> : Query<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        private readonly IFirstStepReadManager _businessManager;

        public FirstStepQuery(IQueryContext<TIntent> context, IFirstStepReadManager manager) : base(context)
        {
            _businessManager = manager;
        }
      
        public IFirstStepReadManager BusinessManager => _businessManager;
    }
}
