using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException(string message) : base(message)
        {
        }

        public InvalidIdException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
