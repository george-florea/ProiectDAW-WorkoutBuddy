using System.Runtime.Serialization;

namespace Backend.BusinessLogic.Splits
{
    [Serializable]
    internal class ForbiddenErrorException : Exception
    {
        public ForbiddenErrorException()
        {
        }

        public ForbiddenErrorException(string? message) : base(message)
        {
        }

        public ForbiddenErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ForbiddenErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}