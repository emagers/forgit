using forgit.Commands;
using forgit.Enums;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Providers;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class RegisterTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly ISettings settings = new Settings("registerSettings.json");

        [Fact]
        public async Task RegisterRepo_RepoNameAlreadyExists_ShouldThrowException()
        {
            Register command = new Register(settings, mockOutputter.Object, new Options.RegisterOptions { Name = "registered", Path = $"C:{Path.DirectorySeparatorChar}" });
            await Assert.ThrowsAsync<RepositoryAlreadyRegisteredException>(() => command.Execute());
        }

        [Fact]
        public async Task RegisterRepo_NoNameSpecified_NameShouldBeDirectoryName()
        {
            Register command = new Register(settings, mockOutputter.Object, new Options.RegisterOptions { Path = $"C:{Path.DirectorySeparatorChar}registertest1" });
            await command.Execute();

            RepositoryList list = await settings.GetRepositories();
            Assert.Contains(list.Repositories, repo => repo.Name.Equals("registertest1", StringComparison.OrdinalIgnoreCase) && repo.Path.Equals($"C:{Path.DirectorySeparatorChar}registertest1"));
            mockOutputter.Verify(x => x.WriteLine($"registertest1 has been registered at C:{Path.DirectorySeparatorChar}registertest1", TextColor.Cyan), Times.Once);
        }

        [Fact]
        public async Task RegisterRepo_NoPathSpecified_PathShouldBeExecutingDirectory()
        {
            Register command = new Register(settings, mockOutputter.Object, new Options.RegisterOptions { });
            await command.Execute();

            string dir = Environment.CurrentDirectory;
            string dirName = dir.Split(Path.DirectorySeparatorChar).Last();

            RepositoryList list = await settings.GetRepositories();
            Assert.Contains(list.Repositories, repo => repo.Name.Equals(dirName, StringComparison.OrdinalIgnoreCase) && repo.Path.Equals(dir));
            mockOutputter.Verify(x => x.WriteLine($"{dirName} has been registered at {dir}", TextColor.Cyan), Times.Once);
        }

        [Fact]
        public async Task RegisterRepo_PathAndNameSpecified_ShouldMatch()
        {
            Register command = new Register(settings, mockOutputter.Object, new Options.RegisterOptions { Name = "test2", Path = $"C:{Path.DirectorySeparatorChar}registertest2" });
            await command.Execute();

            RepositoryList list = await settings.GetRepositories();
            Assert.Contains(list.Repositories, repo => repo.Name.Equals("test2", StringComparison.OrdinalIgnoreCase) && repo.Path.Equals($"C:{Path.DirectorySeparatorChar}registertest2"));
            mockOutputter.Verify(x => x.WriteLine($"test2 has been registered at C:{Path.DirectorySeparatorChar}registertest2", TextColor.Cyan), Times.Once);
        }
    }
}
