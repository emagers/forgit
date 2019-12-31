using CommandLine;
using forgit.Interfaces;
using System;

namespace forgit.Options
{
    [Verb("register", HelpText = "Registers a repository for easy access.")]
    public class RegisterOptions : IOptions
    {
        [Option(shortName: 'p', longName: "path", HelpText = "The path of the repository to register. Defaults to current directory.")]
        public string Path { get; set; } = Environment.CurrentDirectory;
        [Option(shortName: 'n', longName: "name", HelpText = "The name of the repository to register. Defaults to current directory name of the provided path.")]
        public string Name { get; set; }
    }
}
