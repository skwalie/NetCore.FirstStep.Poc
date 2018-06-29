using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.ViewArguments
{
    public class GetAccountViewArgument : IViewArgument<GetAccountIntent>
    {
        public string Key { get; set; }

        public GetAccountIntent Map(IRequestContext input)
        {
            return new GetAccountIntent(Key);
        }
    }
}
