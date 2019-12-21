namespace forgit.Interfaces
{
    public interface IProcessRunner
    {
        void InvokeProcess(string directory, string command, string arguments);
    }
}