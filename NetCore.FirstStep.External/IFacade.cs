using System.Threading.Tasks;

namespace NetCore.FirstStep.External
{
    public interface IFacade<TInput, TOutput>
    {
        Task<TOutput> Do(TInput argument);
    }
}
