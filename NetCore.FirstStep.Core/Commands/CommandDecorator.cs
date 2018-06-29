using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Core
{
    public abstract class CommandDecorator<TIntent, TOutput> : Command<TIntent, TOutput>
    {
        private readonly ICommand<TIntent, TOutput> _command;

        public CommandDecorator(
            ICommandContext<TIntent> context,
            ICommand<TIntent, TOutput> command) : base(context)
        {
            _command = command;
        }

        protected ICommand<TIntent, TOutput> Instance => _command;

        public override Task<IResult<TOutput>> Execute(TIntent argument)
        {
            Context.CopyTo(_command);
            return Instance.Execute(argument);
        }
    }
}
