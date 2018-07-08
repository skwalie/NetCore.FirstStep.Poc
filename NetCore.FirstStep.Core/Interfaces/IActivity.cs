using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IActivity<TIntent, TOutput> : IContextHolder
    {
        TIntent Intent { get; }
        IResult<TOutput> Result { get; }
        IActivity<TIntent, TOutput> Run(TIntent intent);
    }
}