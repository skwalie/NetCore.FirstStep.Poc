using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class QueryDecorator<TInput, TOutput> : QueryBase<TInput, TOutput>, IQueryDecorator<TInput, TOutput>
    {
        private readonly IQuery<TInput, TOutput> _query;

        public QueryDecorator(IQuery<TInput, TOutput> query) : base()
        {
            _query = query;
        }

        protected IQuery<TInput, TOutput> Instance => _query;

        public override Task<IResult<TOutput>> Fetch(TInput argument)
        {
            return Instance.Fetch(argument);
        }

        protected override Task<IResult<TOutput>> ProcessQuery(TInput argument)
        {
            throw new NotImplementedException();
        }
    }
}
