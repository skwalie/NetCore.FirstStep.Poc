using System;

namespace NetCore.FirstStep.Core
{
    public interface ICacheService<TIntent, TData>
    {
        TData Create(TIntent key, TData result, TimeSpan relativeExpiration);
        TData Get(TIntent intent);
        void Invalidate(TIntent intent);
    }
}