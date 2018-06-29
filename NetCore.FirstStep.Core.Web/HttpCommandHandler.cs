﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace NetCore.FirstStep.Core
{ 
    public abstract class HttpCommandHandler<TIntent, TOutput> : HttpRequestHandler<TIntent, TOutput>,
        IHttpCommandHandler<TIntent, TOutput>
        where TIntent : ICommandIntent
    {
        public HttpCommandHandler(
            ICommand<TIntent, TOutput> command, 
            IResponseMapper<TIntent, TOutput> responseMapper) : base(command, responseMapper)
        {
        }

        protected ICommand<TIntent, TOutput> Command => (ICommand<TIntent, TOutput>)ContextHolder;

        protected override Task<IResult<TOutput>> ProcessRequest(TIntent input)
        {
            return Command.Execute(input);
        }
    }
}
