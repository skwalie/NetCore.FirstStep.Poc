using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace NetCore.FirstStep.Core
{
    public class HttpQueryHandler<TInput, TOutput> : IHttpQueryHandler<TInput, TOutput>
    {
        private readonly IQuery<TInput, TOutput> _query;

        public HttpQueryHandler(IQuery<TInput, TOutput> query)
        {
            _query = query;
        }

        public virtual async Task<IActionResult> HandleQuery(ControllerBase controller, TInput argument)
        {
            if (controller.ModelState.IsValid)
            {
                var response = await _query.Fetch(argument);

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
                    .Select(state => $"{state.Key}/{state.Value}")
                    .ToArray());

                return CreateErrorResponse(controller, result);
            }
        }

        protected virtual IActionResult CreateErrorResponse(ControllerBase controller, IResult result)
        {
            var reason = result.ExceptionDetails.Max(x => (short)x.Reason);
            return controller.StatusCode(reason);
        }
    }
}
