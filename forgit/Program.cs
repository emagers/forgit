using forgit.Options;
using System;
using CommandLine;
using System.Linq;
using System.Collections.Generic;
using forgit.Commands;
using forgit.Interfaces;
using forgit.Providers;
using System.IO;
using System.Reflection;

namespace forgit
{
    class Program
    {
        private const string SETTINGS_NAME = "settings.json";
        private static ISettings settings;
        private static IOutput outputter;
        private static IProcessRunner processRunner;

        static void Main(string[] args)
        {
            settings = new Settings(Path.Combine(AssemblyDirectory, SETTINGS_NAME));
            outputter = new ConsoleOutputter();
            processRunner = new ProcessRunner(outputter);

            string gitRunError = null;

            try
            {
                if (args.Length > 1)
                {
                    Git git = new Git(settings, outputter, processRunner);
                    git.Execute(new GitOptions
                    {
                        Name = args.First(),
                        Command = string.Join(" ", args.Skip(1))
                    }).Wait();
                }
            }
            catch(Exception exception)
            {
                gitRunError = exception.Message;
            }

            if (args.Length <= 2 && !string.IsNullOrEmpty(gitRunError))
            {
                var parserResult = Parser.Default.ParseArguments<CloneOptions, RegisterOptions, UnregisterOptions, ShowOptions, ListOptions>(args);
                try
                {
                    List<Error> result = parserResult.MapResult<CloneOptions, RegisterOptions, UnregisterOptions, ShowOptions, ListOptions, List<Error>>(
                        (CloneOptions) => {
                            new Clone(
                                settings,
                                outputter,
                                processRunner,
                                new Register(settings, outputter)
                            ).Execute(CloneOptions).Wait();

                            return new List<Error>();
                        },
                        (RegisterOptions) =>
                        {
                            new Register(settings, outputter).Execute(RegisterOptions).Wait();

                            return new List<Error>();
                        },
                        (UnregisterOptions) =>
                        {
                            new Unregister(settings, outputter).Execute(UnregisterOptions).Wait();

                            return new List<Error>();
                        },
                        (ShowOptions) =>
                        {
                            new Show(settings, outputter).Execute(ShowOptions).Wait();

                            return new List<Error>();
                        },
                        (ListOptions) =>
                        {
                            new ListRepos(settings, outputter).Execute(ListOptions).Wait();

                            return new List<Error>();
                        },
                        errs => errs.ToList());

                    if (result.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"To run a git command in a registered project directory, use the format `<repositoryName> <gitCommand> <args>`");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
