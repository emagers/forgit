using forgit.Enums;
using forgit.Interfaces;
using System;
using System.Threading.Tasks;

namespace forgit.Providers
{
    public class ConsoleOutputter : IOutput
    {
        public async Task Write(string output, TextColor textColor)
        {
            ConsoleColor current = Console.ForegroundColor;

            Console.ForegroundColor = (ConsoleColor)textColor;
            Console.Write(output);
            Console.ForegroundColor = current;

            await Task.CompletedTask;
        }

        public async Task WriteLine(string output, TextColor textColor)
        {
            ConsoleColor current = Console.ForegroundColor;

            Console.ForegroundColor = (ConsoleColor)textColor;
            Console.WriteLine(output);
            Console.ForegroundColor = current;

            await Task.CompletedTask;
        }
    }
}
