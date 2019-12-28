using CommandLine;
using forgit.Interfaces;

namespace forgit.Options
{
    [Verb("run", HelpText = "Runs a git command in the given repositories directory.")]
    public class GitOptions : IOptions
    {
        [Value(0, Required = true, HelpText = "The name of the repository to run git commands against")]
        public string Name { get; set; }
    }
}
