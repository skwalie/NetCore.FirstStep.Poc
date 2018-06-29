using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class ExtendedQuery<TIntent, TOutput> : Query<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        public ExtendedQuery(IQueryContext<TIntent> context) : base(context)
        {
        }

        public sealed override async Task<IResult<TOutput>> Fetch(TIntent argument)
        {
            if (argument == null)
            {
                throw  new ArgumentException("query argument is null");
            }

            var preProccessResult = await PreProcessQuery(argument);

            if(! preProccessResult.IsSuccessful)
            {
                return preProccessResult;
            }
                
            var result = await Process(argument);
            await PostProcessQuery(argument, ref result);

            return result;
        }

        internal abstract Task<IResult<TOutput>> Process(TIntent argument);

        protected virtual Task<IResult<TOutput>> PreProcessQuery(TIntent argument)
        {
            return Task.FromResult(default(TOutput).ToResult());
        }

        protected virtual Task PostProcessQuery(TIntent argument,  ref IResult<TOutput> result)
        {
            return Task.CompletedTask;
        }
    }
}
