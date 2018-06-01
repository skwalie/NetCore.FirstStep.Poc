using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class QueryBase<TInput, TOutput> : 
        IQuery<TInput, TOutput>
    {
        public virtual async Task<IResult<TOutput>> Fetch(TInput argument)
        {
            if (argument == null)
            {
                throw  new ArgumentException("query argument");
            }

            try
            {
                var preProccessResult = await PreProcessQuery(argument);

                if(! preProccessResult.IsSuccessful)
                {
                    return preProccessResult;
                }
                
                var result = await ProcessQuery(argument);

                await PostProcessQuery(argument, ref result);

                return result;
            }
            catch (Exception exception)
            {
                return FailureReason.InternalServerError.ToErrorResult<TOutput>(exception);
            }
        }
  
        protected virtual Task<IResult<TOutput>> PreProcessQuery(TInput argument)
        {
            return Task.FromResult(default(TOutput).ToResult());
        }

        protected abstract Task<IResult<TOutput>> ProcessQuery(TInput argument);

        protected virtual Task PostProcessQuery(TInput argument, ref IResult<TOutput> result)
        {
            return Task.CompletedTask;
        }
    }
}
