using System;
using System.Threading;
using System.Threading.Tasks;
using _Root.Scripts.Utils;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace _Root.Scripts.Core.Unit
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        private int _walkHash = Animator.StringToHash("Walk");
        private int _idleHash = Animator.StringToHash("Idle");
        
        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            var pointStart = command.From;
            var pointEnd = command.To;
            var navMesh = GetComponent<NavMeshAgent>();
            while (true)
            {
                navMesh.destination = pointEnd;
                _animator.SetTrigger(_walkHash);
                _stopCommandExecutor.CancellationToken = new CancellationTokenSource();
                try
                {
                    await _stop.WithCancellation(_stopCommandExecutor.CancellationToken.Token);
                }
                catch
                {
                    navMesh.isStopped = true;
                    navMesh.ResetPath();
                    break;
                }

                (pointStart, pointEnd) = (pointEnd, pointStart);
            }

            _stopCommandExecutor.CancellationToken = null;
            _animator.SetTrigger(_idleHash);
        }
    }
}