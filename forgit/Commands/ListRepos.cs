using System.Threading.Tasks;
using forgit.Interfaces;
using forgit.Models;

namespace forgit.Commands
{
    public class ListRepos : BaseCommand, IBaseCommand
    {
        public ListRepos(ISettings settings, IOutput output) : base(settings, output)
        {

        }

        public async Task Execute(IOptions options)
        {
            RepositoryList repositories = await settings.GetRepositories();

            foreach (Repository repo in repositories.Repositories)
            {
                await output.Write($"\n{repo.Name.PadRight(30)}\t", Enums.TextColor.Cyan);
                await output.WriteLine($"{repo.Path.PadRight(50)}\n", Enums.TextColor.White);
            }
        }
    }
}
