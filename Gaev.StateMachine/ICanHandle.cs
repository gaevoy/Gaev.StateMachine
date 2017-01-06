using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public interface ICanHandle
    {
        Task<TResult> HandleAsync<TMessage, TResult>(TMessage msg);
    }
}