using System;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface ICommand<TIntent, TOutput> : IContextHolder
    {
        Task<IResult<TOutput>> Execute(TIntent input);
    }
}
