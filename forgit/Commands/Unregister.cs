using System;
using System.Linq;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class Unregister : BaseCommand
    {
        private readonly UnregisterOptions options;

        public Unregister(ISettings settings, IOutput output, UnregisterOptions options) : base(settings, output)
        {
            this.options = options;
        }

        public override async Task Execute()
        {
            RepositoryList repositoryList = await settings.GetRepositories();

            Repository repository = repositoryList.Repositories.FirstOrDefault(repo => repo.Name.Equals(options.Name, StringComparison.OrdinalIgnoreCase));

            if (repository == null)
            {
                throw new RepositoryNotRegisteredException(options.Name);
            }

            repositoryList.Repositories.Remove(repository);
            await settings.SaveRepositories(repositoryList);

            await output.WriteLine($"{options.Name} has been unregistered", Enums.TextColor.Cyan);
        }
    }
}
