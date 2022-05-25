using _Root.Scripts.Abstractions;
using Abstractions;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.Core.Unit
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] private CommandExecutorBase<IProduceUnitCommand> _produceUnit;
        [Inject] private CommandExecutorBase<ISetRallyPointCommand> _setRallyPoint;
        public async void EnqueCommand(object command)
        {
            await _produceUnit.TryExecuteCommand(command);
            await _setRallyPoint.TryExecuteCommand(command);
        }

        public void Clear()
        {
            
        }
    }
}