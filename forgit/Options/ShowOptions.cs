using CommandLine;
using forgit.Interfaces;

namespace forgit.Options
{
    [Verb("show", HelpText = "Displays a given repository.")]
    public class ShowOptions : IOptions
    {
        [Option(shortName: 'n', Default = false, Required = true, HelpText = "The repository name to display.")]
        public string Name { get; set; }
    }
}
