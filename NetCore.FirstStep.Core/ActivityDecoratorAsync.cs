using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class ActivityDecoratorAsync<TIntent, TOutput> : ActivityAsync<TIntent, TOutput>
        where TIntent : IIntent
    {
        private readonly IAsyncActivity<TIntent, TOutput> _activity;

        protected ActivityDecoratorAsync(IAsyncActivity<TIntent, TOutput> activity) : base(activity.Context)
        {
            _activity = activity;
        }

        protected IAsyncActivity<TIntent, TOutput> Internal => _activity;

        public override Task<IAsyncActivity<TIntent, TOutput>> RunAsync(TIntent intent)
        {
            return Internal.RunAsync(intent);
        }
    
        protected override Task RunInternalAsync(TIntent intent)
        {
            throw new NotImplementedException();
        }
    }
}
