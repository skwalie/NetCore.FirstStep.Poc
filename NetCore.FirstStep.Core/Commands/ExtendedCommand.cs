using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class ExtendedCommand<TIntent, TOutput> : Command<TIntent, TOutput> 
    {
        public ExtendedCommand(ICommandContext<TIntent> context) : base(context)
        {
        }

        public sealed override async Task<IResult<TOutput>> Execute(TIntent argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("command argument");
            }

            try
            {
                var preProcessResult = await PreProcessCommand(argument);

                if(! preProcessResult.IsSuccessful)
                {
                    return preProcessResult;
                }

                var result = await ExecuteInternal(argument);
                await PostProcessCommand(argument, ref result);
                return result;
            }
            catch (Exception exception)
            {
                return FailureReason.InternalServerError.ToErrorResult<TOutput>(exception);
            }
        }

        protected abstract Task<IResult<TOutput>> ExecuteInternal(TIntent argument);

        protected virtual Task<IResult<TOutput>> PreProcessCommand(TIntent argument)
        {
            return Task.FromResult(default(TOutput).ToResult());
        }

        protected virtual Task PostProcessCommand(TIntent argument, ref IResult<TOutput> result)
        {
            return Task.CompletedTask;
        }
    }
}
