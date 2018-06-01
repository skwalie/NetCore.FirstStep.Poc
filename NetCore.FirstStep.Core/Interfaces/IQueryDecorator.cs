using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IQueryDecorator<TInput, TOutput> : IQuery<TInput, TOutput>
    {
    }
}