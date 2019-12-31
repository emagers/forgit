using forgit.Commands;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
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
        private readonly Mock<ISettings> settings = new Mock<ISettings>();
        private readonly RepositoryList baseList = new RepositoryList
        {
            Repositories = new System.Collections.Generic.List<Repository>
                {
                    new Repository
                    {
                        Name = "registered",
                        Path = "C:\\"
                    }
                }
        };

        public RegisterTests()
        {
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(baseList);
        }

        [Fact]
        public async Task RegisterRepo_RepoNameAlreadyExists_ShouldThrowException()
        {
            Register command = new Register(settings.Object, mockOutputter.Object);
            await Assert.ThrowsAsync<RepositoryAlreadyRegisteredException>(() => command.Execute(new Options.RegisterOptions { Name = "registered", Path = $"C:{Path.DirectorySeparatorChar}" }));
        }

        [Fact]
        public async Task RegisterRepo_NoNameSpecified_NameShouldBeDirectoryName()
        {
            Register command = new Register(settings.Object, mockOutputter.Object);
            await command.Execute(new Options.RegisterOptions { Path = $"C:{Path.DirectorySeparatorChar}registertest1" });

            Assert.Contains(baseList.Repositories, repo => repo.Name.Equals("registertest1", StringComparison.OrdinalIgnoreCase) && repo.Path.Equals($"C:{Path.DirectorySeparatorChar}registertest1"));
        }

        [Fact]
        public async Task RegisterRepo_NoPathSpecified_PathShouldBeExecutingDirectory()
        {
            Register command = new Register(settings.Object, mockOutputter.Object);
            await command.Execute(new Options.RegisterOptions { });

            string dir = Environment.CurrentDirectory;
            string dirName = dir.Split(Path.DirectorySeparatorChar).Last();

            Assert.Contains(baseList.Repositories, repo => repo.Name.Equals(dirName, StringComparison.OrdinalIgnoreCase) && repo.Path.Equals(dir));
        }

        [Fact]
        public async Task RegisterRepo_PathAndNameSpecified_ShouldMatch()
        {
            Register command = new Register(settings.Object, mockOutputter.Object);
            await command.Execute(new Options.RegisterOptions { Name = "test2", Path = $"C:{Path.DirectorySeparatorChar}registertest2" });

            Assert.Contains(baseList.Repositories, repo => repo.Name.Equals("test2", StringComparison.OrdinalIgnoreCase) && repo.Path.Equals($"C:{Path.DirectorySeparatorChar}registertest2"));
        }
    }
}
