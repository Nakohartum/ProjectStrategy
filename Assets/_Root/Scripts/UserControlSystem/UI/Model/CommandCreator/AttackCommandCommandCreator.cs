using System;
using System.Threading;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core.Unit;
using _Root.Scripts.Utils;
using Abstractions;
using Injector;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument)
        {
            return new AttackCommand(argument);
        }
    }
}