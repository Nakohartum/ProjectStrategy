using System;
using Abstractions;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IStopCommand> callback)
        {
            
        }
    }
}