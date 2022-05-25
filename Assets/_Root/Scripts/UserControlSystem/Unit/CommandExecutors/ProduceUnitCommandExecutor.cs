using System;
using System.Threading.Tasks;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core;
using _Root.Scripts.Core.Unit;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Root.Scripts.UserControlSystem.Unit.CommandExecutors
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        [Inject] private DiContainer _diContainer;
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
                var unit = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, transform.position, 
                    Quaternion.identity, _unitsParent);
                var queue = unit.GetComponent<ICommandsQueue>();
                var mainBuilding = GetComponent<MainBuilding>();
                queue.EnqueCommand(new MoveCommand(mainBuilding.RallyPoint));
            }
        }
            
        

        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
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