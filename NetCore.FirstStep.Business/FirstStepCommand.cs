using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Business.Commands
{
    public abstract class FirstStepCommand<TIntent, TOutput> : 
        Command<TIntent, TOutput>,
        IContextHolder
    {
        private readonly IFirstStepBusinessManager _businessManager;

        public FirstStepCommand(
            ICommandContext<TIntent> context,  
            IFirstStepBusinessManager manager) : base(context)
        {
            _businessManager = manager;
        }

        protected async Task<IResult<TOutput>> HandleResult(TIntent intent, Func<Task<TOutput>> managerFunc)
        {
            try
            {
                var response = await managerFunc();

                if (response == null)
                {
                    throw new NullResultException((IIntent)intent, this);
                }

                return response.ToResult();
            }
            catch (Exception exception)
            {
                throw new IntentException((IIntent)intent, this, exception);
            }
        }

        protected IFirstStepBusinessManager BusinessManager => _businessManager;
    }
}
