using NetCore.FirstStep.Core;

namespace NetCore.FirstStep.Business.Queries
{
    public abstract class FirstStepQuery<TIntent, TOutput> : Query<TIntent, TOutput>
        where TIntent : IQueryIntent
    {
        private readonly IFirstStepReadManager _businessManager;

        public FirstStepQuery(IQueryContext<TIntent> context, IFirstStepReadManager manager) : base(context)
        {
            _businessManager = manager;
        }
      
        public IFirstStepReadManager BusinessManager => _businessManager;
    }
}
