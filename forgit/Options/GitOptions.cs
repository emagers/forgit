using CommandLine;

namespace forgit.Options
{
    [Verb("run", HelpText = "Runs a git command in the given repositories directory.")]
    public class GitOptions
    {
        [Value(0, Required = true, HelpText = "The name of the repository to run git commands against")]
        public string Name { get; set; }
    }
}
