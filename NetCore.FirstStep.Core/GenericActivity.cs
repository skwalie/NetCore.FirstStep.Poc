using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public class GenericActivity<TIntent, TOutput> : Activity<TIntent, TOutput>,
        IAsyncActivity<TIntent, TOutput>
        where TIntent : IIntent
    {
        private readonly Func<TIntent, TOutput> _funcToRun;

        public GenericActivity(IRequestContext context, Func<TIntent, TOutput> funcToRun) : base(context)
        {
            _funcToRun = funcToRun;
        }

        protected override IActivity<TIntent, TOutput> RunInternal(TIntent intent)
        {
            try
            {
                if (intent == null) throw new ArgumentNullException("intent");

                var returnValue = _funcToRun(intent);
                Result = returnValue.ToResult();
            }
            catch (Exception exc)
            {
                throw new IntentException(intent, this, exc);
            }

            return this;
        }

        protected override async Task RunInternalAsync(TIntent intent)
        {
            try
            {
                if (intent == null) throw new ArgumentNullException("intent");

                var returnValue = await Task.Run(() => _funcToRun(intent));
                Result = returnValue.ToResult();
            }
            catch (Exception exc)
            {
                throw new IntentException(intent, this, exc);
            }
        }
    }
}
