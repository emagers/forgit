using CommandLine;
using System;

namespace forgit.Options
{
    [Verb("register", HelpText = "Registers a repository for easy access.")]
    public class RegisterOptions
    {
        [Option(HelpText = "The path of the repository to register. Defaults to current directory.")]
        public string Path { get; set; } = Environment.CurrentDirectory;
        [Option(HelpText = "The name of the repository to register. Defaults to current directory name of the provided path.")]
        public string Name { get; set; }
    }
}
