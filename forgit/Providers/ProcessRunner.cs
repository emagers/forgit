using System.Diagnostics;
using System.IO;
using forgit.Interfaces;

namespace forgit.Providers
{
    public class ProcessRunner : IProcessRunner
    {
        public bool InvokeProcess(string directory, string command, string arguments)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo {
                    FileName = Path.Combine(directory, command),
                    Arguments = arguments,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            return process.ExitCode == 0;
        }
    }
}