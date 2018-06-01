using System;

namespace NetCore.FirstStep.Core
{
    public interface ICacheService<TQueryArgument, TData>
        where TQueryArgument : ICacheableQueryArgument
    {
        TData Create(TQueryArgument argument, TData result);
        TData Get(TQueryArgument argument);
        void Update(TQueryArgument argument, TData result);
        void Delete(TQueryArgument argument);
    }
}