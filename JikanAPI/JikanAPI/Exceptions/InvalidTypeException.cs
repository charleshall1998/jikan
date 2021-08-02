using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message) : base(message)
        {
        }

        public InvalidTypeException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
