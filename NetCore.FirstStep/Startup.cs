using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.FirstStep.Business;
using NetCore.FirstStep.Business.Implementation;
using NetCore.FirstStep.Core;
using NetCore.FirstStep.Handlers;
using NetCore.FirstStep.Decorators;
using System;
using System.Linq;
using NetCore.FirstStep.Business.Mongo;

namespace NetCore.FirstStep
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery();
            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;

                config.InputFormatters.Add(new XmlSerializerInputFormatter());
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                // ....

            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddMemoryCache();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterGeneric(typeof(DefaultQueryHandler<,>)).As(typeof(IHttpQueryHandler<,>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(DefaultCommandHandler<,>)).As(typeof(IHttpCommandHandler<,>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(CommandContext<>)).As(typeof(ICommandContext<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(QueryContext<>)).As(typeof(IQueryContext<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(MemoryCacheService<,>)).As(typeof(ICacheService<,>)).InstancePerDependency();

            builder.RegisterType<MongoBusinessManager>().As<IFirstStepBusinessManager>().InstancePerDependency();
            builder.RegisterType<MongoBusinessManager>().As<IFirstStepReadManager>().InstancePerDependency();
            builder.RegisterInstance(new MongoDefaultConfig("mongodb://localhost:27017", "first-step")).As<IMongoConfig>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(asm => asm.FullName.StartsWith("NetCore"))
                .ToArray();

            builder.RegisterImplementedInterfacesOf(
                assemblies,
                //typeof(ICommand<,>),
                //typeof(IQuery<,>),
                typeof(ICacheKeyBuilder<>),
                typeof(IResponseMapper<,>),
                typeof(IMapper<>),
                typeof(IMapper<,>),
                typeof(IMapper<,,>),
                typeof(IMapper<,,,>)
                );

           builder.RegisterDecoratorsOf(
              assemblies,
              typeof(IQuery<,>),
              typeof(QueryCacheDecorator<,>),
               typeof(ExecutionMonitorDecorator<,>),
              typeof(DefaultExceptionHandlerQueryDecorator<,>)
              );

            builder.RegisterDecoratorsOf(
               assemblies,
               typeof(ICommand<,>),
               typeof(CacheInvalidatorDecorator<,>),
               typeof(ExecutionMonitorDecorator<,>),
               typeof(DefaultExceptionHandlerCommandDecorator<,>)
               );


            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(input => input);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.Use(input => input);

        }

        public RequestDelegate PreProcess(RequestDelegate request)
        {

            return request;
        }
    }
}
