using System;

namespace Backend.Common.Exceptions
{
    public class NotFoundErrorException : Exception
    {
        public NotFoundErrorException(string message) : base(message)
        {
        }

        public NotFoundErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundErrorException()
        {
        }
    }
}
