using System;

namespace Upope.Challenge.CustomExceptions
{
    public class UserNotAvailableException : Exception
    {
        public UserNotAvailableException()
        {
        }

        public UserNotAvailableException(string message)
            : base(message)
        {
        }

        public UserNotAvailableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
