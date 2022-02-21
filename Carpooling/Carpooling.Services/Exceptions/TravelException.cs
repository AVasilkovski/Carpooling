using System;

namespace Carpooling.Services.Exceptions
{
    public class TravelException : ApplicationException
    {
        public TravelException()
        {

        }

        public TravelException(string message)
            : base(message)
        {

        }
    }
}
