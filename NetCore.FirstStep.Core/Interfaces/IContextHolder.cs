namespace NetCore.FirstStep.Core
{
    public interface IContextHolder
    {
        IRequestContext Context { get; }
    }
}
