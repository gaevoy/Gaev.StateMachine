// ReSharper disable InconsistentNaming

namespace Gaev.StateMachine.Tests
{
    public class Delivery
    {
        private readonly IStateMachine it = new StateMachine();
        public string StateName;

        public Delivery()
        {
            it.Become(New);
        }

        public void Handle<TMessage>(TMessage msg) => it.Handle(msg);

        private void New()
        {
            StateName = nameof(New);
            it.Receive<Send>(msg =>
            {
                // sent logic
                it.Become(Sent);
            });
            it.Receive<Cancel>(msg =>
            {
                // cancel logic
                it.Become(Canceled);
            });
            it.ReceiveAny(Void.ReturnNothing);
        }

        private void Sent()
        {
            StateName = nameof(Sent);
            it.Receive<Receive>(msg =>
            {
                // receive logic
                it.Become(Received);
            });
            it.ReceiveAny(Void.ReturnNothing);
        }

        private void Canceled()
        {
            StateName = nameof(Canceled);
            it.ReceiveAny(Void.ReturnNothing);
        }

        private void Received()
        {
            StateName = nameof(Received);
            it.ReceiveAny(Void.ReturnNothing);
        }

        public class Send { }
        public class Receive { }
        public class Cancel { }
    }
}