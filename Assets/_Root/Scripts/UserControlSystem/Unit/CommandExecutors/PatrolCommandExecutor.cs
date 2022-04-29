using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} patrolling from {command.From} to {command.To}");
        }
    }
}