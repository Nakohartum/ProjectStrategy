using System;
using _Root.Scripts.UserControlSystem.CommandCreator;
using Abstractions;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem
{
    public class CommandsButtonModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;

        private bool _commandIsPending;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
        {
            if (_commandIsPending)
            {
                ProcessOnCancel();
            }

            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _attacker.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _mover.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _stopper.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _patroller.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
        }

        public void ExecuteCommandWrapper(ICommandExecutor commandExecutor, object command)
        {
            commandExecutor.ExecuteCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            ProcessOnCancel();
        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _stopper.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();
            OnCommandCancel?.Invoke();
        }
    }
}