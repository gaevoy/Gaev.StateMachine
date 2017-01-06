using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Gaev.StateMachine.Tests
{
    public class StateMachineTests
    {
        [Test]
        public async Task HandleAsync()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.ReceiveAsync<Request, Response>(async msg =>
            {
                await Task.Delay(5);
                return new Response { Id = msg.Id };
            });

            // When
            var resp = await it.HandleAsync<Request, Response>(req);

            // Then
            Assert.AreEqual(req.Id, resp.Id);
        }

        [Test]
        public async Task HandleAsyncAndException()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.ReceiveAsync<Request, Response>(async msg =>
            {
                await Task.Delay(5);
                throw new ResponseException(msg.Id);
            });

            // When
            AsyncTestDelegate act = () => it.HandleAsync<Request, Response>(req);

            // Then
            await AssertExt.CatchAsync<ResponseException>(act, req.Id.ToString());
        }

        [Test]
        public async Task HandleAny()
        {
            // Given
            var req = new Request { Id = Guid.NewGuid() };
            var it = new StateMachine();
            it.ReceiveAnyAsync(async msg =>
            {
                await Task.Delay(5);
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
            it.ReceiveAnyAsync(async msg =>
            {
                await Task.Delay(5);
                throw new ResponseException(((Request)msg).Id);
            });

            // When
            AsyncTestDelegate act = () => it.HandleAsync<Request, Response>(req);

            // Then
            await AssertExt.CatchAsync<ResponseException>(act, req.Id.ToString());
        }
    }
}
