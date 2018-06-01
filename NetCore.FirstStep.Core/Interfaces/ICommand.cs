using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface ICommand<TInput, TOutput> : IRequest<TInput, TOutput>
    {
        Task<IResult<TOutput>> Execute(TInput argument);
    }
}
