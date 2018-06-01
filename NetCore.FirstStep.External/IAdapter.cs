using NetCore.FirstStep.Core;
using System.Threading.Tasks;

namespace NetCore.FirstStep.External
{
    public interface IAdapter<TInput, TOutput>
    {
        Task<IResult<TOutput>> Adapt(TInput input);
    }
}
