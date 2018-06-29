using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core.Commands
{
    public abstract class ExceptionHandlerCommandDecorator<TIntent, TOutput> : CommandDecorator<TIntent, TOutput>
    {
        public ExceptionHandlerCommandDecorator(
            ICommandContext<TIntent> context, 
            ICommand<TIntent, TOutput> command) : base(context, command)
        {
        }

        public override async Task<IResult<TOutput>> Execute(TIntent argument)
        {
            try
            {
                var result = await base.Execute(argument);

                if(! result.IsSuccessful)
                {
                    return HandleFailureDetails(result.FailureDetails);
                }

                return result;
            }
            catch (Exception exception)
            {
                var result = FailureReason.InternalServerError.ToErrorResult<TOutput>(exception);
                return HandleFailureDetails(result.FailureDetails);
            }
        }

        protected abstract IResult<TOutput> HandleFailureDetails(IEnumerable<FailureDetail> failureDetails);
    }
}
