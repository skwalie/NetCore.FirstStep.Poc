using System;

namespace NetCore.FirstStep.Core
{
    public interface ICommandContext<TIntent> : IRequestContext
    {
    }
}
