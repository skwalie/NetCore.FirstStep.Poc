using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IRequestContextReader
    {
        T GetValue<T>();
        T GetValue<T>(string name);

        void CopyTo(IContextHolder desinatation);
    }
}
