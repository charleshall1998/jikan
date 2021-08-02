using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message)
        {
        }

        public InvalidQuantityException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
