namespace NetCore.FirstStep.Core
{
    public interface ICacheKeyBuilder<TIntent>
    {
        CacheKey GetCacheKey(TIntent intent);
    }
}
