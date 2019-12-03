using System;
using System.Threading.Tasks;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class ListRepos : BaseCommand
    {
        public ListRepos(ISettings settings, IOutput output) : base(settings, output)
        {

        }

        public override async Task Execute()
        {
            RepositoryList repositories = await settings.GetRepositories();

            await output.Write("Name".PadRight(30), Enums.TextColor.White);
            await output.WriteLine("Path".PadRight(50), Enums.TextColor.White);
            await output.WriteLine("--------------------------------------------------------------------------------", Enums.TextColor.White);
            foreach (Repository repo in repositories.Repositories)
            {
                await output.WriteLine($"{repo.Name.PadRight(30)}{repo.Path.PadRight(50)}", Enums.TextColor.White);
            }
        }
    }
}
