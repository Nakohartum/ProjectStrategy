using System.Threading;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationToken { get; set; }
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationToken?.Cancel();
        }
    }
}