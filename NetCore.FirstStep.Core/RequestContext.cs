using System.Collections.Generic;

namespace NetCore.FirstStep.Core
{
    public abstract class RequestContext : IRequestContext
    {
        protected readonly IDictionary<object, object> _contextItems;

        public RequestContext(IDictionary<object, object> contextItems)
        {
            _contextItems = contextItems;
        }

        public void CopyTo(IContextHolder destination)
        {
            foreach (var item in _contextItems)
            {
                destination.Context.SetValue((string)item.Key, item.Value);
            }
        }

        public T GetValue<T>(string name)
        {
            object output;

            if (_contextItems.TryGetValue(name, out output))
            {
                return output is T ? (T)output : default(T);
            }
            return default(T);
        }

        public T GetValue<T>()
        {
            object output;

            if(_contextItems.TryGetValue(typeof(T).Name, out output))
            {
                return (T)output;
            }
            return default(T);
        }

        public void SetValue<T>(string name, T value)
        {
            if (!_contextItems.TryAdd(name, value))
            {
                _contextItems[name] = value;
            }
        }

        public void SetValue<T>(T value)
        {
            var t = typeof(T).Name;
            if(! _contextItems.TryAdd(t, value))
            {
                _contextItems[t] = value;
            }
        }
   }
}
