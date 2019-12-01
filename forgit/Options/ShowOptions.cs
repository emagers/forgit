using CommandLine;

namespace forgit.Options
{
    [Verb("show", HelpText = "Displays a given repository.")]
    public class ShowOptions
    {
        [Option(Default = false, Required = true, HelpText = "The repository name to display.")]
        public string Name { get; set; }
    }
}
