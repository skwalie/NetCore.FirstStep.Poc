using System.Collections.Generic;
using System.Linq;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Core.Commands;

namespace NetCore.FirstStep.Decorators
{
    public class DefaultExceptionHandlerQueryDecorator<TIntent, TOutput> : ExceptionHandlerQueryDecorator<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        public DefaultExceptionHandlerQueryDecorator(IQueryContext<TIntent> context, IQuery<TIntent, TOutput> query) : base(context, query)
        {
        }

        protected override IResult<TOutput> HandleFailureDetails(IEnumerable<FailureDetail> failureDetails)
        {
           ///TODO:  fallbacks, retry ?

            return failureDetails.ToErrorResult<TOutput>();

        }
    }
}
