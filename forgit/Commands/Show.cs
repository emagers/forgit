using System;
using System.Linq;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class Show : BaseCommand
    {
        private readonly ShowOptions options;

        public Show(ISettings settings, IOutput output, ShowOptions options) : base(settings, output)
        {
            this.options = options;
        }

        public override async Task Execute()
        {
            RepositoryList repositories = await settings.GetRepositories();

            Repository repo = repositories.Repositories.FirstOrDefault(repo => repo.Name.Equals(options.Name, StringComparison.OrdinalIgnoreCase));

            if (repo == null)
            {
                throw new RepositoryNotRegisteredException(options.Name);
            }

            await output.WriteLine($"{repo.Name.PadRight(30)}{repo.Path.PadRight(50)}", Enums.TextColor.White);
        }
    }
}
