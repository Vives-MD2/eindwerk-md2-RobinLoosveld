using System;

namespace Thunderstruck.DOMAIN.Helpers
{
    public enum ExceptionTypes
    {
        Warning,
        Fatal,
    }
    public class ThunderstruckException : Exception
    {
        public ExceptionTypes EType { get; set; } = ExceptionTypes.Fatal;
        public ThunderstruckException()
        {

        }
        public ThunderstruckException(string message, ExceptionTypes eType = ExceptionTypes.Fatal) : base(message)
        {

        }
        public ThunderstruckException(string message, Exception inner, ExceptionTypes eType = ExceptionTypes.Fatal) : base(message, inner)
        {

        }
    }

}