using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public static class ResultExtensions
    {
        public static IResult<TOuput> ToErrorResult<TOuput>(this FailureReason reason, Exception exception)
        {
            return new ResultBase<TOuput>(new FailureDetail(reason, exception, exception.Message));
        }

        public static IResult<TOuput> ToErrorResult<TOuput>(this FailureReason reason, params string[] messages)
        {
            return new ResultBase<TOuput>(new FailureDetail(reason, messages));
        }

        public static IResult<TOuput> ToErrorResult<TOuput>(this IList<FailureDetail> failureDetails)
        {
            return new ResultBase<TOuput>(failureDetails.ToArray());
        }

        public static IResult<TOuput> ToResult<TOuput>(this TOuput content)
        {
            return new ResultBase<TOuput>(content);
        }
    }
}
