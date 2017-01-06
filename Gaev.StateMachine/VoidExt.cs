using System;
using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public static class VoidExt
    {
        public static Task HandleAsync<TMessage>(this ICanHandle handler, TMessage msg)
        {
            return handler.HandleAsync<TMessage, Void>(msg);
        }

        public static void ReceiveAsync<TMessage>(this IStateMachine sm, Func<TMessage, Task> handler)
        {
            sm.ReceiveAsync<TMessage, Void>(async msg =>
            {
                await handler(msg);
                return Void.Nothing;
            });
        }
    }
}