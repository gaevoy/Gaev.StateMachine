using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private Func<object, object> _anyHandler = Void.ReturnNothing;
        private Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
        public async Task<TResult> HandleAsync<TMessage, TResult>(TMessage msg)
        {
            object handler;
            if (_handlers.TryGetValue(typeof(TMessage), out handler))
                return await ((Func<TMessage, Task<TResult>>)handler)(msg);
            return (TResult)_anyHandler(msg);
        }

        public void Become(Action act)
        {
            _anyHandler = Void.ReturnNothing;
            _handlers = new Dictionary<Type, object>();
            act();
        }

        public void ReceiveAsync<TMessage, TResult>(Func<TMessage, Task<TResult>> handler)
        {
            _handlers[typeof(TMessage)] = handler;
        }

        public void ReceiveAny(Func<object, object> handler)
        {
            _anyHandler = handler;
        }
    }
}
