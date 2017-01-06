using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Gaev.StateMachine.Tests
{
    public class SyncExtTests
    {
        [Test]
        public void Handle()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.Receive<Request, Response>(msg =>
            {
                return new Response { Id = msg.Id };
            });

            // When
            var resp = it.Handle<Request, Response>(req);

            // Then
            Assert.AreEqual(req.Id, resp.Id);
        }

        [Test]
        public void HandleAndException()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.Receive<Request, Response>(msg =>
            {
                throw new ResponseException(msg.Id);
            });

            // When
            TestDelegate act = () => it.Handle<Request, Response>(req);

            // Then
            Assert.Catch<ResponseException>(act, req.Id.ToString());
        }

        [Test]
        public async Task HandleAny()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.ReceiveAny(msg =>
            {
                return new Response { Id = ((Request)msg).Id };
            });

            // When
            var resp = await it.HandleAsync<Request, Response>(req);

            // Then
            Assert.AreEqual(req.Id, resp.Id);
        }

        [Test]
        public async Task HandleAnyAndException()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.ReceiveAny(msg =>
            {
                throw new ResponseException(((Request)msg).Id);
            });

            // When
            AsyncTestDelegate act = () => it.HandleAsync<Request, Response>(req);

            // Then
            await AssertExt.CatchAsync<ResponseException>(act, req.Id.ToString());
        }
    }
}