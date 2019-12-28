using System;
using System.Linq;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class Unregister : BaseCommand, IBaseCommand
    {
        public Unregister(ISettings settings, IOutput output) : base(settings, output)
        {

        }

        public async Task Execute(IOptions options)
        {
            UnregisterOptions unregisterOptions = options as UnregisterOptions;

            RepositoryList repositoryList = await settings.GetRepositories();

            Repository repository = repositoryList.Repositories.FirstOrDefault(repo => repo.Name.Equals(unregisterOptions.Name, StringComparison.OrdinalIgnoreCase));

            if (repository == null)
            {
                throw new RepositoryNotRegisteredException(unregisterOptions.Name);
            }

            repositoryList.Repositories.Remove(repository);
            await settings.SaveRepositories(repositoryList);

            await output.WriteLine($"{unregisterOptions.Name} has been unregistered", Enums.TextColor.Cyan);
        }
    }
}
