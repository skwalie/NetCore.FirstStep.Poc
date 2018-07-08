using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class ActivityAsync<TIntent, TOutput> : 
        IContextHolder, 
        IAsyncActivity<TIntent, TOutput>
        where TIntent : IIntent
    {
        protected ActivityAsync(IRequestContext context)
        {
            Context = context;
        }
        
        public IRequestContext Context { get; }
        public TIntent Intent { get; private set; }
        public IResult<TOutput> Result { get; protected set; }
 
        public virtual async Task<IAsyncActivity<TIntent, TOutput>> RunAsync(TIntent intent)
        {
            await RunInternalAsync(intent);
            Intent = intent;
            return this;
        }

        public static IActivity<TIntent, TOutput> Create(Func<TIntent, TOutput> function)
        {
            return new GenericActivity<TIntent, TOutput>(
                new ActivityContext(new Dictionary<object, object>()),
                    function);
        }

        public static IAsyncActivity<TIntent, TOutput> CreateAsync(Func<TIntent, TOutput> function)
        {
            return new GenericActivity<TIntent, TOutput>(
                new ActivityContext(new Dictionary<object, object>()), 
                    function);
        }

        protected abstract Task RunInternalAsync(TIntent intent);
        protected abstract IActivity<TIntent, TOutput> RunInternal(TIntent intent);
    }
}
