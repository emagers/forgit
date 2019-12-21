using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forgit.Enums;
using forgit.Interfaces;
using forgit.Options;

namespace forgit.Commands
{
    public class Clone : BaseCommand
    {
        private readonly CloneOptions options;
        private readonly IProcessRunner processRunner;

        public Clone(ISettings settings, IOutput output, IProcessRunner processRunner, CloneOptions options) : base(settings, output)
        {
            this.options = options;
        }

        public override async Task Execute()
        {
            if (string.IsNullOrEmpty(options.Path)) 
            {
                options.Path = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(options.Name)) 
            {
                options.Name = ParseProjectNameFromGitUrl(options.Url);
            }

            processRunner.InvokeProcess(options.Path, "git clone", options.Url);

            await output.Write($"{options.Url} was cloned to {options.Path} with the project name: {options.Name}", TextColor.Cyan);
        }

        private static string ParseProjectNameFromGitUrl(string url) => new Uri(url).Segments.Last().Split('.').First();
    }
}
