using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCore.FirstStep.Core.Web
{
    public abstract class HttpActivityHandler<TInput, TOutput> : HttpRequestHandler<TInput, TOutput> 
        where TInput : IIntent
    {
        public HttpActivityHandler(IAsyncActivity<TInput, TOutput> contextHolder, IResponseMapper<TInput, TOutput> responseMapper) : base(contextHolder, responseMapper)
        {
        }

        protected IAsyncActivity<TInput, TOutput> Activity => (IAsyncActivity<TInput, TOutput>)ContextHolder;

        protected override async Task<IResult<TOutput>> ProcessRequest(TInput input)
        {
            try
            {
                var activity = await Activity.RunAsync(input);

                if (activity == null)
                {
                    throw new IntentException(input, Activity, new NullReferenceException("activity is null"));
                }

                if (activity.Result == null)
                {
                    throw new IntentException(input, Activity, new NullReferenceException("result is null"));
                }

                if(! activity.Result.IsSuccessful)
                {
                    if(activity.Result.FailureDetails.Any(failure => failure.Exception is IntentException))
                    {
                        var intentExceptions = activity.Result.FailureDetails.OfType<IntentException>();

                        var results = intentExceptions.Select(exc => HandleIntentException(exc));

                        return results.Any(result => result.IsSuccessful) ?
                            activity.Result :
                            results.SelectMany(result => result.FailureDetails).ToErrorResult<TOutput>();
                    }
                    else
                    {
                        HandleFailureDetails(activity.Result.FailureDetails);
                    }
                }

                return activity.Result;

            }
            catch(Exception exc)
            {
                if(exc is IntentException)
                {
                    return HandleIntentException((IntentException)exc);
                }
                return HandleOtherException(exc);
            }
        }

        protected abstract IResult<TOutput> HandleIntentException(IntentException innerExeption);
        protected abstract IResult<TOutput> HandleFailureDetails(IEnumerable<FailureDetail> failures);
        protected abstract IResult<TOutput> HandleOtherException(Exception exc);
    }
}
