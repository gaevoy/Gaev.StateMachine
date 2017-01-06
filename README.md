# Gaev.StateMachine
Akka.NET actor has really useful feature is [switching the behavior](https://codingjourneyman.com/2015/08/24/switching-the-behavior-of-an-akka-net-actor/) using Become() method.
It inspired me to make simple implementation to use Become() separately outside Akka.NET.

StateMachine can be used via Inheritance (IS-A) see [StateMachineBase](https://github.com/gaevoy/Gaev.StateMachine/blob/master/Gaev.StateMachine/StateMachineBase.cs) or Composition (HAS-A) see example [below](https://github.com/gaevoy/Gaev.StateMachine/blob/master/Gaev.StateMachine.Tests/Delivery.cs#L8). StateMachine supports [sync](https://github.com/gaevoy/Gaev.StateMachine/blob/master/Gaev.StateMachine/SyncExt.cs) and [async](https://github.com/gaevoy/Gaev.StateMachine/blob/master/Gaev.StateMachine/IStateMachine.cs) handlers.

See example of usage [here](https://github.com/gaevoy/Gaev.StateMachine/blob/master/Gaev.StateMachine.Tests/Delivery.cs#L6): 

```csharp
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
        it.ReceiveAny(NotSupported);
    }

    private void Sent()
    {
        StateName = nameof(Sent);
        it.Receive<Receive>(msg =>
        {
            // receive logic
            it.Become(Received);
        });
        it.ReceiveAny(NotSupported);
    }

    private void Canceled()
    {
        StateName = nameof(Canceled);
        it.ReceiveAny(NotSupported);
    }

    private void Received()
    {
        StateName = nameof(Received);
        it.ReceiveAny(NotSupported);
    }

    private object NotSupported(object msg)
    {
        throw new NotSupportedException();
    }

    public class Send { public string Address; }
    public class Receive { public string Feedback; }
    public class Cancel { public string Reason; }
}

class DeliveryExamples
{
    void ItCanSendThenReceive()
    {
        var delivery = new Delivery();
        delivery.Handle(new Delivery.Send { Address = "Redmond, WA 98052-7329, USA" });
        delivery.Handle(new Delivery.Receive { Feedback = "Wow, thanks!" });
    }
    void ItCanCancel()
    {
        var delivery = new Delivery();
        delivery.Handle(new Delivery.Cancel { Reason = "Running out of money" });
        delivery.Handle(new Delivery.Send { Address = "Redmond, WA 98052-7329, USA" }); // NotSupportedException
    }
    void ItCanNotCancel()
    {
        var delivery = new Delivery();
        delivery.Handle(new Delivery.Send { Address = "Redmond, WA 98052-7329, USA" });
        delivery.Handle(new Delivery.Cancel { Reason = "Running out of money" }); // NotSupportedException
    }
}
```
