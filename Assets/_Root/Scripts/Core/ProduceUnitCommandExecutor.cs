using System;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core;
using Abstractions;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Root.Scripts.UserControlSystem.Unit.CommandExecutors
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        private void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeLeft -= Time.deltaTime;
            if (innerTask.TimeLeft <= 0)
            {
                RemoveTaskAtIndex(0);
                Instantiate(innerTask.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                    Quaternion.identity, _unitsParent);
            }
        }
            
        

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(command.Icon, command.UnitName, command.ProductionTime,
                command.UnitPrefab));
        }

        public void Cancel(int index)
        {
            RemoveTaskAtIndex(index);
        }

        private void RemoveTaskAtIndex(int index)
        {
            for (int i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }

            _queue.RemoveAt(_queue.Count - 1);
        }
    }
}