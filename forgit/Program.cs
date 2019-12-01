using forgit.Options;
using System;
using CommandLine;
using System.Linq;

namespace forgit
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<CloneOptions, RegisterOptions, UnregisterOptions, ShowOptions, ListOptions, GitOptions>(args);
            string result = parserResult.MapResult<CloneOptions, RegisterOptions, UnregisterOptions, ShowOptions, ListOptions, GitOptions, string>(
                    (CloneOptions) => "clone",
                    (RegisterOptions) => "register",
                    (UnregisterOptions) => "unregister",
                    (ShowOptions) => "show",
                    (ListOptions) => "list",
                    (GitOptions) => $"git {GitOptions.Name}",
                    errs => string.Join("\n", errs.Select(err => err.ToString())));

            Console.WriteLine(result);
        }
    }
}
