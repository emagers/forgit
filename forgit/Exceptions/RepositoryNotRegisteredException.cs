using System;
using System.Runtime.Serialization;

namespace forgit.Exceptions
{
    [Serializable]
    public class RepositoryNotRegisteredException : Exception
    {
        public RepositoryNotRegisteredException()
        {
        }

        public RepositoryNotRegisteredException(string repository) : base($"Repository not registered: {repository}")
        {
        }

        public RepositoryNotRegisteredException(string repository, Exception innerException) : base($"Repository not registered: {repository}", innerException)
        {
        }

        protected RepositoryNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
