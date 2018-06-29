using System.Threading.Tasks;

namespace NetCore.FirstStep.External
{
    public interface IFacade<TIntent, TOutput>
    {
        Task<TOutput> Do(TIntent argument);
    }
}
