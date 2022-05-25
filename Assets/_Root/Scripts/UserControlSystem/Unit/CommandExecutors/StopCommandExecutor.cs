using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core.Unit
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationToken { get; set; }
        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationToken?.Cancel();
        }
    }
}