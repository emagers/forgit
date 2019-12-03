using forgit.Commands;
using forgit.Enums;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Providers;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class UnregisterTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly ISettings settings = new Settings("unregisterSettings.json");

        [Fact]
        public async Task UnregisterRepoNotRegistered_ShouldThrowException()
        {
            Unregister command = new Unregister(settings, mockOutputter.Object, new Options.UnregisterOptions { Name = "nope" });
            await Assert.ThrowsAsync<RepositoryNotRegisteredException>(() => command.Execute());
        }

        [Fact]
        public async Task UnregisterRegisteredRepo_ShouldRemoveRepo()
        {
            Unregister command = new Unregister(settings, mockOutputter.Object, new Options.UnregisterOptions { Name = "unregister" });
            await command.Execute();

            RepositoryList list = await settings.GetRepositories();
            Assert.DoesNotContain(list.Repositories, repo => repo.Name.Equals("unregister", StringComparison.OrdinalIgnoreCase));
            mockOutputter.Verify(x => x.WriteLine("unregister has been unregistered", TextColor.Cyan), Times.Once);
        }
    }
}
