using NetCore.FirstStep.Core;
using NetCore.FirstStep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public class CreateAccountCommand : FirstStepCommand<CreateAccountIntent, Account>
    {
        public CreateAccountCommand(
            ICommandContext<CreateAccountIntent> context, 
            IFirstStepBusinessManager manager) : base(context, manager)
        {
        }

        public override async Task<IResult<Account>> Execute(CreateAccountIntent argument)
        {
            return await HandleResult(argument, () => BusinessManager.CreateAccount());
        }
    }
}
