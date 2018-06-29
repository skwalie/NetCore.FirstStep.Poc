using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IViewModel<in TIntent> where TIntent : IIntent
    {
        
    }
}
