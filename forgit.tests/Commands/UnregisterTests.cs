using forgit.Commands;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class UnregisterTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly Mock<ISettings> settings = new Mock<ISettings>();
        private readonly RepositoryList baseList = new RepositoryList
        {
            Repositories = new System.Collections.Generic.List<Repository>
                {
                    new Repository
                    {
                        Name = "unregister",
                        Path = "C:\\"
                    }
                }
        };

        public UnregisterTests()
        {
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(baseList);
        }

        [Fact]
        public async Task UnregisterRepoNotRegistered_ShouldThrowException()
        {
            Unregister command = new Unregister(settings.Object, mockOutputter.Object);
            await Assert.ThrowsAsync<RepositoryNotRegisteredException>(() => command.Execute(new Options.UnregisterOptions { Name = "nope" }));
        }

        [Fact]
        public async Task UnregisterRegisteredRepo_ShouldRemoveRepo()
        {
            Unregister command = new Unregister(settings.Object, mockOutputter.Object);
            await command.Execute(new Options.UnregisterOptions { Name = "unregister" });

            Assert.DoesNotContain(baseList.Repositories, repo => repo.Name.Equals("unregister", StringComparison.OrdinalIgnoreCase));
        }
    }
}
