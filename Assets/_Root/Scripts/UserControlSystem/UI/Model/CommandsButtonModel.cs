using System;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core.Unit;
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
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _setRallyPoint;

        private bool _commandIsPending;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue queue)
        {
            if (_commandIsPending)
            {
                ProcessOnCancel();
            }

            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(command, queue));
            _attacker.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(command, queue));
            _mover.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(command, queue));
            _stopper.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(command, queue));
            _patroller.ProccesCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(command, queue));
            _setRallyPoint.ProccesCommandExecutor(commandExecutor, 
                command => ExecuteCommandWrapper(command, queue));
        }

        public void ExecuteCommandWrapper(object command, ICommandsQueue queue)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                queue.Clear();
            }
            queue.EnqueCommand(command);
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