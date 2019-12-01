using forgit.Exceptions;
using forgit.Models;
using forgit.Providers;
using System.Threading.Tasks;
using Xunit;

namespace forgit.tests.Providers
{
    public class SettingsTests
    {
        private const string path = "validSettings.json";
        private readonly Settings settings = new Settings(path);

        [Fact]
        public async Task ReadingSettingsFromValidFile_ShouldReturnRepositories()
        {
            RepositoryList list = await settings.GetRepositories();

            Assert.NotNull(list);
        }

        [Fact]
        public async Task AddingRepository_ShouldPersist()
        {
            RepositoryList list = await settings.GetRepositories();
            list.Repositories.Add(new Repository
            {
                Name = "test1",
                Path = "C:/"
            });

            await settings.SaveRepositories(list);

            RepositoryList updated = await settings.GetRepositories();

            Assert.Contains(updated.Repositories, repo => repo.Name == "test1" && repo.Path == "C:/");
        }

        [Fact]
        public async Task ReadingSettingsFromImaginaryFile_ShouldThrowException()
        {
            Settings badSettings = new Settings("a.json");

            await Assert.ThrowsAsync<SettingsFileNotFoundException>(() => badSettings.GetRepositories());
        }

        [Fact]
        public async Task ReadingSettingsFromInvalidFile_ShouldThrowException()
        {
            Settings badSettings = new Settings("invalidSettings.json");

            await Assert.ThrowsAsync<SettingsFileInvalidJsonException>(() => badSettings.GetRepositories());
        }
    }
}
