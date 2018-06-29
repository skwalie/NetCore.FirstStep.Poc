using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class Query<TIntent, TOutput> : 
        IQuery<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        public Query(IQueryContext<TIntent> context)
        {
            Context = context;
        }

        public virtual IRequestContext Context { get; }
        public abstract Task<IResult<TOutput>> Fetch(TIntent argument);
    }
}
