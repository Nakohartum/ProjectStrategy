using System;
using _Root.Scripts.Abstractions;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;
using _Root.Scripts.Utils;

namespace _Root.Scripts.Core.Unit
{
    public class ChomperCommandsQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] private CommandExecutorBase<IMoveCommand> _moveCommandExecutor;
        [Inject] private CommandExecutorBase<IPatrolCommand> _patrolCommandExecutor;
        [Inject] private CommandExecutorBase<IAttackCommand> _attackCommandExecutor;
        [Inject] private CommandExecutorBase<IStopCommand> _stopCommandExecutor;

        private readonly ReactiveCollection<ICommand> _commands = new ReactiveCollection<ICommand>();

        [Inject]
        private void Init()
        {
            _commands.ObserveAdd().Subscribe(OnNewCommandAdded).AddTo(this);
        }

        private void OnNewCommandAdded(ICommand command, int index)
        {
            if (index == 0)
            {
                ExecuteCommand(command);
            }
        }

        private async void ExecuteCommand(ICommand command)
        {
            await _moveCommandExecutor.TryExecuteCommand(command);
            await _patrolCommandExecutor.TryExecuteCommand(command);
            await _attackCommandExecutor.TryExecuteCommand(command);
            await _stopCommandExecutor.TryExecuteCommand(command);
            if (_commands.Count > 0)
            {
                _commands.RemoveAt(0);
            }

            CheckTheQueue();
        }

        private void CheckTheQueue()
        {
            if (_commands.Count > 0)
            {
                ExecuteCommand(_commands[0]);
            }
        }

        public void EnqueCommand(object command)
        {
            var wrappedCommand = command as ICommand;
            
            _commands.Add(wrappedCommand);
        }

        public void Clear()
        {
            _commands.Clear();
            _stopCommandExecutor.ExecuteSpecificCommand(new StopCommand());
        }
    }
}