using System;
using System.Runtime.Serialization;

namespace forgit.Exceptions
{
    public class RepositoryAlreadyRegisteredException : Exception
    {
        public RepositoryAlreadyRegisteredException()
        {
        }

        public RepositoryAlreadyRegisteredException(string name, string path) : base($"Repository {name} is already registered at {path}")
        {
        }

        public RepositoryAlreadyRegisteredException(string name, string path, Exception innerException) : base($"Repository {name} is already registered at {path}", innerException)
        {
        }

        protected RepositoryAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
