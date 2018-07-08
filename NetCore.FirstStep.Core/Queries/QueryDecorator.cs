using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class QueryDecorator<TIntent, TOutput> : Query<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        private readonly IQuery<TIntent, TOutput> _query;

        public QueryDecorator(
            IQueryContext<TIntent> context,
            IQuery<TIntent, TOutput> query) : base(context)
        {
            _query = query;
        }

        protected IQuery<TIntent, TOutput> Instance => _query;

        public override Task<IResult<TOutput>> Fetch(TIntent argument)
        {
            Context.CopyTo(Instance);
            return Instance.Fetch(argument);
        }
    }
}
