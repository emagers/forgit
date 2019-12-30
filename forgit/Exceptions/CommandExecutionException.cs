using System;
using System.Runtime.Serialization;

namespace forgit.Exceptions
{
    [Serializable]
    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(string command, string path) : base($"Error running command `{command}` at path `{path}`")
        {
        }

        public CommandExecutionException(string command, string path, Exception innerException) : base($"Error running command `{command}` at path `{path}`", innerException)
        {
        }

        public CommandExecutionException()
        {
        }

        protected CommandExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
