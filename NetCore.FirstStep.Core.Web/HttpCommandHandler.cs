using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace NetCore.FirstStep.Core
{
    public class HttpCommandHandler<TInput, TOutput> : IHttpCommandHandler<TInput, TOutput>
    {
        private readonly ICommand<TInput, TOutput> _request;

        public HttpCommandHandler(ICommand<TInput, TOutput> command)
        {
            _request = command;
        }

        public virtual async Task<IActionResult> HandleCommand(ControllerBase controller, TInput argument)
        {
            if (controller.ModelState.IsValid)
            {
                var response = await _request.Execute(argument);

                if (response.IsSuccessful)
                {
                    return controller.Ok(response.Content);
                }
                else
                {
                    return CreateErrorResponse(controller, response);
                }
            }
            else
            {
                var result = FailureReason.PreconditionFailed.ToErrorResult<TOutput>(controller.ModelState
                    .Select(x => $"{x.Key}/{x.Value}")
                    .ToArray());

                return CreateErrorResponse(controller, result);
            }
        }

        protected virtual IActionResult CreateValidationErrorResponse(ControllerBase controller, IResult result)
        {
            return controller.StatusCode(422, result);
        }

        protected virtual IActionResult CreateErrorResponse(ControllerBase controller, IResult result)
        {
            var reason = result.ExceptionDetails.Max(x => (short)x.Reason);
            return controller.StatusCode(reason, result);
        }
    }
}
