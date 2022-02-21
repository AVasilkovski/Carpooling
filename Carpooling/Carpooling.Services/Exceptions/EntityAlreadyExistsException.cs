using System;

namespace Carpooling.Services.Exceptions
{
    public class EntityAlreadyExistsException : ApplicationException
    {
        public EntityAlreadyExistsException()
        {

        }

        public EntityAlreadyExistsException(string message)
            : base(message)
        {

        }
    }
}
