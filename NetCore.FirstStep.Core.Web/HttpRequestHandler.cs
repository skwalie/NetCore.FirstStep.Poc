using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetCore.FirstStep.Core
{
    public abstract class HttpRequestHandler<TIntent, TOutput> : IHttpRequestHandler<TIntent>
        where TIntent : IIntent
    {
        private readonly IContextHolder _contextHolder;
        private readonly IResponseMapper<TIntent, TOutput> _responseMapper;

        public HttpRequestHandler(
            IContextHolder contextHolder, 
            IResponseMapper<TIntent, TOutput> responseMapper)
        {
            _contextHolder = contextHolder;
            _responseMapper = responseMapper;
        }

        protected IContextHolder ContextHolder => _contextHolder;

        public virtual async Task<IActionResult> Handle(HttpRequest httpRequest, ModelStateDictionary modelState, IViewArgument<TIntent> viewArgument)
        {
            if (modelState.IsValid)
            {
                EnrichContext(_contextHolder.Context, httpRequest);

                var intent = viewArgument.Map(_contextHolder.Context);
                var result = await ProcessRequest(intent);

                if(result.IsSuccessful)
                {
                    var mappedViewModel = _responseMapper.Map(result.Content, _contextHolder.Context);
                    return CreateResponse(httpRequest, mappedViewModel);
                }
                return CreateErrorResponse(httpRequest, result);
            }
            else
            {
                var result = modelState.ToErrorResult<TOutput>();
                return CreateValidationErrorResponse(httpRequest, result);
            }
        }

        protected abstract Task<IResult<TOutput>> ProcessRequest(TIntent input);

        protected abstract void EnrichContext(IRequestContext requestContext, HttpRequest request);

        protected virtual IActionResult CreateResponse(HttpRequest httpRequest, IViewModel<TIntent> viewModel, bool isCreated = false)
        {
            return new ObjectResult(viewModel.ToResult())
            {
                StatusCode = isCreated ? 201 : 200
            };
        }

        protected virtual IActionResult CreateValidationErrorResponse(HttpRequest httpRequest, IResult result)
        {
            return new ObjectResult(result)
            {
                StatusCode = (int)FailureReason.PreconditionFailed
            };
        }

        protected virtual IActionResult CreateErrorResponse(HttpRequest httpRequest, IResult result)
        {
            return new ObjectResult(result)
            {
                StatusCode = result.FailureDetails.Max(x => (short)x.Reason)
            };
        }
    }
}