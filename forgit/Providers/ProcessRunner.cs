using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using forgit.Interfaces;

namespace forgit.Providers
{
    public class ProcessRunner : IProcessRunner
    {
        public void InvokeProcess(string directory, string command, string arguments)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo {
                    FileName = Path.Combine(directory, command),
                    Arguments = arguments,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }
}