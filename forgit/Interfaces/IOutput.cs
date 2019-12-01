using forgit.Enums;
using System.Threading.Tasks;

namespace forgit.Interfaces
{
    public interface IOutput
    {
        Task Write(string output, TextColor textColor);
        Task WriteLine(string output, TextColor textColor);
    }
}
