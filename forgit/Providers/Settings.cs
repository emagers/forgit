using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace forgit.Providers
{
    public class Settings : ISettings
    {
        public readonly string path;

        public Settings(string path)
        {
            this.path = path;
        }

        public async Task<RepositoryList> GetRepositories()
        {
            try
            {
                using StreamReader reader = new StreamReader(File.OpenRead(path));
                return JsonSerializer.Deserialize<RepositoryList>(await reader.ReadToEndAsync());
            }
            catch (FileNotFoundException ex)
            {
                throw new SettingsFileNotFoundException(path, ex);
            }
            catch (JsonException ex)
            {
                throw new SettingsFileInvalidJsonException(path, ex);
            }
        }

        public async Task SaveRepositories(RepositoryList repositories)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Open, FileAccess.Write));
                await writer.WriteAsync(JsonSerializer.Serialize(repositories));
            }
            catch (FileNotFoundException ex)
            {
                throw new SettingsFileNotFoundException(path, ex);
            }
        }
    }
}
