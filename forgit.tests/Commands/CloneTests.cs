using forgit.Interfaces;
using forgit.Options;
using forgit.Providers;
using System;
using Xunit;
using Moq;
using forgit.Commands;
using forgit.Enums;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Models;
using System.IO;

namespace forgit.tests.Commands
{
    public class CloneTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly Mock<IProcessRunner> mockProcessRunner = new Mock<IProcessRunner>();
        private readonly Mock<IBaseCommand> register = new Mock<IBaseCommand>();
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

        public CloneTests()
        {
            mockProcessRunner.Setup(x => x.InvokeProcess(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(baseList);
        }

        [Fact]
        public async Task Clone_WithoutProjectName_ShouldParseProjectName() 
        {
            string expected = "forgit";
            CloneOptions options = new CloneOptions
            {
                Name = "",
                Path = "./",
                Url = $"https://github.com/emagers/{expected}.git"
            };

            Clone command = new Clone(settings.Object, mockOutputter.Object, mockProcessRunner.Object, register.Object);
            await command.Execute(options);

            register.Verify(x => x.Execute(It.Is<RegisterOptions>(opts => opts.Name == expected)));
        }

        [Fact]
        public async Task Clone_WithoutDirectory_ShouldUseCurrentDirectoy() 
        {
            string name = "apples";
            string currentDir = Environment.CurrentDirectory;
            string projectName = "forgit";
            string expectedDir = Path.Combine(currentDir, projectName);
            CloneOptions options = new CloneOptions
            {
                Name = name,
                Path = "",
                Url = $"https://github.com/emagers/{projectName}.git"
            };

            Clone command = new Clone(settings.Object, mockOutputter.Object, mockProcessRunner.Object, register.Object);
            await command.Execute(options);

            register.Verify(x => x.Execute(It.Is<RegisterOptions>(opts => opts.Path == expectedDir)));
        }

        [Fact]
        public async Task Clone_WithNameAndDirectory_ShouldUseProvidedParams() 
        {
            string expectedName = "apples2";
            string projectName = "forgit";
            string dir = "./newDir";
            string expectedDir = Path.Combine(dir, projectName);
            CloneOptions options = new CloneOptions
            {
                Name = expectedName,
                Path = dir,
                Url = $"https://github.com/emagers/{projectName}.git"
            };

            Clone command = new Clone(settings.Object, mockOutputter.Object, mockProcessRunner.Object, register.Object);
            await command.Execute(options);

            register.Verify(x => x.Execute(It.Is<RegisterOptions>(opts => 
                opts.Name == expectedName && 
                opts.Path == expectedDir
            )));
        }

        [Fact]
        public async Task Clone_ProcessFails_ShouldThrowCloneException()
        {
            Mock<IProcessRunner> processRunner = new Mock<IProcessRunner>();
            processRunner.Setup(x => x.InvokeProcess(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            CloneOptions options = new CloneOptions
            {
                Name = "apples2",
                Path = "./newDir",
                Url = "https://github.com/emagers/forgit.git"
            };

            Clone command = new Clone(settings.Object, mockOutputter.Object, processRunner.Object, register.Object);
            await Assert.ThrowsAsync<CloneException>(() => command.Execute(options));
        }
    }
}
