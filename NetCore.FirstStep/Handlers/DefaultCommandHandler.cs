using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Business.Queries;

namespace NetCore.FirstStep.Handlers
{ 
    public class DefaultCommandHandler<TIntent, TOutput> : HttpCommandHandler<TIntent, TOutput>
        where TIntent : ICommandIntent
    {
        public DefaultCommandHandler(
            ICommand<TIntent, TOutput> command, 
            IResponseMapper<TIntent, TOutput> responseMapper) : base(command, responseMapper)
        {
        }

        protected override void EnrichContext(IRequestContext requestContext, HttpRequest request)
        {
            requestContext.SetValue(new QueryCacheContext(true, new TimeSpan(0, 5, 0)));
        }
    }
}
