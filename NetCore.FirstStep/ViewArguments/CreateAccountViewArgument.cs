using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.ViewArguments
{
    public class CreateAccountViewArgument : IViewArgument<CreateAccountIntent>
    {
        public CreateAccountIntent Map(IRequestContext input)
        {
            return new CreateAccountIntent();
        }
    }
}
