using NetCore.FirstStep.Core;
using NetCore.FirstStep.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Decorators
{
    public class DefaultExceptionHandlerCommandDecorator<TIntent, TOutput> : ExceptionHandlerCommandDecorator<TIntent, TOutput>
    {
        public DefaultExceptionHandlerCommandDecorator(ICommandContext<TIntent> context, ICommand<TIntent, TOutput> command) : base(context, command)
        {
        }

        protected override IResult<TOutput> HandleFailureDetails(IEnumerable<FailureDetail> failureDetails)
        {
            // refine this
            return failureDetails.ToErrorResult<TOutput>();
        }
    }
}
