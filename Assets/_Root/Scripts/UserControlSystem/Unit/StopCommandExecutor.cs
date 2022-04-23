using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"Stopped");
        }
    }
}