using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgit.Commands
{
    public class Git : BaseCommand, IBaseCommand
    {
        private const string GIT = "git";
        private readonly IProcessRunner processRunner;

        public Git(ISettings settings, IOutput output, IProcessRunner processRunner) : base(settings, output)
        {
            this.processRunner = processRunner;
        }

        private async Task Execute(GitOptions options)
        {
            RepositoryList repositories = await settings.GetRepositories();
            Repository target = repositories.Repositories.FirstOrDefault(x => x.Name.Equals(options.Name));

            if (target == null)
                throw new RepositoryNotRegisteredException(options.Name);

            List<string> command = options.Command.Split(' ').ToList();
            if (command.Count <= 1 || command[0] != GIT)
            {
                command = command.Prepend(GIT).ToList();
            }
            string finalCommand = string.Join(' ', command);

            if (!processRunner.InvokeProcess(target.Path, finalCommand, string.Empty))
            {
                throw new CommandExecutionException(finalCommand, target.Path);
            }
        }

        public async Task Execute(IOptions options)
        {
            await Execute(options as GitOptions);
        }
    }
}
