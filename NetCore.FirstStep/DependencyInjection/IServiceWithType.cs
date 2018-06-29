using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.DependencyInjection
{
    /// <summary>
    /// Interface supported by services that carry type information.
    /// </summary>
    public interface IServiceWithType
    {
        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        Type ServiceType
        {
            get;
        }

        /// <summary>
        /// Return a new service of the same kind, but carrying
        /// <paramref name="newType" /> as the <see cref="P:Autofac.Core.IServiceWithType.ServiceType" />.
        /// </summary>
        /// <param name="newType">The new service type.</param>
        /// <returns>A new service with the service type.</returns>
        Service ChangeType(Type newType);
    }
}
