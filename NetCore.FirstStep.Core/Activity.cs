using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class Activity<TIntent, TOutput> : 
        IContextHolder, 
        IActivity<TIntent, TOutput>,
        IAsyncActivity<TIntent, TOutput>
        where TIntent : IIntent
    {
        protected Activity(IRequestContext context)
        {
            Context = context;
        }
        
        public IRequestContext Context { get; }
        public TIntent Intent { get; private set; }
        public IResult<TOutput> Result { get; protected set; }
 
        public virtual IActivity<TIntent, TOutput> Run(TIntent intent)
        {
            RunInternal(intent);
            Intent = intent;
            return this;
        }

        public virtual async Task<IAsyncActivity<TIntent, TOutput>> RunAsync(TIntent intent)
        {
            await RunInternalAsync(intent);
            Intent = intent;
            return this;
        }

        public static Activity<TIntent, TOutput> Create(Func<TIntent, TOutput> function)
        {
            return new GenericActivity<TIntent, TOutput>(
                new ActivityContext(new Dictionary<object, object>()), 
                    function);
        }

        protected abstract Task RunInternalAsync(TIntent intent);
        protected abstract IActivity<TIntent, TOutput> RunInternal(TIntent intent);
    }
}
