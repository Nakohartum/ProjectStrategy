using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{name} is moving to {command.Target}");
        }
    }
}