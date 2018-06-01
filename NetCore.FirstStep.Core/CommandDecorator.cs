using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public class CommandDecorator<TInput, TOutput> : CommandBase<TInput, TOutput>
    {
        private readonly ICommand<TInput, TOutput> _command;

        public CommandDecorator(ICommand<TInput, TOutput> command) : base()
        {
            _command = command;
        }

        protected ICommand<TInput, TOutput> Instance => _command;

        public override Task<IResult<TOutput>> Execute(TInput argument)
        {
            return Instance.Execute(argument);
        }

        protected override Task<IResult<TOutput>> ProcessCommand(TInput argument)
        {
            throw new NotImplementedException();
        }
    }
}
