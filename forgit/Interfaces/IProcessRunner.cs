namespace forgit.Interfaces
{
    public interface IProcessRunner
    {
        bool InvokeProcess(string directory, string command, string arguments);
    }
}