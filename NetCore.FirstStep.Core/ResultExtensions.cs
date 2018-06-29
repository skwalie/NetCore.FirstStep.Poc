using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public static class ResultExtensions
    {
        public static IResult<TOuput> ToErrorResult<TOuput>(this FailureReason reason, string code, string message)
        {
            return new ResultBase<TOuput>(new FailureDetail(reason, new KeyValuePair<string, object>(code, message)));
        }

        public static IResult<TOuput> ToResult<TOuput>(this TOuput content)
        {
            return new ResultBase<TOuput>(content);
        }

        public static IResult<TOuput> ToErrorResult<TOuput>(this FailureReason reason, Exception exception)
        {
            return new ResultBase<TOuput>(new FailureDetail(reason, exception, new KeyValuePair<string, object>("exception", exception.Message)));
        }

        public static IResult<TOuput> ToErrorResult<TOuput>(this FailureReason reason, params KeyValuePair<string, object>[] messages)
        {
            return new ResultBase<TOuput>(new FailureDetail(reason, messages));
        }

        public static IResult<TOuput> ToErrorResult<TOuput>(this IEnumerable<FailureDetail> failureDetails)
        {
            return new ResultBase<TOuput>(failureDetails.ToArray());
        }

        public static IResult<TOutput> MapFailures<TInput, TOutput>(this IResult<TInput> source, IResult<TOutput> destination)
        {
            if (!source.IsSuccessful)
            {
                destination = source.FailureDetails.ToErrorResult<TOutput>();
            }
            return destination;
        }


    }
}
