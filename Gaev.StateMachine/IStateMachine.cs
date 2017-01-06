using System;
using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public interface IStateMachine : ICanHandle
    {
        void Become(Action act);
        void ReceiveAsync<TMessage, TResult>(Func<TMessage, Task<TResult>> handler);
        void ReceiveAnyAsync(Func<object, Task<object>> handler);
    }
}