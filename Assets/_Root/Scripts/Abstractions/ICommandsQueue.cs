namespace _Root.Scripts.Abstractions
{
    public interface ICommandsQueue
    {
        void EnqueCommand(object command);
        void Clear();
    }
}