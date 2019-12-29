using CommandLine;
using forgit.Interfaces;

namespace forgit.Options
{
    [Verb("unregister", HelpText = "Unregisters a repository.")]
    public class UnregisterOptions : IOptions
    {
        [Option(shortName: 'n', Default = false, Required = true, HelpText = "The repository name to unregister.")]
        public string Name { get; set; }
    }
}
