using System;
using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public static class SyncVoidExt
    {
        public static void Receive<TMessage>(this IStateMachine sm, Action<TMessage> handler)
        {
            sm.ReceiveAsync<TMessage>(msg =>
            {
                try
                {
                    handler(msg);
                    return Void.CompletedTask;
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }
            });
        }

        public static void Handle<TMessage>(this ICanHandle handler, TMessage msg)
        {
            handler.Handle<TMessage, Void>(msg);
        }
    }
}