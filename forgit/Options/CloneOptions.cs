using CommandLine;
using forgit.Interfaces;
using System;

namespace forgit.Options
{
    [Verb("clone", HelpText = "Clones and registers a repository at the given path.")]
    public class CloneOptions : IOptions
    {
        [Option(HelpText = "The name of the repository being cloned. Defaults to the repository name from the URL.")]
        public string Name { get; set; }
        [Option(HelpText = "The path to clone the repository to. Defaults to the current directory.")]
        public string Path { get; set; } = Environment.CurrentDirectory;
        [Option(Default = false, Required = true, HelpText = "The clone URL for the repository.")]
        public string Url { get; set; }
    }
}
