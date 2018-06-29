using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IRequestContext : IRequestContextReader
    {
        void SetValue<T>(T value);
        void SetValue<T>(string name, T value);
    }
}
