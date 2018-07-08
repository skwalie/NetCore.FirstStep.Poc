using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    //TODO: rename it !
    public interface IRequestContext : IRequestContextReader
    {
        void SetValue<T>(T value);
        void SetValue<T>(string name, T value);
    }
}
