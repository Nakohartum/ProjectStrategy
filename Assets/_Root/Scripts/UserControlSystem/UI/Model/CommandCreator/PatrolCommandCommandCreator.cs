using System;
using Abstractions;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> callback)
        {
            
        }
    }
}