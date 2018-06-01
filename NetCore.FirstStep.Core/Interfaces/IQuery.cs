using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IQuery<TInput, TOutput> : IRequest<TInput, TOutput>
    {
        Task<IResult<TOutput>> Fetch(TInput argument);
    }
}
