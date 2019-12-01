using forgit.Interfaces;
using System.Threading.Tasks;

namespace forgit.Commands
{
    public abstract class BaseCommand
    {
        protected readonly ISettings settings;
        protected readonly IOutput output;

        protected BaseCommand(ISettings settings, IOutput output)
        {
            this.settings = settings;
            this.output = output;
        }

        public abstract Task Execute();
    }
}
