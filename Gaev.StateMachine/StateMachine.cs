using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public sealed class StateMachine : IStateMachine
    {
        private Func<object, Task<object>> _any = Void.ReturnNothingAsync;
        private Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
        public async Task<TResult> HandleAsync<TMessage, TResult>(TMessage msg)
        {
            object handler;
            if (_handlers.TryGetValue(typeof(TMessage), out handler))
                return await ((Func<TMessage, Task<TResult>>)handler)(msg);
            return (TResult)await _any(msg);
        }

        public void Become(Action state)
        {
            _any = Void.ReturnNothingAsync;
            _handlers = new Dictionary<Type, object>();
            state();
        }

        public void ReceiveAsync<TMessage, TResult>(Func<TMessage, Task<TResult>> handler)
        {
            _handlers[typeof(TMessage)] = handler;
        }

        public void ReceiveAnyAsync(Func<object, Task<object>> handler)
        {
            _any = handler;
        }
    }
}
