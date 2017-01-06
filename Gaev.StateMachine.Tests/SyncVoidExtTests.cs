using System;
using NUnit.Framework;

namespace Gaev.StateMachine.Tests
{
    public class SyncVoidExtTests
    {
        [Test]
        public void Handle()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var resp = Guid.Empty;
            var it = new StateMachine();
            it.Receive<Request>(msg =>
            {
                resp = msg.Id;
            });

            // When
            it.Handle(req);

            // Then
            Assert.AreEqual(req.Id, resp);
        }

        [Test]
        public void HandleAndException()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.Receive<Request>(msg =>
            {
                throw new ResponseException(msg.Id);
            });

            // When
            TestDelegate act = () => it.Handle(req);

            // Then
            Assert.Catch<ResponseException>(act, req.Id.ToString());
        }
    }
}