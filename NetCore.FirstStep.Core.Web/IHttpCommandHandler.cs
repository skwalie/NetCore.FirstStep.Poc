using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public interface IHttpCommandHandler<TInput, TOutput> 
    {
        Task<IActionResult> HandleCommand(ControllerBase controller, TInput argument);
    }
}
