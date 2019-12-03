using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class Register : BaseCommand
    {
        private readonly RegisterOptions options;

        public Register(ISettings settings, IOutput output, RegisterOptions options) : base(settings, output)
        {
            this.options = options;
        }

        public override async Task Execute()
        {
            RepositoryList repositoryList = await settings.GetRepositories();

            Repository repository = repositoryList.Repositories.FirstOrDefault(repo => repo.Name.Equals(options.Name, StringComparison.OrdinalIgnoreCase));
            if (repository != null)
            {
                throw new RepositoryAlreadyRegisteredException(repository.Name, repository.Path);
            }

            if (string.IsNullOrEmpty(options.Path))
            {
                options.Path = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(options.Name))
            {
                options.Name = options.Path.Split(Path.DirectorySeparatorChar).Last();
            }

            repositoryList.Repositories.Add(new Repository
            {
                Name = options.Name,
                Path = options.Path
            });
            await settings.SaveRepositories(repositoryList);

            await output.WriteLine($"{options.Name} has been registered at {options.Path}", Enums.TextColor.Cyan);
        }
    }
}
