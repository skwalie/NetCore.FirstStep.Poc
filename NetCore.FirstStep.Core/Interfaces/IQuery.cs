using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IQuery<TIntent, TOutput> : IContextHolder
    {
        Task<IResult<TOutput>> Fetch(TIntent argument); 
    }
}
