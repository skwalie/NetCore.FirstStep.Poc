using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCore.FirstStep.DependencyInjection
{
    public class TypeDecorator : TypeDelegator
    {
        private readonly Type _declaringType;

        public TypeDecorator(Type type, Type declaringType = null) : base(type)
        {
            _declaringType = declaringType ?? type;
        }

        public override Type DeclaringType => _declaringType;
    }
}
