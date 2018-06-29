using Autofac;
using Autofac.Core;
using System;
using System.Linq;
using System.Reflection;

namespace NetCore.FirstStep
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterDecoratorsOf(this ContainerBuilder builder,
            Assembly[] assemblies,
            Type decoratedType,
            params Type[] decorators)
        {
            Func<Type, string> getKey = t => new string(t.Name.Where(x => char.IsLetter(x)).ToArray());
            var decoratedObjectKey = getKey(decoratedType);

            var implementations = assemblies
                .SelectMany(asm => asm.GetTypes().Where(type => type.IsClosedTypeOf(decoratedType))
                .Select(t => (t, t.GetInterfaces().Single(type => type.IsClosedTypeOf(decoratedType))))
                    );


            foreach (var imp in implementations)
            {
                builder
                    .RegisterType(imp.Item1)
                    .Named(decoratedObjectKey, imp.Item2)
                    .InstancePerDependency();
            }

            foreach (var decoratorType in decorators)
            {
                var decoratorObjectKey = decorators.Last() == decoratorType ? null : getKey(decoratorType);
                
                builder.RegisterGenericDecorator(decoratorType, decoratedType, decoratedObjectKey, decoratorObjectKey )
                       .InstancePerDependency();

                decoratedObjectKey = decoratorObjectKey;
            }
        }

        public static void RegisterImplementedInterfacesOf(this ContainerBuilder builder, Assembly[] assemblies, params Type[] genericInterfaces)
        {
            genericInterfaces.ToList().ForEach(x => 
                builder.RegisterImplementedInterfaceOf(assemblies, x));
        }

        public static void RegisterImplementedInterfaceOf(this ContainerBuilder builder, Assembly[] assemblies, Type genericInterface)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .As(t => t.GetInterfaces().Where(i => i.IsClosedTypeOf(genericInterface)))
                .InstancePerDependency();
        }
    }
}
