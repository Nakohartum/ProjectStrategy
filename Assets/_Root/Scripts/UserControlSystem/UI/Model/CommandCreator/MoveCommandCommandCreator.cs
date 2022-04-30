using System;
using System.Threading;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        
        protected override IMoveCommand CreateCommand(Vector3 argument)
        {
            return new MoveCommand(argument);
        }
    }
}