using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IQueryDecorator<TIntent, TOutput> : IQuery<TIntent, TOutput>
    {
    }
}