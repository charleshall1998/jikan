using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException(string message) : base(message)
        {
        }

        public InvalidPriceException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
