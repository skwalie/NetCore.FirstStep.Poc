using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IHttpQueryHandler<TIntent, TOutput> : IHttpRequestHandler<TIntent>
        where TIntent : IQueryIntent
    {
    }
}
