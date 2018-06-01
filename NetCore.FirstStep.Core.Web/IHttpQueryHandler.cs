using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IHttpQueryHandler<TInput, TOutput>
    {
        Task<IActionResult> HandleQuery(ControllerBase controller, TInput argument);
    }
}
