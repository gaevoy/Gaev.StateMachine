using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public abstract class StateMachineBase: ICanHandle
    {
        protected readonly IStateMachine It = new StateMachine();
        public Task<TResult> HandleAsync<TMessage, TResult>(TMessage msg) => It.HandleAsync<TMessage, TResult>(msg);
    }
}
