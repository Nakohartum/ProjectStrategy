using System;
using System.Threading;
using System.Threading.Tasks;
using _Root.Scripts.Abstractions;
using _Root.Scripts.UserControlSystem;
using _Root.Scripts.Utils;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Root.Scripts.Core.Unit
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {
            public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {
                private AttackOperation _attackOperation;

                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    attackOperation.OnComplete += OnComplete;
                }

                private void OnComplete()
                {
                    _attackOperation.OnComplete -= OnComplete;
                    OnWaitFinish(new AsyncExtensions.Void());
                }
                
            }

            private event Action OnComplete;
            private readonly AttackCommandExecutor _attackCommandExecutor;
            private readonly IAttackable _target;
            private bool _isCancelled;

            public AttackOperation(AttackCommandExecutor executor, IAttackable target)
            {
                _attackCommandExecutor = executor;
                _target = target;
                var thread = new Thread(AttackAlgorythm);
                thread.Start();
            }

            public void Cancel()
            {
                _isCancelled = true;
                OnComplete?.Invoke();
            }

            private void AttackAlgorythm(object obj)
            {
                while (true)
                {
                    if (_attackCommandExecutor == null || _attackCommandExecutor._ourHealth.Health == 0 || _target.Health == 0 || _isCancelled)
                    {
                        OnComplete?.Invoke();
                        return;
                    }

                    var targetPosition = default(Vector3);
                    var ourPosition = default(Vector3);
                    var ourRotation = default(Quaternion);
                    lock (_attackCommandExecutor)
                    {
                        targetPosition = _attackCommandExecutor._targetPos;
                        ourPosition = _attackCommandExecutor._ourPos;
                        ourRotation = _attackCommandExecutor._ourRot;
                    }

                    var vector = targetPosition - ourPosition;
                    var distanceToTarget = vector.magnitude;
                    if (distanceToTarget > _attackCommandExecutor._attackingDistance)
                    {
                        var finalDistance = targetPosition -
                                            vector.normalized * (_attackCommandExecutor._attackingDistance * 0.9f);
                        _attackCommandExecutor._targetPositions.OnNext(finalDistance);
                        Thread.Sleep(100);
                    }
                    else if (ourRotation != Quaternion.LookRotation(vector))
                    {
                            _attackCommandExecutor._targetRotations.OnNext(Quaternion.LookRotation(vector));
                    }
                    else
                    {
                        _attackCommandExecutor._attackTargets.OnNext(_target);
                        Thread.Sleep((int) _attackCommandExecutor._attackingPeriod);
                    }
                }
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                return new AttackOperationAwaiter(this);
            }
        }
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        [Inject] private IHealth _ourHealth;
        [Inject(Id = "AttackDistance")] private float _attackingDistance;
        [Inject(Id = "AttackPeriod")] private float _attackingPeriod;

        private int _walkHash = Animator.StringToHash("Walk");
        private int _attackHash = Animator.StringToHash("Attack");
        private int _idleHash = Animator.StringToHash("Idle");
        private Vector3 _ourPos;
        private Vector3 _targetPos;
        private Quaternion _ourRot;
        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();
        private Transform _targetTransform;
        private AttackOperation _attackOperation;

        [Inject]
        private void Init()
        {
            _targetPositions.Select(value => new Vector3((float) Math.Round(value.x, 2), (float) Math.Round(value.y, 2),
                (float) Math.Round(value.z, 2))).Distinct().ObserveOnMainThread().Subscribe(StartMovingAction);

            _attackTargets.ObserveOnMainThread().Subscribe(StartAttackingTargets);
            _targetRotations.ObserveOnMainThread().Subscribe(SetAttackRotation);
        }

        private void StartMovingAction(Vector3 pos)
        {
            GetComponent<NavMeshAgent>().destination = pos;
            _animator.SetTrigger(_walkHash);
        }

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
            _animator.SetTrigger(_attackHash);
            target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }
        
        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            _targetTransform = (command.Target as Component).transform;
            _attackOperation = new AttackOperation(this, command.Target);
            Update();
            _stopCommandExecutor.CancellationToken = new CancellationTokenSource();
            try
            {
                await _attackOperation.WithCancellation(_stopCommandExecutor.CancellationToken.Token);
            }
            catch
            {
                _attackOperation.Cancel();
            }
            _animator.SetTrigger(_idleHash);
            _attackOperation = null;
            _targetTransform = null;
            _stopCommandExecutor.CancellationToken = null;
        }

        private void Update()
        {
            if (_attackOperation == null)
            {
                return;
            }

            lock (this)
            {
                _ourPos = transform.position;
                _ourRot = transform.rotation;
                if (_targetTransform != null)
                {
                    _targetPos = _targetTransform.position;
                }
            }
        }
    }
}