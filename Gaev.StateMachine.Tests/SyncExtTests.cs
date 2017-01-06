using System;
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
    }
}