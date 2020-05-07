using System;

namespace Hilma.Domain.Exceptions
{
    public class HilmaUnexpectedException : Exception
    {
        public HilmaUnexpectedException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
