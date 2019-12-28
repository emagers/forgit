using CommandLine;
using forgit.Interfaces;

namespace forgit.Options
{
    [Verb("list", HelpText = "Displays all registered repositories.")]
    public class ListOptions : IOptions
    {

    }
}
