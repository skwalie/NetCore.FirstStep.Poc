using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IHttpCommandHandler<TIntent, TOutput> : IHttpRequestHandler<TIntent>
        where TIntent : ICommandIntent
    {

    }
}
