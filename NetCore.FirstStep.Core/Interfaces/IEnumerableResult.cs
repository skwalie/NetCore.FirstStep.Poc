namespace NetCore.FirstStep.Core.Interfaces
{
    public interface IEnumerableResult<T> : IResult<T>
    {
        int MatchCount { get; }
    }
}
