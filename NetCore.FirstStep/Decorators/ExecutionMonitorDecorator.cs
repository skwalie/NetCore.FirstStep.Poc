using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore.FirstStep.Decorators
{
    public class ExecutionMonitorDecorator<TIntent, TOutput> :
        IQuery<TIntent, TOutput>,
        ICommand<TIntent, TOutput>
        where TIntent : IIntent
    {
        public IRequestContext Context { get; }

        private readonly ICommand<TIntent, TOutput> _command;
        private readonly IQuery<TIntent, TOutput> _query;

        public ExecutionMonitorDecorator(
            IQueryContext<TIntent> context,
            IQuery<TIntent, TOutput> query)
        {
            Context = context;
            _query = query;
        }

        public ExecutionMonitorDecorator(
            ICommandContext<TIntent> context,
            ICommand<TIntent, TOutput> command
    )
        {
            Context = context;
            _command = command;
        }

        protected async Task<IResult<TOutput>> Monitor(TIntent input, Func<Task<IResult<TOutput>>> executeFunc)
        {
            var watch = new Stopwatch();

            watch.Start();
            var result = await Task.Run(executeFunc);
            watch.Stop();

            var status = result.IsSuccessful ? "SUCCESS" : "FAILURE";
            Trace.TraceInformation($"{status} [{Thread.CurrentThread.ManagedThreadId}] -> {input.GetType().Name} has been Executed in {watch.ElapsedMilliseconds.ToString("N4")} ms ");

            return result;
        }


        public async Task<IResult<TOutput>> Execute(TIntent intent)
        {
            Context.CopyTo(_command);
            return await Monitor(intent, () => _command.Execute(intent));
        }

        public async Task<IResult<TOutput>> Fetch(TIntent intent)
        {
            Context.CopyTo(_query);
            return await Monitor(intent, () => _query.Fetch(intent));
        }
    }
}
