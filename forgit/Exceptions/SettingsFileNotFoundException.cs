using System;
using System.Runtime.Serialization;

namespace forgit.Exceptions
{
    [Serializable]
    public class SettingsFileNotFoundException : Exception
    {
        public SettingsFileNotFoundException(string path) : base($"Settings file not found: {path}")
        {
        }

        public SettingsFileNotFoundException(string path, Exception innerException) : base($"Settings file not found: {path}", innerException)
        {
        }

        public SettingsFileNotFoundException() : base("Settings file not found.")
        {
        }

        protected SettingsFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
