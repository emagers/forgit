﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using forgit.Exceptions;
using forgit.Interfaces;
using forgit.Models;
using forgit.Options;

namespace forgit.Commands
{
    public class Register : BaseCommand, IBaseCommand
    {
        public Register(ISettings settings, IOutput output) : base(settings, output)
        {

        }

        public async Task Execute(IOptions options)
        {
            RegisterOptions registerOptions = options as RegisterOptions;

            RepositoryList repositoryList = await settings.GetRepositories();

            if (string.IsNullOrEmpty(registerOptions.Path))
            {
                registerOptions.Path = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(registerOptions.Name))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(registerOptions.Path);
                registerOptions.Name = directoryInfo.Name;
            }

            Repository repository = repositoryList.Repositories.FirstOrDefault(repo => repo.Name.Equals(registerOptions.Name, StringComparison.OrdinalIgnoreCase));
            if (repository != null)
            {
                throw new RepositoryAlreadyRegisteredException(repository.Name, repository.Path);
            }

            repositoryList.Repositories.Add(new Repository
            {
                Name = registerOptions.Name,
                Path = registerOptions.Path
            });
            await settings.SaveRepositories(repositoryList);

            await output.Write(registerOptions.Name, Enums.TextColor.Cyan);
            await output.Write(" has been registered at ", (Enums.TextColor)Console.ForegroundColor);
            await output.Write($"{registerOptions.Path}\n", Enums.TextColor.White);
        }
    }
}
