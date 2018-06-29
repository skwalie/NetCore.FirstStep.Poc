using NetCore.FirstStep.Core.Interfaces;

namespace NetCore.FirstStep.Core
{
    public interface IViewArgument<TIntent> : IMapper<IRequestContext, TIntent> 
        where TIntent : IIntent
    {
    }
}
