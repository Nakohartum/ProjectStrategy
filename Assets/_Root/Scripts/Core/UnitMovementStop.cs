using System;
using _Root.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace _Root.Scripts.Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter : IAwaiter<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;
            private Action _continuation;
            private bool _isCompleted;

            public bool IsCompleted => _isCompleted;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.OnStop -= OnStop;
                _isCompleted = true;
                _continuation?.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                if (_isCompleted)
                {
                    continuation?.Invoke();
                }
                else
                {
                    _continuation = continuation;
                }
            }

            public AsyncExtensions.Void GetResult() => new AsyncExtensions.Void();
        }

        public event Action OnStop;

        [SerializeField] private NavMeshAgent _navMeshAgent;

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