using forgit.Commands;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Options;
using forgit.Providers;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class ShowTests
    {
        private readonly ISettings settings = new Settings("validSettings.json");
        private readonly Mock<IOutput> output = new Mock<IOutput>();

        [Fact]
        public async Task ShowRepository_RepoDoesntExist_ShouldThrowException()
        {
            Show command = new Show(settings, output.Object);
            await Assert.ThrowsAsync<RepositoryNotRegisteredException>(() => command.Execute(new ShowOptions { Name = "nope" }));
        }

        [Fact]
        public async Task ShowRepository_RepoDoesExist_ShouldOutput()
        {
            Show command = new Show(settings, output.Object);
            await command.Execute(new ShowOptions { Name = "default" });
            output.Verify(x => x.WriteLine($"{"default".PadRight(30)}{"C:/".PadRight(50)}", Enums.TextColor.White), Times.Once);
        }
    }
}
