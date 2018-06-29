using Microsoft.AspNetCore.Http;
using NetCore.FirstStep.Core;
using System;

namespace NetCore.FirstStep.Handlers
{
    public class DefaultQueryHandler<TIntent, TOutput> : HttpQueryHandler<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        public DefaultQueryHandler(
            IQuery<TIntent, TOutput> query, 
            IResponseMapper<TIntent, TOutput> responseMapper) : base(query, responseMapper)
        {
        }

        protected override void EnrichContext(IRequestContext requestContext, HttpRequest request)
        {
            requestContext.SetValue(request.Headers);
            requestContext.SetValue(request.HttpContext.User);
            requestContext.SetValue(new QueryCacheContext(true, new TimeSpan(0, 5, 0)));
        }

    }
}
