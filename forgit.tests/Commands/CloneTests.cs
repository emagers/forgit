using forgit.Interfaces;
using forgit.Options;
using forgit.Providers;
using System;
using Xunit;
using Moq;
using forgit.Commands;
using forgit.Enums;
using System.Threading.Tasks;

namespace forgit.tests.Commands
{
    public class CloneTests
    {
        private readonly Mock<IOutput> mockOutputter = new Mock<IOutput>();
        private readonly ISettings settings = new Settings("cloneSettings.json");

        [Fact]
        public async Task Clone_WithoutProjectName_ShouldParseProjectName() 
        {
            CloneOptions options = new CloneOptions
            {
                Name = "",
                Path = "./",
                Url = "https://github.com/emagers/forgit.git"
            };

            Clone command = new Clone(settings, mockOutputter.Object, options);
            await command.Execute();

            mockOutputter.Verify(x => x.Write($"{options.Url} was cloned to {options.Path} with the project name: forgit", It.IsAny<TextColor>()));
        }

        [Fact]
        public async Task Clone_WithoutDirectory_ShouldUseCurrentDirectoy() 
        {
            CloneOptions options = new CloneOptions
            {
                Name = "apples",
                Path = "",
                Url = "https://github.com/emagers/forgit.git"
            };

            Clone command = new Clone(settings, mockOutputter.Object, options);
            await command.Execute();

            mockOutputter.Verify(x => x.Write($"{options.Url} was cloned to {Environment.CurrentDirectory} with the project name: {options.Name}", It.IsAny<TextColor>()));
        }

        [Fact]
        public async Task Clone_WithNameAndDirectory_ShouldUseProvidedParams() 
        {
            CloneOptions options = new CloneOptions
            {
                Name = "apples2",
                Path = "./newDir",
                Url = "https://github.com/emagers/forgit.git"
            };

            Clone command = new Clone(settings, mockOutputter.Object, options);
            await command.Execute();

            mockOutputter.Verify(x => x.Write($"{options.Url} was cloned to {options.Path} with the project name: {options.Name}", It.IsAny<TextColor>()));
        }
    }
}
