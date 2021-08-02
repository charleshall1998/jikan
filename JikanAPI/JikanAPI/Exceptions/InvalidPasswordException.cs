using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message)
        {
        }

        public InvalidPasswordException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
