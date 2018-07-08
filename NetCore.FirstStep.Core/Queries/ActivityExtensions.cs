using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public static class ActivityExtensions
    {
        public static IActivity<TIntent, TOutput> Then<TIntent, TOutput>(this IActivity<TIntent, TOutput> currentActivity, IActivity<TIntent, TOutput> nextActivity)
        {
            currentActivity.Context.CopyTo(nextActivity);
            return nextActivity.Run(currentActivity.Intent);
        }

        public static IActivity<TIntent, TOutput> Then<TIntent, TOutput>(this IActivity<TIntent, TOutput> currentActivity, Func<TOutput, Func<TIntent, TOutput>> nextFunction)
            where TIntent : IIntent
        {
            var next = Activity<TIntent, TOutput>.Create(nextFunction(currentActivity.Result.Content));
            currentActivity.Context.CopyTo(next);
            return next.Run(currentActivity.Intent);
        }
    }
}
