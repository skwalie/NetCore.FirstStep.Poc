using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IAsyncActivity<TIntent, TOutput> : IContextHolder
    {
        IResult<TOutput> Result { get; }
        Task<IAsyncActivity<TIntent, TOutput>> RunAsync(TIntent intent);
    }
}