using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class Command<TIntent, TOutput> : ICommand<TIntent, TOutput>
    {
        private readonly ICommandContext<TIntent> _context;

        public Command(ICommandContext<TIntent> context)
        {
            _context = context;
        }

        public IRequestContext Context => _context;
        public abstract Task<IResult<TOutput>> Execute(TIntent intent);
    }
}
