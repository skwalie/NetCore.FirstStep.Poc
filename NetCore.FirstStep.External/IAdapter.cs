using NetCore.FirstStep.Core;
using System.Threading.Tasks;

namespace NetCore.FirstStep.External
{
    public interface IAdapter<TIntent, TOutput>
    {
        Task<IResult<TOutput>> Adapt(TIntent input);
    }
}
