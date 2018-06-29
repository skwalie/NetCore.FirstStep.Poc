using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using NetCore.FirstStep.ViewArguments;

namespace NetCore.FirstStep.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IHttpQueryHandler<GetAccountIntent, Account> _getAccountQueryHandler;
        private readonly IHttpCommandHandler<CreateAccountIntent, Account> _createAccountCommandHandler;

        public AccountController(
            IHttpQueryHandler<GetAccountIntent, Account> getAccountQueryHandler,
            IHttpCommandHandler<CreateAccountIntent, Account> createAccountCommandHandler)
        {
            _getAccountQueryHandler = getAccountQueryHandler;
            _createAccountCommandHandler = createAccountCommandHandler;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(GetAccountViewArgument argument)
        {
            return await _getAccountQueryHandler.Handle(Request, ModelState, argument);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewArgument argument)
        {
            return  await _createAccountCommandHandler.Handle(Request, ModelState, argument);
        }
    }
}
