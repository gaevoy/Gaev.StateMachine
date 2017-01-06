using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public sealed class Void
    {
        private Void() { }
        public static readonly Task<object> CompletedObjectTask = Task.FromResult<object>(null);
        public static readonly Void Nothing = new Void();
        public static readonly Task<Void> CompletedTask = Task.FromResult(Nothing);
        public static void DoNothing(object arg1) { }
        public static object ReturnNothing(object arg1) => null;
        public static Task<object> ReturnCompletedObjectTask(object arg1) => CompletedObjectTask;
    }
}