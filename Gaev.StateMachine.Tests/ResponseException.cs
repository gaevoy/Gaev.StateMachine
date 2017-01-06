using System;

namespace Gaev.StateMachine.Tests
{
    public class ResponseException : Exception
    {
        public ResponseException(Guid id) : base(id.ToString()) { }
    }
}