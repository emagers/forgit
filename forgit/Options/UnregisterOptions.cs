using CommandLine;

namespace forgit.Options
{
    [Verb("unregister", HelpText = "Unregisters a repository.")]
    public class UnregisterOptions
    {
        [Option(Default = false, Required = true, HelpText = "The repository name to unregister.")]
        public string Name { get; set; }
    }
}
