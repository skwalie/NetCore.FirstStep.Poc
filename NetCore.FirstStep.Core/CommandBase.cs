using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class CommandBase<TInput, TOutput> : ICommand<TInput, TOutput>
    {
        public virtual async Task<IResult<TOutput>> Execute(TInput argument)
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

                var result = await ProcessCommand(argument);

                await PostProcessCommand(argument, ref result);

                return result;
            }
            catch (Exception exception)
            {
                return FailureReason.InternalServerError.ToErrorResult<TOutput>(exception);
            }
        }

        protected virtual Task<IResult<TOutput>> PreProcessCommand(TInput argument)
        {
            return Task.FromResult(default(TOutput).ToResult());
        }

        protected abstract Task<IResult<TOutput>> ProcessCommand(TInput argument);

        protected virtual Task PostProcessCommand(TInput argument, ref IResult<TOutput> result)
        {
            return Task.CompletedTask;
        }
    }
}
