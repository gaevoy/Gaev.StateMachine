using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Gaev.StateMachine.Tests
{
    public static class AssertExt
    {
        public static async Task<T> CatchAsync<T>(AsyncTestDelegate act, string message = null) where T : Exception
        {
            try
            {
                await act();
            }
            catch (T ex)
            {
                if (message != null && message != ex.Message)
                    throw new AssertionException("Exception message differs");
                return ex;
            }
            throw new AssertionException($"Exception {typeof(T).Name} was not thrown");
        }
    }
}