using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class HttpQueryHandler<TIntent, TOutput> : 
        HttpRequestHandler<TIntent, TOutput>,
        IHttpQueryHandler<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        public HttpQueryHandler(
            IQuery<TIntent, TOutput> query, 
            IResponseMapper<TIntent, TOutput> responseMapper) : base(query, responseMapper)
        {
        }

        protected IQuery<TIntent, TOutput> Query => (IQuery<TIntent, TOutput>)ContextHolder;

        protected override Task<IResult<TOutput>> ProcessRequest(TIntent input)
        {
            return Query.Fetch(input);
        }
    }
}
