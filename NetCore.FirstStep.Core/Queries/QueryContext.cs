using System.Collections.Generic;

namespace NetCore.FirstStep.Core
{
    public class QueryContext<TIntent> : 
        RequestContext,
        IQueryContext<TIntent>
        where TIntent : IQueryIntent
    {
        public QueryContext(IDictionary<object, object> contextItems) : base(contextItems)
        {
        }
     
        public QueryContext() : this(new Dictionary<object, object>())
        {
        }




    }
}
