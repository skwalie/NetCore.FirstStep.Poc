using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public class ActivityContext : RequestContext
    {
        public ActivityContext(IDictionary<object, object> contextItems) : base(contextItems)
        {
        }

        public ActivityContext(params object[] contextItems) : base(new Dictionary<object, object>())
        {
            foreach(var item in contextItems)
            {
                if(item == null)
                {
                    throw new ArgumentNullException("context item");
                }

                SetValue(nameof(item), item);
            }

        }
    }
}
