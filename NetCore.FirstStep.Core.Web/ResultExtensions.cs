using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public static class ResultExtensions
    {
        public static IResult<TOuput> ToErrorResult<TOuput>(this ModelStateDictionary modelState)
        {
            return new ResultBase<TOuput>(modelState
                .Select(entry => new FailureDetail(FailureReason.PreconditionFailed, new KeyValuePair<string, object>(entry.Key, entry.Value.RawValue)))
                .ToArray());
        }
    }
}
