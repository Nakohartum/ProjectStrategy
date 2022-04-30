using System;
using _Root.Scripts.Core.Unit;
using Abstractions;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        
        protected override void ClassSpecificCommandCreation(Action<IStopCommand> callback)
        {
            callback?.Invoke(new StopCommand());
        }
    }
}