using System;
using Abstractions;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> callback)
        {
            
        }
    }
}