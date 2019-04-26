using System;

namespace Upope.Game.CustomException
{
    public class WrongChoiceTypeException : Exception
    {
        public WrongChoiceTypeException()
        {
        }

        public WrongChoiceTypeException(string message)
            : base(message)
        {
        }

        public WrongChoiceTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
