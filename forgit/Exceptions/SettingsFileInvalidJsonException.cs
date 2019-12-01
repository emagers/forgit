using System;
using System.Runtime.Serialization;

namespace forgit.Exceptions
{
    [Serializable]
    public class SettingsFileInvalidJsonException : Exception
    {
        public SettingsFileInvalidJsonException(string path) : base($"Could not parse settings file at {path}")
        {
        }

        public SettingsFileInvalidJsonException(string path, Exception innerException) : base($"Could not parse settings file at {path}", innerException)
        {
        }

        public SettingsFileInvalidJsonException()
        {
        }

        protected SettingsFileInvalidJsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
