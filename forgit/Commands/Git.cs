using forgit.Interfaces;
using System;
using System.Threading.Tasks;

namespace forgit.Commands
{
    public class Git : BaseCommand, IBaseCommand
    {
        public Git(ISettings settings, IOutput output) : base(settings, output)
        {
        }

        public Task Execute(IOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
