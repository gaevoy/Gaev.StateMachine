using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Gaev.StateMachine.Tests
{
    public class VoidExtTests
    {
        [Test]
        public async Task HandleAsync()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var resp = Guid.Empty;
            var it = new StateMachine();
            it.ReceiveAsync<Request>(async msg =>
            {
                await Task.Delay(5);
                resp = msg.Id;
            });

            // When
            await it.HandleAsync(req);

            // Then
            Assert.AreEqual(req.Id, resp);
        }

        [Test]
        public async Task HandleAsyncAndException()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.ReceiveAsync<Request>(async msg =>
            {
                await Task.Delay(5);
                throw new ResponseException(msg.Id);
            });

            // When
            AsyncTestDelegate act = () => it.HandleAsync(req);

            // Then
            await AssertExt.CatchAsync<ResponseException>(act, req.Id.ToString());
        }
    }
}