using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public class QueryCacheDecorator<TInput, TOutput> : QueryDecorator<TInput, TOutput>
        where TInput : ICacheableQueryArgument
    {
        private readonly CacheService<TInput, TOutput> _cacheService;
 
        public QueryCacheDecorator(IQuery<TInput, TOutput> query, CacheService<TInput, TOutput> cacheService) : base(query)
        {
            _cacheService = cacheService; 
        }
        
        public override async Task<IResult<TOutput>> Fetch(TInput argument)
        {
            if(_cacheService != null && argument.UseCache)
            {
                var key = _cacheService;
                var cachedObject = _cacheService.Get(argument);

                if(cachedObject == null)
                {
                    var result = await Instance.Fetch(argument);

                    if(result.IsSuccessful)
                    {
                        _cacheService.Create(argument, result.Content);
                    }
                    return new ResultBase<TOutput>((TOutput)result);
                }
                else
                {
                    return new ResultBase<TOutput>((TOutput)cachedObject);
                }
            }
            else
            {
                return await Instance.Fetch(argument);
            }
        }
        
    }
}
