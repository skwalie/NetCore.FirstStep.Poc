using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Core
{
    public interface IGetByIdIntent<TIdentifier> : IQueryIntent
    {
        TIdentifier Key { get; }
    }
}
