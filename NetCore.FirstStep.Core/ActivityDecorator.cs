using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class ActivityDecorator<TIntent, TOutput> : Activity<TIntent, TOutput>
        where TIntent : IIntent
    {
        private readonly IActivity<TIntent, TOutput> _activity;

        protected ActivityDecorator(IActivity<TIntent, TOutput> activity) : base(activity.Context)
        {
            _activity = activity;
        }

        protected IActivity<TIntent, TOutput> Internal => _activity;

        public override IActivity<TIntent, TOutput> Run(TIntent intent)
        {
            return Internal.Run(intent);
        }

        protected override IActivity<TIntent, TOutput> RunInternal(TIntent intent)
        {
            throw new NotImplementedException();
        }
    }
}
