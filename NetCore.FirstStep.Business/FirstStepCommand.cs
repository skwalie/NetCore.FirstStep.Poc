using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business
{
    public abstract class FirstStepCommand<TInput, TOutput> : CommandBase<TInput, TOutput>
    {
        private readonly IFirstStepBusinessManager _businessManager;

        public FirstStepCommand(IFirstStepBusinessManager manager) : base()
        {
            _businessManager = manager;
        }

        protected IFirstStepBusinessManager BusinessManager => _businessManager;
    }
}
