using System;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem.CommandCreator
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetContext _assetContext;
        [Inject] private SelectableObject _selectable;

        private Action<IPatrolCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value vector3Value)
        {
            vector3Value.OnNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 value)
        {
                _creationCallback?.Invoke(
                _assetContext.Inject(new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, value)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> callback)
        {
            _creationCallback = callback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }
    }
}