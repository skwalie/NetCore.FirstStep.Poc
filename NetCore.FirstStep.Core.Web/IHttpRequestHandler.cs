using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IHttpRequestHandler<TIntent>
        where TIntent : IIntent
    {
        Task<IActionResult> Handle(HttpRequest httpContext, ModelStateDictionary modelState, IViewArgument<TIntent> argument);
    }
}
