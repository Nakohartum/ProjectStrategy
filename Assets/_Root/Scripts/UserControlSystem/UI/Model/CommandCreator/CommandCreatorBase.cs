using System;
using Abstractions;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        public ICommandExecutor ProccesCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;
            if (classSpecificExecutor != null)
            {
                ClassSpecificCommandCreation(callback);
            }

            return commandExecutor;
        }

        protected abstract void ClassSpecificCommandCreation(Action<T> callback);

        public virtual void ProcessCancel()
        {
            
        }
    }
}