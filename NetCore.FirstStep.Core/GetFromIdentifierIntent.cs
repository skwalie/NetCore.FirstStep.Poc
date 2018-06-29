namespace NetCore.FirstStep.Core
{
    public abstract class GetFromIdentifierIntent<TDomain, TIdentifer> : IGetByIdIntent<TIdentifer>
    {
        private readonly TIdentifer _key;

        public GetFromIdentifierIntent(TIdentifer key)
        {
            _key = key;
        }

        public TIdentifer Key => _key;
    }
}
