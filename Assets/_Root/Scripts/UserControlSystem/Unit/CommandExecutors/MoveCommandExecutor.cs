using System;
using System.Threading;
using System.Threading.Tasks;
using _Root.Scripts.Utils;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace _Root.Scripts.Core.Unit
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        private int _walkTriggerHash;
        private int _idleTriggerHash;

        private void Awake()
        {
            _walkTriggerHash = Animator.StringToHash("Walk");
            _idleTriggerHash = Animator.StringToHash("Idle");
        }
        

        public override async Task ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(_walkTriggerHash);
            _stopCommandExecutor.CancellationToken = new CancellationTokenSource();
            try
            {
                await _stop.WithCancellation(_stopCommandExecutor.CancellationToken.Token);
            }
            catch
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
            }
            _animator.SetTrigger(_idleTriggerHash);
            _stopCommandExecutor.CancellationToken = null;
        }
    }
}