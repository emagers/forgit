using System;
using System.Runtime.Serialization;

namespace forgit.Exceptions
{
    [Serializable]
    public class CloneException : Exception
    {
        public CloneException(string repoName) : base($"Cloning {repoName} failed.")
        {
        }

        public CloneException(string repoName, Exception innerException) : base($"Cloning {repoName} failed.", innerException)
        {
        }

        public CloneException()
        {
        }

        protected CloneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
