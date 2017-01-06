using System.Threading.Tasks;

namespace Gaev.StateMachine
{
    public sealed class Void
    {
        private Void() { }
        public static readonly Void Nothing = new Void();
        public static readonly Task<Void> CompletedTask = Task.FromResult(Nothing);
        public static void DoNothing(object arg1) { }
        public static object ReturnNothing(object arg1) { return null; }
    }
}