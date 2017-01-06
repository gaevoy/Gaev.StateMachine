using System;
using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public static class SyncExt
    {
        public static void Receive<TMessage, TResult>(this IStateMachine sm, Func<TMessage, TResult> handler)
        {
            sm.ReceiveAsync<TMessage, TResult>(msg =>
            {
                try
                {
                    return Task.FromResult(handler(msg));
                }
                catch (Exception ex)
                {
                    return Task.FromException<TResult>(ex);
                }
            });
        }

        public static TResult Handle<TMessage, TResult>(this ICanHandle handler, TMessage msg)
        {
            var task = handler.HandleAsync<TMessage, TResult>(msg);
            if (task.IsCompleted)
            {
                if (task.IsFaulted)
                    throw task.Exception.InnerException;
                return task.Result;
            }
            throw new NotSupportedException();
        }
    }
}