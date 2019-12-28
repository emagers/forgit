using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using forgit.Enums;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Options;

namespace forgit.Commands
{
    public class Clone : BaseCommand, IBaseCommand
    {
        private readonly IProcessRunner processRunner;
        private readonly IBaseCommand register;

        public Clone(ISettings settings, IOutput output, IProcessRunner processRunner, IBaseCommand register) : base(settings, output)
        {
            this.register = register;
            this.processRunner = processRunner;
        }

        public async Task Execute(IOptions options)
        {
            CloneOptions cloneOptions = options as CloneOptions;

            string gitProjectName = ParseProjectNameFromGitUrl(cloneOptions.Url);
            if (string.IsNullOrEmpty(cloneOptions.Path)) 
            {
                cloneOptions.Path = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(cloneOptions.Name)) 
            {
                cloneOptions.Name = gitProjectName;
            }

            if (processRunner.InvokeProcess(cloneOptions.Path, "git clone", cloneOptions.Url))
            {

                await output.Write($"{cloneOptions.Url} was cloned to {cloneOptions.Path} with the project name: {cloneOptions.Name}", TextColor.Cyan);
                
                await register.Execute(new RegisterOptions
                {
                    Name = cloneOptions.Name,
                    Path = Path.Combine(cloneOptions.Path, gitProjectName)
                });
            }
            else
            {
                throw new CloneException(gitProjectName);
            }
        }

        private static string ParseProjectNameFromGitUrl(string url) => new Uri(url).Segments.Last().Split('.').First();
    }
}
