using System;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace _Root.Scripts.Core.Unit
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;

        private int _walkTriggerHash;
        private int _idleTriggerHash;

        private void Start()
        {
            _walkTriggerHash = Animator.StringToHash("Walk");
            _idleTriggerHash = Animator.StringToHash("Idle");
        }

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(_walkTriggerHash);
            await _stop;
            _animator.SetTrigger(_idleTriggerHash);
        }
    }
}