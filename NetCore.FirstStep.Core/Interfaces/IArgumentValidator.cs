using System.Collections.Generic;

namespace NetCore.FirstStep.Core
{
    public class ValidationError
    {
        public string Ruleset { get; set; }
        public string Value { get; set; }
    }

    public interface IArgumentValidator<T>
    {
        bool Validate(T argument);
        IEnumerable<ValidationError> Errors { get; }
    }
}