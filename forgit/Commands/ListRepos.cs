using System;
using System.Threading.Tasks;
using forgit.Interfaces;
using forgit.Options;

namespace forgit.Commands
{
    public class ListRepos : BaseCommand
    {
        private readonly ListOptions options;

        public ListRepos(ISettings settings, IOutput output, ListOptions options) : base(settings, output)
        {
            this.options = options;
        }

        public override Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
