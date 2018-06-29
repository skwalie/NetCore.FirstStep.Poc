using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public class CacheInvalidatorDecorator<TIntent, TOutput> : CommandDecorator<TIntent, TOutput>
        where TIntent : ICommandIntent
    {
        private readonly ICacheService<TIntent, TOutput> _cacheService;

        public CacheInvalidatorDecorator(
            ICommandContext<TIntent> context, 
            ICommand<TIntent, TOutput> command,
            ICacheService<TIntent, TOutput> cacheService) : base(context, command)
        {
            _cacheService = cacheService;
        }

        public override async Task<IResult<TOutput>> Execute(TIntent argument)
        {
            var result = await base.Execute(argument);

            if(result.IsSuccessful)
            {
                _cacheService.Invalidate(argument);
            }

            return result;
        }
    }
}
