using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core.Interfaces
{
    public interface ICrudService<TDomain, TId>
    {
        Task<TDomain> Create(TDomain domain);
        Task<TDomain> Read(TId domain);
        Task<TDomain> Update(TDomain domain);
        Task Delete(TDomain domain);
    }
}
