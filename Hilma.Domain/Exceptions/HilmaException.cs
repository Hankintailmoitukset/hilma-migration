using System;

namespace Hilma.Domain.Exceptions
{
    public class HilmaException : Exception
    {
        public HilmaException() { }
        public HilmaException(string msg) : base(msg) { }
    }

    public class HilmaUnauthorizedException : HilmaException
    {
        public HilmaUnauthorizedException() { }
        public HilmaUnauthorizedException(string msg) : base(msg) { }
    }

    public class HilmaUpstreamException : HilmaException
    {
        public HilmaUpstreamException() { }
        public HilmaUpstreamException(string msg) : base(msg) { }
    }

    public class HilmaMalformedRequestException : HilmaException
    {
        public HilmaMalformedRequestException() { }
        public HilmaMalformedRequestException(string msg) : base(msg) { }
    }

    public class HilmaIdentifierException : HilmaException
    {
        public HilmaIdentifierException() { }
        public HilmaIdentifierException(string msg) : base(msg) { }
    }

    public class HilmaNotFoundException : HilmaException
    {

    }
}
