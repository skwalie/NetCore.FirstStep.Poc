using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public class CommandContext<TIntent> :
        RequestContext,
        ICommandContext<TIntent>
        where TIntent : ICommandIntent
    {
        public CommandContext() : this(new Dictionary<object, object>())
        {
        }

        public CommandContext(IDictionary<object, object> contextItems) : base(contextItems)
        {
        }

    }
}
