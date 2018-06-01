//using NetCore.FirstStep.Core;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NetCore.FirstStep.Business.Arguments
//{
//    public class ArgumentValidatorBase<TArgument> : IArgumentValidator<TArgument>
//    {
//        public string[] ErrorMessages { get; private set; }

//        public virtual bool Validate(TArgument argument)
//        {
//            if (argument == null) throw new ArgumentNullException();
//            return true;
//        }

//        protected void SetErrorMessages(params string[] messages)
//        {
//            ErrorMessages = messages;
//        }
//    }
//}
