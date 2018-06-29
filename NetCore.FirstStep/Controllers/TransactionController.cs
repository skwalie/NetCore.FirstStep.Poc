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
    public class TransactionController : Controller
    {
        private readonly IHttpQueryHandler<GetTransactionIntent, Transaction> _getTransactionQueryHandler;
        private readonly IHttpCommandHandler<SubmitTransactionIntent, Transaction> _submitTransactionCommandHandler;
        private readonly IHttpCommandHandler<ValidateTransactionIntent, Account> _validateTransactionCommandHandler;

        public TransactionController(
            IHttpQueryHandler<GetTransactionIntent, Transaction> getTransactionQueryHandler,
            IHttpCommandHandler<SubmitTransactionIntent, Transaction> submitTransactionCommandHandler,
            IHttpCommandHandler<ValidateTransactionIntent, Account> validateTransactionCommandHandler
            ) 
        {
            _submitTransactionCommandHandler = submitTransactionCommandHandler;
            _validateTransactionCommandHandler = validateTransactionCommandHandler;
            _getTransactionQueryHandler = getTransactionQueryHandler;
        }

        [HttpGet("{tansactionId}")]
        public async Task<IActionResult> Get(string tansactionId)
        {
            var argument = new GetTransactionViewArgument() { TransactionId = tansactionId };
            return await _getTransactionQueryHandler.Handle(Request, ModelState, argument);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SubmitTransactionViewArgument argument)
        {
            return await _submitTransactionCommandHandler.Handle(Request, ModelState, argument);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ValidateTransactionViewArgument argument)
        {
            return await _validateTransactionCommandHandler.Handle(Request, ModelState, argument);
        }
    }
}
