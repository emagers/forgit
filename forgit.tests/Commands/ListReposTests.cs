using forgit.Commands;
using forgit.Enums;
using forgit.Interfaces;
using forgit.Providers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Commands
{
    public class ListReposTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly ISettings settings = new Settings("validSettings.json");

        [Fact]
        public async Task ListCommand_ShouldListAllProjectsInSettings()
        {
            ListRepos listCommand = new ListRepos(settings, mockOutputter.Object);

            await listCommand.Execute();

            mockOutputter.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<TextColor>()));
        }
    }
}
