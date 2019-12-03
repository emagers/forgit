using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using forgit.Interfaces;
using forgit.Options;

namespace forgit.Commands
{
    public class Clone : BaseCommand
    {
        private readonly CloneOptions options;

        public Clone(ISettings settings, IOutput output, CloneOptions options) : base(settings, output)
        {
            this.options = options;
        }

        public override async Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
