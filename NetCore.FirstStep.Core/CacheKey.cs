using System;
using System.Collections.Generic;

namespace NetCore.FirstStep.Core
{
    public class CacheKey
    {
        public CacheKey(string value, params CacheKey[] dependencies)
        {
            Value = value ?? throw new ArgumentNullException("value");
            Dependencies = dependencies ?? new CacheKey[] { } ;
        }

        public CacheKey(string value) : this(value, null)
        {

        }

        public string Value { get; }
        public IEnumerable<CacheKey> Dependencies { get; }

        public override bool Equals(object obj)
        {
            return Value == (obj as CacheKey)?.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
