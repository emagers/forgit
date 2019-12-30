using forgit.Commands;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class GitTests
    {
        private readonly Mock<ISettings> settings = new Mock<ISettings>();
        private readonly Mock<IOutput> outputter = new Mock<IOutput>();
        private readonly Mock<IProcessRunner> processRunner = new Mock<IProcessRunner>();

        public GitTests()
        {
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(
                new Models.RepositoryList
                {
                    Repositories = new System.Collections.Generic.List<Models.Repository>()
                });
        }

        [Fact]
        public async Task ProjectNotFound_ShouldThrowError()
        {
            Git git = new Git(settings.Object, outputter.Object, processRunner.Object);

            await Assert.ThrowsAsync<RepositoryNotRegisteredException>(() => git.Execute(new GitOptions
            {
                Name = "notfound"
            }));
        }

        [Theory]
        [InlineData("pull")]
        [InlineData("git pull")]
        public async Task Command_ShouldHaveGitPrepended(string command)
        {
            string repo = "forgit";
            string expected = "git pull";
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(
                new Models.RepositoryList
                {
                    Repositories = new System.Collections.Generic.List<Models.Repository>
                    {
                        new Models.Repository
                        {
                            Name = repo,
                            Path = string.Empty
                        }
                    }
                });
            processRunner.Setup(x => x.InvokeProcess(It.IsAny<string>(), expected, It.IsAny<string>())).Returns(true);

            Git git = new Git(settings.Object, outputter.Object, processRunner.Object);
            await git.Execute(new GitOptions
            {
                Command = command,
                Name = repo
            });

            processRunner.Verify(x => x.InvokeProcess(string.Empty, expected, string.Empty), Times.Once);
        }

        [Fact]
        public async Task SuccessfulCommand_ShouldLogSuccess()
        {
            string repo = "forgit";
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(
                new Models.RepositoryList
                {
                    Repositories = new System.Collections.Generic.List<Models.Repository>
                    {
                        new Models.Repository
                        {
                            Name = repo,
                            Path = string.Empty
                        }
                    }
                });
            
            processRunner.Setup(x => x.InvokeProcess(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            Git git = new Git(settings.Object, outputter.Object, processRunner.Object);
            await git.Execute(new GitOptions
            {
                Command = "git pull",
                Name = repo
            });


        }

        [Fact]
        public async Task ErrorRunningCommand_ShouldThrowCommandExecutionException()
        {
            string repo = "forgit";
            settings.Setup(x => x.GetRepositories()).ReturnsAsync(
                new Models.RepositoryList
                {
                    Repositories = new System.Collections.Generic.List<Models.Repository>
                    {
                        new Models.Repository
                        {
                            Name = repo,
                            Path = string.Empty
                        }
                    }
                });

            processRunner.Setup(x => x.InvokeProcess(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            Git git = new Git(settings.Object, outputter.Object, processRunner.Object);
            await Assert.ThrowsAsync<CommandExecutionException>(() => git.Execute(new GitOptions
            {
                Command = "git pull",
                Name = repo
            }));
        }
    }
}
