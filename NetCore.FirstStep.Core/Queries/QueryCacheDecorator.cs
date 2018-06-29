using NetCore.FirstStep.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public class QueryCacheDecorator<TIntent, TOutput> : QueryDecorator<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        private readonly ICacheService<TIntent, TOutput> _cacheService;

        public QueryCacheDecorator(
            IQueryContext<TIntent> context,
            IQuery<TIntent, TOutput> query,
            ICacheService<TIntent, TOutput> cacheService) : base(context, query)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException("context");
        }

        public override async Task<IResult<TOutput>> Fetch(TIntent argument)
        {
            var cacheContextItem = Context.GetValue<QueryCacheContext>();

            if(cacheContextItem != null && cacheContextItem.UseCache)
            {
                var cachedObject = _cacheService.Get(argument);

                if(cachedObject == null)
                {
                    var result = await Instance.Fetch(argument);

                    if(result.IsSuccessful)
                    {
                        _cacheService.Create(
                            argument, 
                            result.Content,
                            cacheContextItem.RelativeExpiration);
                    }
                    return result;
                }
                else
                {
                    return cachedObject.ToResult();
                }
            }
            else
            {
                return await Instance.Fetch(argument);
            }
        }
    }
}
