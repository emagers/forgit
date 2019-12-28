using System.Threading.Tasks;

namespace forgit.Interfaces
{
    public interface IBaseCommand
    {
        Task Execute(IOptions options);
    }
}
