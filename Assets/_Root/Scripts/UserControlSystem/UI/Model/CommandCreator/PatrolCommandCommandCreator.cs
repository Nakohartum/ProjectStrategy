using System;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private SelectableObject _selectable;

        protected override IPatrolCommand CreateCommand(Vector3 argument)
        {
            return new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, argument);
        }
    }
}