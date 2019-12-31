using System;
using System.Linq;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class Show : BaseCommand, IBaseCommand
    {
        public Show(ISettings settings, IOutput output) : base(settings, output)
        {

        }

        public async Task Execute(IOptions options)
        {
            ShowOptions showOptions = options as ShowOptions;

            RepositoryList repositories = await settings.GetRepositories();

            Repository repo = repositories.Repositories.FirstOrDefault(repo => repo.Name.Equals(showOptions.Name, StringComparison.OrdinalIgnoreCase));

            if (repo == null)
            {
                throw new RepositoryNotRegisteredException(showOptions.Name);
            }

            await output.Write($"\n{repo.Name.PadRight(30)}\t", Enums.TextColor.Cyan);
            await output.WriteLine($"{repo.Path.PadRight(50)}\n", Enums.TextColor.White);
        }
    }
}
