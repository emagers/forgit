using forgit.Commands;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class ShowTests
    {
        private readonly Mock<IOutput> output = new Mock<IOutput>();
        private readonly Mock<ISettings> settings = new Mock<ISettings>();
        private readonly RepositoryList baseList = new RepositoryList
        {
            Repositories = new System.Collections.Generic.List<Repository>
                {
                    new Repository
                    {
                        Name = "default",
                        Path = "C:\\"
                    }
                }
        };

        public ShowTests()
        {
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(baseList);
        }

        [Fact]
        public async Task ShowRepository_RepoDoesntExist_ShouldThrowException()
        {
            Show command = new Show(settings.Object, output.Object);
            await Assert.ThrowsAsync<RepositoryNotRegisteredException>(() => command.Execute(new ShowOptions { Name = "nope" }));
        }

        [Fact]
        public async Task ShowRepository_RepoDoesExist_ShouldOutput()
        {
            Show command = new Show(settings.Object, output.Object);
            await command.Execute(new ShowOptions { Name = "default" });
        }
    }
}
