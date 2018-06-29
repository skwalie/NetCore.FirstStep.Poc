using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core.Commands
{
    public abstract class ExceptionHandlerQueryDecorator<TIntent, TOutput> : QueryDecorator<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        public ExceptionHandlerQueryDecorator(IQueryContext<TIntent> context, IQuery<TIntent, TOutput> query) : base(context, query)
        {
        }

        public override async Task<IResult<TOutput>> Fetch(TIntent argument)
        {
            try
            {
                var result = await base.Fetch(argument);

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
