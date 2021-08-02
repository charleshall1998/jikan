using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException(string message) : base(message)
        {
        }

        public InvalidUsernameException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
