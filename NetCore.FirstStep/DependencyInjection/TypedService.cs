using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.FirstStep.DependencyInjection
{
    /// <summary>
    /// Identifies a service according to a type to which it can be assigned.
    /// </summary>
    public sealed class TypedService : Service, IServiceWithType, IEquatable<TypedService>
    {
        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        public Type ServiceType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a human-readable description of the service.
        /// </summary>
        /// <value>The description.</value>
        public override string Description
        {
            get
            {
                return this.ServiceType.FullName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the TypedService class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        public TypedService(Type serviceType)
        {
            this.ServiceType = serviceType ?? throw new ArgumentNullException("serviceType");
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(TypedService other)
        {
            return !(other == null) && this.ServiceType == other.ServiceType;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TypedService);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            return this.ServiceType.GetHashCode();
        }

        public Service ChangeType(Type newType)
        {
            if (newType == null)
            {
                throw new ArgumentNullException("newType");
            }
            return new TypedService(newType);
        }
    }
}
