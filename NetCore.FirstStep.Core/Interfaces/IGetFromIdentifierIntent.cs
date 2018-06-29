namespace NetCore.FirstStep.Core
{
    public interface IGetByIdIntent<TIdentifier> : IQueryIntent
    {
        TIdentifier Key { get; }
    }
}
