using System;
using _Root.Scripts.Abstractions;
using _Root.Scripts.UserControlSystem;
using _Root.Scripts.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace _Root.Scripts.Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.OnStop -= OnStop;
                OnWaitFinish(new AsyncExtensions.Void());
            }
        }

        public event Action OnStop;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private ColissionDetector _colissionDetector;
        [SerializeField] private int _throttleFrames = 60;
        [SerializeField] private int _continuityThreshold = 10;

        private void Awake()
        {
            _colissionDetector.Collisions.Where(_ => _navMeshAgent.hasPath)
                .Where(collision => collision.collider.GetComponentInParent<IUnit>() != null)
                .Select(_ => Time.frameCount).Distinct()
                .Buffer(_throttleFrames).Where(buffer =>
                {
                    for (int i = 1; i < buffer.Count; i++)
                    {
                        if (buffer[i] - buffer[i - 1] > _continuityThreshold)
                        {
                            return false;
                        }
                    }

                    return true;
                }).Subscribe(_ =>
                {
                    _navMeshAgent.isStopped = true;
                    _navMeshAgent.ResetPath();
                    OnStop?.Invoke();
                }).AddTo(this);
        }

        private void Update()
        {
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0.0f)
                    {
                        OnStop?.Invoke();
                    }
                }
            }
        }


        public IAwaiter<AsyncExtensions.Void> GetAwaiter()
        {
            return new StopAwaiter(this);
        }
    }
}