using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} patrolling from {command.From} to {command.To}");
        }
    }
}