using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message)
        {
        }

        public InvalidNameException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
