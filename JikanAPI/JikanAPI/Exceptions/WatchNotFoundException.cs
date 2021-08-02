using System;

namespace JikanAPI.Exceptions
{
    [Serializable]
    public class WatchNotFoundException : Exception
    {
        public WatchNotFoundException(string message) : base(message)
        {
        }

        public WatchNotFoundException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
