using System.Diagnostics;
using forgit.Interfaces;

namespace forgit.Providers
{
    public class ProcessRunner : IProcessRunner
    {
        private readonly IOutput outputter;

        public ProcessRunner(IOutput outputter)
        {
            this.outputter = outputter;
        }

        public bool InvokeProcess(string directory, string command, string arguments)
        {
            ProcessStartInfo StartInfo = new ProcessStartInfo
            {
                WorkingDirectory = directory,
                FileName = command,
                Arguments = arguments,
                CreateNoWindow = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false
            };

            using Process process = Process.Start(StartInfo);
            process.WaitForExit();

            //string stdOut = process.StandardOutput.ReadToEnd();
            //string stdErr = process.StandardError.ReadToEnd();

            //outputter.WriteLine(stdOut, Enums.TextColor.Gray);
            //outputter.WriteLine(stdErr, Enums.TextColor.Red);

            return process.ExitCode == 0;
        }
    }
}