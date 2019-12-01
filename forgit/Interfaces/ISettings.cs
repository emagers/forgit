using forgit.Models;
using System.Threading.Tasks;

namespace forgit.Interfaces
{
    public interface ISettings
    {
        Task<RepositoryList> GetRepositories();
        Task SaveRepositories(RepositoryList repositories);
    }
}
