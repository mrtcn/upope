using System;

namespace Upope.Game.CustomException
{
    public class AlreadyAnsweredException : Exception
    {
        public AlreadyAnsweredException()
        {
        }

        public AlreadyAnsweredException(string message)
            : base(message)
        {
        }

        public AlreadyAnsweredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
